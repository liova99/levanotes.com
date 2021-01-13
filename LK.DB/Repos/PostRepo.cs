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
                new Post() { Id = 1, Tags= new string[] {"C#", "ASP.NET" }, Category=_categoryRepo.GetCategoryById(1),
                    FileName = "~/posts/post1.md", Title="My super first Post", Slug = _urlHelper.CreateSlug( "This is my first post")},
                
                new Post() { Id = 2, Tags= new string[] {"C#", "ASP.NET" }, Category=_categoryRepo.GetCategoryById(1),
                    FileName = "~/posts/post2.md", Title="My super second Post", Slug = _urlHelper.CreateSlug( "This is my Second post")},

                new Post() { Id = 3, Tags= new string[] {"C#", "ASP.NET" }, Category=_categoryRepo.GetCategoryById(1),
                     FileName = "~/posts/post3.md", Title="My super Third Post", Slug = _urlHelper.CreateSlug( "This is my 3th post")}


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
