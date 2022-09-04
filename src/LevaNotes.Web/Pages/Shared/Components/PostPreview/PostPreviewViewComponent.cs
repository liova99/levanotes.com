using LevaNotes.Web.Models;
using LevaNotes.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace LevaNotes.Web.Pages.Shared.Components.PostPreview;

public class PostPreviewViewComponent : ViewComponent
{
    private readonly PostService _postService;

    public PostPreviewViewComponent(PostService postService)
    {
        _postService = postService;
    }

    public record PostPreviewDto(Post Post, string RelativeUrl, bool IsFirst);

    public IViewComponentResult Invoke(Post post, bool isFirst) // you can use InvokeAsyncToo
    {
        string relativPath = _postService.CreateRelativePostUrl(post);
        PostPreviewDto result = new(post, relativPath, isFirst);


        return View(result);
    }
}
