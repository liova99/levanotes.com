using LK.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LK.DB.Repos
{
    public class TagRepo
    {
        List<Tag> _tags { get; set; }

        public TagRepo()
        {
            _tags = new List<Tag>()
            {
                new Tag() {Id = 1, Name="C#"},
                new Tag() {Id = 2, Name="ASP.NET"},
            };  
        }
    }
}
