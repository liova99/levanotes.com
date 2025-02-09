using System.Collections.Generic;
using LevaNotes.Web.Interfaces;
using LevaNotes.Web.Models;

namespace LevaNotes.Web.Services;

public class PostService
{
    public PostService()
    {
        _posts = LoadPosts();
    }

    private HashSet<Post> _posts;
  
    private static HashSet<Post> LoadPosts()
    {
        return new HashSet<Post>()
        {
            new Post() {
                PostId = 15877,
                Tags= new string[] { "Philosophy", "Random" },
                Category= "Philosophy",
                BasePath = "/posts/2017/02/15/truth/",
                UrlTitle="truth",
                FileName = "truth.md",
                Title="Η Αλήθεια",
                //Slug = "publish-a-asp-dotnet-app-on-linux",
                ImgName = "randy-jacob-standing-man-reflection.jpg",
                ImgAlt= "standing man reflection ",
                Language = Languages.GR,
                CreateDate = new DateTimeOffset(2017,02,15,0,0,0, new TimeSpan(1,0,0))
            },
            new Post() {
                PostId = 15878,
                Tags= new string[] {"C#", "ASP.NET" },
                Category= "ASP.NET",
                BasePath = "/posts/2021/02/05/publish-asp-on-linux/",
                UrlTitle="Publish an ASP.Net App on a Linux VPS",
                FileName = "publish-asp-on-linux.md",
                Title="Publish an ASP.Net App on a Linux VPS",
                //Slug = "publish-a-asp-dotnet-app-on-linux",
                ImgName = "linux-penguin.jpg",
                ImgAlt= "linux penguin ",
                Language = Languages.EN,
                CreateDate = new DateTimeOffset(2021,02,05,0,0,0, new TimeSpan(1,0,0))
            },
            new Post() {
                PostId = 15879,
                Tags= new string[] {"MS-Office", "Outlook" },
                Category= "MS-Office",
                BasePath = "/posts/2022/09/24/run-outlook-rule-once/",
                UrlTitle="How To run Outlook rules once",
                FileName = "run-outlook-rule-once.md",
                Title="How To run Outlook rules once",
                //Slug = "publish-a-asp-dotnet-app-on-linux",
                ImgName = "logo-outlook-300.webp",
                ImgAlt= "logo-outlook.webp ",
                Language = Languages.EN,
                CreateDate = new DateTimeOffset(2022,09,24,0,0,0, new TimeSpan(1,0,0))
            },
            new Post() {
                PostId = 15880,
                Tags= new string[] {"Productivity", "Fun" },
                Category= "Productivity",
                BasePath = "/posts/2022/11/06/pomodoro-is-not-productive/",
                UrlTitle="Pomodoro technique is a concurrency of unproductiveness",
                FileName = "pomodoro-is-not-productive.md",
                Title="Pomodoro technique is a concurrency of unproductiveness",
                //Slug = "publish-a-asp-dotnet-app-on-linux",
                ImgName = "henry-co-spiral.jpg",
                ImgAlt= "spiral stares",
                Language = Languages.EN,
                CreateDate = new DateTimeOffset(2022,11,06,0,0,0, new TimeSpan(1,0,0))
            },
            new Post() {
                PostId = 15881,
                Tags= new string[] { "Philosophy", "Random" },
                Category= "Philosophy",
                BasePath = "/posts/2023/08/27/paradeisos/",
                UrlTitle="Paradeisos",
                FileName = "paradeisos.md",
                Title="Παράδεισος",
                //Slug = "publish-a-asp-dotnet-app-on-linux",
                ImgName = "timo-volz-sunset_beach.jpg",
                ImgAlt= "Sunset on the beach",
                Language = Languages.GR,
                CreateDate = new DateTimeOffset(2023,08,27,14,0,0, new TimeSpan(1,0,0))
            },
            new Post() {
                PostId = 15882,
                Tags= new string[] { "Travel", "USA", },
                Category= "Travel",
                BasePath = "/posts/2025/01/25/trip-to-usa/",
                UrlTitle="Trip-To-USA-2025",
                FileName = "trip-to-usa-jan-2025.md",
                Title="Trip to USA Januar 2025",
                //Slug = "publish-a-asp-dotnet-app-on-linux",
                ImgName = "timo-volz-sunset_beach.jpg",
                ImgAlt= "Sunset on the beach",
                Language = Languages.GR,
                CreateDate = new DateTimeOffset(2025,01,25,14,0,0, new TimeSpan(1,0,0))
            },

        //    new Post() {
        //        PostId = 15879,
        //        Tags= new string[] {"C#", "ASP.NET" },
        //        Category= "ASP.NET",
        //        BasePath = "/posts/2022/08/20/testPost/",
        //        FileName = "test.md",
        //        Title="My super second Post",
        //        //Slug = "hard to say",
        //        ImgName = "name.jpg",
        //        ImgAlt= "alt of an image",
        //        Language = Languages.EN,
        //        Date = new DateTimeOffset(2021,02,05,0,0,0, new TimeSpan(1,0,0))                },
            
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public IReadOnlySet<Post> GetPosts(int offset, int max = 10)
    {
        HashSet<int> idToRedirect = new() { 15878, 15879, 15880 };

        IReadOnlySet<Post> posts = _posts.Skip(offset).Take(max)
            .Where(x => !idToRedirect.Contains(x.PostId))
            .OrderByDescending(x => x.CreateDate)
            .ToHashSet();
        
        return posts;
    }

    public string CreateRelativePostUrl(Post post)
    {
        string postUrl = $"/post/{post.Language}/{post.Category}/{post.UrlTitle}-{post.PostId}";
        postUrl = postUrl.Replace(' ', '-').ToLowerInvariant();

        return postUrl;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="postId"></param>
    /// <returns>The Post or null if the post wasn't found</returns>
    public Task<Post?> GetPostByIdAsync(int postId)
    {
        Post? post = _posts.SingleOrDefault(x => x.PostId == postId);

        return Task.FromResult(post);

    }

    /// <summary>
    /// every slug ends with the id of the post
    /// eg. postname-1455
    /// </summary>
    /// <param name="slug"></param>
    /// <returns>The Post or null if the post wasn't found</returns>
    public async Task<Post?> GetPostBySlugAsync(string slug)
    {
        try
        {
            int postId = GetIdFromSlug(slug);

            Post? post = await GetPostByIdAsync(postId);

            return post;
        }
        catch (Exception)
        {
            return null;
        }

    }

    private int GetIdFromSlug(string slug)
    {
        int startIndexOfId = slug.LastIndexOf('-') + 1;

        string identifier = slug.Substring(startIndexOfId);

        if(!int.TryParse(identifier, out int postId))
        {
            throw new ArgumentException($"Given Slug {slug} doesn't contains an Id", nameof(slug));
        }

        return postId;
    }
}
