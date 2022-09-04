namespace LevaNotes.Web.Services;

public class AppInitializerHostedService : IHostedService
{
    private readonly PostService _postService;

    public AppInitializerHostedService(PostService postService)
    {
        _postService = postService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
