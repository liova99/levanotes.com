using LevaNotes.Web.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Westwind.AspNetCore.Markdown;

namespace LevaNotes.Web.Extensions;

public static class RoutingExtensions
{
    /// <summary>
    /// It handles (for now) the posts written in MD located in wwwroot 
    /// folder and process the request to the Post page with the post parameters.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseCustomRouting(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomPostRoutingMiddleware>();
    }
}
