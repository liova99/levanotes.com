using LK.DB.Models;
using LK.Lib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LK.DB.Repos
{
    public class PostRepo : IPostRepo
    {
        private List<Post> _posts { get; set; }
        private readonly IUrlHelper _urlHelper;
        private readonly ICategoryRepo _categoryRepo;

        public PostRepo(IUrlHelper urlHelper, ICategoryRepo categoryRepo)
        {
            _urlHelper = urlHelper;
            _categoryRepo = categoryRepo;

            _posts = new List<Post>()
            {
                new Post() {
                    Id = 1,
                    Tags= new string[] {"C#", "ASP.NET" },
                    Category=_categoryRepo.GetCategoryById(1),
                    FilePath = "/posts/publish-asp-on-linux.md",
                    Title="Publish an ASP.Net App on a Linux VPS",
                    Slug = _urlHelper.CreateSlug( "publish-a-asp-dotnet-app-on-linux"),
                    ImgPath = "/posts/m.publish-asp-on-linux.assets/linux-penguin.jpg",
                    ImgAlt= "linux penguin ",
                    Language = Post.Languages.EN,
                    Date = new DateTime(2021,02,05)
                },

                //new Post() { Id = 2,
                //    Tags= new string[] {"C#", "ASP.NET" },
                //    Category=_categoryRepo.GetCategoryById(1),
                //    FileName = "~/posts/hard-to-say.md",
                //    Title="My super second Post",
                //    Slug = _urlHelper.CreateSlug( "hard to say"),
                //    ImgPath = "/posts/m.hard-to-say.assets/all-you-need-is-coffee.jpg",
                //    ImgAlt= "alt of an image",
                //    Language = Post.Languages.EN
                //},
                
                    

                // new Post() {
                //     Id = 2,
                //     Tags= new string[] {"C#", "ASP.NET" },
                //     Category=_categoryRepo.GetCategoryById(1),
                //     FileName = "~/posts/hard-to-say.md",
                //     Title="My super second Post",
                //     Slug = _urlHelper.CreateSlug( "hard to say"),
                //     ImgPath = "/posts/m.hard-to-say.assets/all-you-need-is-coffee.jpg",
                //     ImgAlt= "alt of an image",
                //     Language = Post.Languages.EN
                // },

                //  new Post() { Id = 2, Tags= new string[] {"C#", "ASP.NET" }, Category=_categoryRepo.GetCategoryById(1),
                //    FileName = "~/posts/hard-to-say.md", Title="My super second Post", Slug = _urlHelper.CreateSlug( "hard to say"),
                //ImgPath = "/posts/m.hard-to-say.assets/all-you-need-is-coffee.jpg", ImgAlt= "alt of an image"},
            };

        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _posts;
        }

        public Post GetPostById(int id)
        {
            return _posts.FirstOrDefault(p => p.Id == id);
        }

        public Post GetPostBySlug(string slug)
        {
            return _posts.FirstOrDefault(p => p.Slug == slug);
        }

    }
}
