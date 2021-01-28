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
                new Post() { Id = 1, Tags= new string[] {"C#", "ASP.NET", "LK" }, Category=_categoryRepo.GetCategoryById(1),
                    FileName = "~/posts/how-to-do-it.md", Title="How to do it", Slug = _urlHelper.CreateSlug( "how-to-do-it"), 
                    ImgPath = "/posts/m.how-to-do-it.assets/pexels-markus-spiske-450.jpg",ImgAlt= "alt of an image" },
                
                new Post() { Id = 2, Tags= new string[] {"C#", "ASP.NET" }, Category=_categoryRepo.GetCategoryById(1),
                    FileName = "~/posts/hard-to-say.md", Title="My super second Post", Slug = _urlHelper.CreateSlug( "hard to say"),
                ImgPath = "/posts/m.hard-to-say.assets/all-you-need-is-coffee.jpg", ImgAlt= "alt of an image"},

                 new Post() { Id = 2, Tags= new string[] {"C#", "ASP.NET" }, Category=_categoryRepo.GetCategoryById(1),
                    FileName = "~/posts/hard-to-say.md", Title="My super second Post", Slug = _urlHelper.CreateSlug( "hard to say"),
                ImgPath = "/posts/m.hard-to-say.assets/all-you-need-is-coffee.jpg", ImgAlt= "alt of an image"},

                  new Post() { Id = 2, Tags= new string[] {"C#", "ASP.NET" }, Category=_categoryRepo.GetCategoryById(1),
                    FileName = "~/posts/hard-to-say.md", Title="My super second Post", Slug = _urlHelper.CreateSlug( "hard to say"),
                ImgPath = "/posts/m.hard-to-say.assets/all-you-need-is-coffee.jpg", ImgAlt= "alt of an image"},
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
