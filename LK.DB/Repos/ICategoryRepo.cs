using LK.DB.Models;
using System.Collections.Generic;

namespace LK.DB.Repos
{
    public interface ICategoryRepo
    {
        IEnumerable<Category> GetAllCategories();
        string GetCategoryById(int id);
        Category GetCategoryByName(string name);
    }
}