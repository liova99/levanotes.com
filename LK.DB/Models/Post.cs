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
        public int Id { get; set; }
        public string[] Tags { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public Category Category { get; set; }
        public string Slug { get; set; }
        public string ImgPath { get; set; }
        public string ImgAlt { get; set; }
    }
}
