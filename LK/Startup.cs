using LK.DB.Repos;
using LK.Lib.Helper;
using Markdig;
using Markdig.Extensions.AutoIdentifiers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Westwind.AspNetCore.Markdown;

namespace LK
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUrlHelper, UrlHelper >();
            services.AddScoped<IPostRepo, PostRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();

            services.AddMarkdown(config =>
            {
                // Create custom MarkdigPipeline 
                // using MarkDig; for extension methods
                // https://github.com/xoofx/markdig
                config.ConfigureMarkdigPipeline = builder =>
                {
                    builder.UseEmphasisExtras(Markdig.Extensions.EmphasisExtras.EmphasisExtraOptions.Default)
                        .UsePipeTables()
                        .UseGridTables()
                        .UseAutoLinks() // URLs are parsed into anchors
                        .UseEmojiAndSmiley(true)
                        .UseListExtras()
                        .UseFigures()
                        .UseTaskLists()
                        .UseCustomContainers()
                        .UseGenericAttributes();
                        //.DisableHtml();   // don't render HTML - encode as text
                };
            });
            services.AddRazorPages();

            services.AddMarkdown();
            // We need to use MVC so we can use a Razor Configuration Template
            services.AddMvc()
                // have to let MVC know we have a controller
                .AddApplicationPart(typeof(MarkdownPageProcessorMiddleware).Assembly);

            services.Configure<RouteOptions>(o =>
            {
                o.LowercaseUrls = true;
                o.LowercaseQueryStrings = true;
                o.AppendTrailingSlash = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseMarkdown();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
