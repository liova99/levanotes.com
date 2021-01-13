using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LK.DB.Models;
using LK.DB.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LK.Pages.Blog
{
    public class IndexModel : PageModel
    {
        private readonly IPostRepo _postRepo;

        public IEnumerable<Post> posts { get; set; }

        public IndexModel(IPostRepo postRepo)
        {
            _postRepo = postRepo;
        }
        public void OnGet()
        {
            posts = _postRepo.GetAllPosts();
        }
    }
}
