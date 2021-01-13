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
    public class PostModel : PageModel
    {
        private readonly IPostRepo _postRepo;

        public PostModel(IPostRepo postRepo)
        {
            _postRepo = postRepo;
        }

        public Post Post { get; set; }

        public IActionResult OnGet(string slug)
        {
            Post = _postRepo.GetPostBySlug(slug);

            return Post == null ? RedirectToPage("/NotFound") : Page();
        }
    }
}
