namespace LevaNotes.Web.Middlewares;

public class CustomPostRoutingMiddleware
{
    private readonly RequestDelegate _next;

    public CustomPostRoutingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task InvokeAsync(HttpContext httpContext)
    {
        string? path = httpContext.Request.Path.Value;

        if (path is not null && path.EndsWith("3", StringComparison.InvariantCulture))
        {
            httpContext.Request.Path = "/post/de/cat/slug/3";
        }


        return _next(httpContext);
    }
}
