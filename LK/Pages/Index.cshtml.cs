using LK.DB.Models;
using LK.DB.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LK.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IPostRepo _postRepo;
        public IEnumerable<Post> Posts { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IPostRepo postRepo)
        {
            _logger = logger;
            _postRepo = postRepo;
        }

        public void OnGet()
        {
            Posts = _postRepo.GetAllPosts();
        }
    }
}
