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
        [Required]
        public int Id { get; set; }
        [Required]
        public string[] Tags { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public string Slug { get; set; }
    }
}
