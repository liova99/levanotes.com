using System.Text;
using System.Text.RegularExpressions;
using LevaNotes.Web.Models;
using LevaNotes.Web.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Westwind.AspNetCore.Markdown;

namespace LevaNotes.Web.Pages;

public class PostModel : PageModel
{
    private readonly PostService _postService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<PostModel> _logger;

    public PostModel(PostService postService, IWebHostEnvironment webHostEnvironment, IMemoryCache memoryCache, ILogger<PostModel> logger)
    {
        _postService = postService;
        _webHostEnvironment = webHostEnvironment;
        _memoryCache = memoryCache;
        _logger = logger;
    }

    public HtmlString HtmlStringrFromMD { get; private set; } = new(string.Empty);
    public string FilePath { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string CacheKey { get; private set; } = string.Empty;

    public async Task<IActionResult> OnGet(string language, string category, string slug)
    {
        Post? post = await _postService.GetPostBySlugAsync(slug);

        if (post is null)
        {
            return RedirectTo404();
        }

        // todo temp solution (really)
        // make a new page with: This blogs were moved bla bla bla
        HashSet<int> idToRedirect = new() { 15878, 15879, 15880 };
        if (idToRedirect.Contains(post.PostId))
        {
            return Redirect("https://levankelesidis.com/blog");
        }


        string basePath = _webHostEnvironment.WebRootPath;

        string url = Request.Path;
        string expectedUrl = _postService.CreateRelativePostUrl(post);

        if (!url.ToLowerInvariant().Equals(expectedUrl))
        {
            return Redirect(expectedUrl);
        }

        if (!TryCreateRelativePath(post.BasePath, post.FileName, out string? relativePath) || string.IsNullOrWhiteSpace(relativePath))
        {
            return RedirectTo404();
        }

        string fullFilePath = Path.Combine(basePath, relativePath);

        if (!System.IO.File.Exists(fullFilePath))
        {
            return RedirectTo404();
        }

        DateTime lastModified = System.IO.File.GetLastWriteTime(fullFilePath);

        CacheKey = lastModified.ToString() + post.PostId;

        string markdown = await GetMarkdownFromPathAsync(fullFilePath, cacheKey: CacheKey);

        if (string.IsNullOrWhiteSpace(markdown))
        {
            return RedirectTo404();
        }

        HtmlString htmText = Markdown.ParseHtmlString(markdown);

        string[] htmlLines = htmText.Value!.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

        string result = AddBasePathToImgUrlsHtml(htmlLines, post.BasePath);

        HtmlStringrFromMD = new HtmlString(result);
        FilePath = relativePath;
        Title = post.Title;

        return Page();

    }

    private IActionResult RedirectTo404()
    {
        return RedirectToPage("/NotFound");
    }

    private static SemaphoreSlim _lockGetMarkdownFromPathAsync = new(1, 1);
    public async Task<string?> GetMarkdownFromPathAsync(string path, object? cacheKey = null, bool ignoreCache = false)
    {
        await _lockGetMarkdownFromPathAsync.WaitAsync();
        try
        {
            cacheKey ??= path;

            if (ignoreCache == true)
            {
                cacheKey = DateTime.UtcNow;
            }

            string? markdown = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.Priority = CacheItemPriority.Low;
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);
                return await System.IO.File.ReadAllTextAsync(path);
            });

