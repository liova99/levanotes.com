using LevaNotes.Web.Extensions;
using LevaNotes.Web.Models;
using LevaNotes.Web.Services;
using Westwind.AspNetCore.Markdown;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddRazorPages();

services.AddRouting(options => {
    
    options.LowercaseUrls = true;
});

services.AddMarkdown();

services.AddSingleton<PostService>();
services.AddHostedService<AppInitializerHostedService>();

services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseCustomRouting();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
