using LevaNotes.Web.Models;
using LevaNotes.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LevaNotes.Web.Pages;

public class IndexModel : PageModel
{
    private readonly PostService _postService;

    public IndexModel(PostService postService)
    {
        _postService = postService;
    }

    public IReadOnlySet<Post> Posts { get; set; } = null!;

    public void OnGet()
    {
        IReadOnlySet<Post> posts = _postService.GetPosts(offset: 0);

        Posts = posts;
    }
    
}
