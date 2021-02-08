using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LK.DB.Models
{
    public class Post
    {
        public enum Languages
        {
            EN,
            GR,
            RU,
            DE
        }

        public int Id { get; set; }
        public string[] Tags { get; set; }
        public string FilePath { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Slug { get; set; }
        public Languages Language { get; set; }
        public DateTime Date { get; set; }
        public string ImgPath { get; set; }
        public string ImgAlt { get; set; }
    }
}
