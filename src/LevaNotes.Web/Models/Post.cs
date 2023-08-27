using LevaNotes.Web.Interfaces;

namespace LevaNotes.Web.Models;

/// <summary>
/// Most of the content is Test/Dev thinks
/// </summary>
public class Post
{

#nullable disable
    public int PostId { get; init; }
    public string[] Tags { get; init; }
    public string BasePath { get; init; }
    public string UrlTitle { get; set; }
    public string FileName { get; init; }
    public string Title { get; init; }
    public string Category { get; init; }
    public string Slug { get; init; }
    public Languages Language { get; init; }
    public DateTimeOffset CreateDate { get; init; }
    public string ImgName { get; init; }
    public string ImgAlt { get; init; }
#nullable enable
    
}
