using LK.DB.Models;
using System.Collections.Generic;

namespace LK.DB.Repos
{
    public interface IPostRepo
    {
        IEnumerable<Post> GetAllPosts();
        Post GetPostById(int id);
        Post GetPostBySlug(string slug);
    }
}