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
                PostId = 15878,
                Tags= new string[] {"C#", "ASP.NET" },
                Category= "ASP.NET",
                BasePath = "/posts/2021/02/05/publish-asp-on-linux/",
                FileName = "publish-asp-on-linux.md",
                Title="Publish an ASP.Net App on a Linux VPS",
                //Slug = "publish-a-asp-dotnet-app-on-linux",
                ImgName = "linux-penguin.jpg",
                ImgAlt= "linux penguin ",
                Language = Languages.EN,
                Date = new DateTimeOffset(2021,02,05,0,0,0, new TimeSpan(1,0,0))
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
        IReadOnlySet<Post> posts = _posts.Skip(offset).Take(max).ToHashSet();
        
        return posts;
    }

    public string CreateRelativePostUrl(Post post)
    {
        string postUrl = $"/post/{post.Language}/{post.Category}/{post.Title}-{post.PostId}";
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