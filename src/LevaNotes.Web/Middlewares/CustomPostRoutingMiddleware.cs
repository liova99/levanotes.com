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

        return _next(httpContext);
    }
}