            return markdown;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Can read from path: {path}", path);
            return string.Empty;
        }
        finally
        {
            _lockGetMarkdownFromPathAsync.Release();
        }
    }

    private bool TryCreateRelativePath(string basePath, string fileName, out string? relativePath)
    {
        relativePath = default;

        if (!fileName.EndsWith(".md", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        relativePath = NormalizePath(basePath) + Path.DirectorySeparatorChar + fileName;

        return true;


    }

    /// <summary>
    /// Normalize the path based on operating system
    /// </summary>
    /// <param name="path"></param>
    /// <param name="preferedSlash">Default slash is the Path.DirectorySeparatorChar</param>
    /// <returns>The path without trailing and start slashes</returns>
    private static string NormalizePath(string path, char? preferedSlash = null)
    {
        preferedSlash ??= Path.DirectorySeparatorChar;

        path = path.Replace('/', preferedSlash.Value);
        path = path.Replace('\\', preferedSlash.Value);
        path = path.Trim(preferedSlash.Value);

        return path;

    }

    private string NormalizeHtmlSrcRelativeUrl(string baseImgPath)
    {
        char slash = '/';

        baseImgPath = NormalizePath(baseImgPath, slash);

        baseImgPath = slash + baseImgPath + slash;

        return baseImgPath;
    }

    /// <summary>
    /// Add the baseImgPath to the md images
    /// <br></br>
    /// E.g.
    /// <code>
    /// ![An image](image.png) => ![An image](/posts/2022/08/23/MyPost/image.png)
    /// </code>
    /// </summary>
    /// <param name="markdownLines"></param>
    /// <param name="baseImgPath"></param>
    /// <returns></returns>
    private string AddBasePathToImgUrlsMarkdown(string[] markdownLines, string baseImgPath)
    {
        StringBuilder md = new();
        baseImgPath = NormalizeHtmlSrcRelativeUrl(baseImgPath);

        foreach (string line in markdownLines)
        {
            if (!line.Contains("!["))
            {
                md.Append(line + Environment.NewLine);
                continue;
            }
            string newLine = line;
            string[] imageNames = line.Split('(', ')');

            for (int i = 0; i < imageNames.Length; i++)
            {
                if (i % 2 == 0)
                {
                    continue;
                }

                if (string.IsNullOrEmpty(imageNames[i]))
                {
                    imageNames[i] = " ";
                }

                newLine = line.Replace(imageNames[i], baseImgPath + imageNames[i]);

            }
            md.Append(newLine + Environment.NewLine);
        }

        return md.ToString();
    }

    /// <summary>
    /// Adds the basePath to the img src <br></br>
    /// E.g. <code> img src="penguin.png" alt="penguin" </code> will be converted to
    /// <code>
    /// "img src="/posts/2022/08/20/Penguin/penguin.png" alt="penguin""
    /// </code>
    /// Also a wrapper div will be added
    /// <br></br>
    /// </summary>
    /// <param name="htmlLines"></param>
    /// <param name="baseImgPath"></param>
    /// <returns></returns>
    private string AddBasePathToImgUrlsHtml(string[] htmlLines, string baseImgPath)
    {
        // todo support video too
        // todo - cleanup :)

        StringBuilder md = new();

        baseImgPath = NormalizeHtmlSrcRelativeUrl(baseImgPath);

        foreach (var line in htmlLines)
        {
            string[] wordsInLine = line.Split(" ");

            int imagesCountInLine = wordsInLine.Count(x => x.Contains("<img"));

            if (imagesCountInLine == 0)
            {
                md.Append(line + Environment.NewLine);
                continue;
            }
            else if (imagesCountInLine > 1)
            {
                _logger.LogWarning("More than one image on a line found. " +
                    "Base Path: {baseImgPath}", baseImgPath);
            }

            string imgClass = "img-std";
            // if a line has more than one img, (which is actually unexpected)
            // by the next iteration we will work with the substring which is after the previous img
            int nextIndex = 0;
            string newLine = string.Empty;
            for (int i = 0; i < imagesCountInLine; i++)
            {

                string startPatern = "<img src=\"";

                string linePart = line.Substring(nextIndex);

                if (linePart.Contains("orientation=\"p\""))
                {
                    imgClass = "img-port";
                }
                else if (linePart.Contains("orientation=\"l\""))
                {
                    imgClass = "img-land";
                }

                int startIndex = linePart.IndexOf(startPatern, StringComparison.InvariantCultureIgnoreCase) + startPatern.Length;

                string stringToReplace = linePart.Substring(startIndex);

                int end = stringToReplace.IndexOf('"');
                string imageName = stringToReplace.Substring(0, end);

                if (string.IsNullOrEmpty(imageName))
                {
                    imageName = " ";
                }

                if (!string.IsNullOrEmpty(newLine))
                {
                    newLine = newLine.Replace(imageName, baseImgPath + imageName);
                }
                else
                {
                    newLine = line.Replace(imageName, baseImgPath + imageName);
                }

                nextIndex = end + 1;
            }

            newLine = newLine.Replace("<p>", string.Empty).Replace("</p>", string.Empty);

            md.Append($"<div class=\"{imgClass}\">");
            newLine = newLine.Replace($"<img", $"<img loading=\"lazy\"");
            md.Append(newLine + Environment.NewLine);
            md.Append("</div>");
        }

        string result = md.ToString();

        return result;
    }

    private string AddBasePathToImgUrlsHtmlWithRegEx(string[] lines, string baseImgPath)
    {
        StringBuilder md = new();
        baseImgPath = NormalizeHtmlSrcRelativeUrl(baseImgPath);

        foreach (var line in lines)
        {
            if (!line.Contains("<img"))
            {
                md.Append(line + Environment.NewLine);
                continue;
            }

            Regex rx = new("src\\s*=\\s*\"(.+?)\"", RegexOptions.IgnoreCase);

            MatchCollection matches = rx.Matches(line);

            string newLine = line;
            foreach (Match match in matches)
            {
                string imageName = " ";
                if (!string.IsNullOrEmpty(match.Value))
                {
                    imageName = match.Value;
                }

                newLine = newLine.Replace(imageName, baseImgPath + imageName);
            }

            newLine = newLine.Replace("<p>", string.Empty).Replace("</p>", string.Empty);
            md.Append("<div class=\"img-md\" >");
            md.Append(newLine + Environment.NewLine);
            md.Append("</div>");
        }

        var result = md.ToString();


        return result;
    }

}
