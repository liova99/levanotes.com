using LK.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LK.DB.Repos
{
    public class CategoryRepo : ICategoryRepo
    {
        private List<Category> _categories { get; set; }

        private Category Undefined = new Category { Name = "Undefined" };

        public CategoryRepo()
        {
            _categories = new List<Category>()
            {
                new Category() {Id = 1, Name="ASP.NET"},
            };
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categories;
        }

        public Category GetCategoryById(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            return category ?? Undefined;
        }

        public Category GetCategoryByName(string name)
        {
            var category = _categories.FirstOrDefault(c => c.Name == name);
            return category ?? Undefined;
        }
    }
}
