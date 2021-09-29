using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoardProject.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Author { get; set; }
        public DateTime CreatedTime { get; set; }

        [Required]
        public string Category { get; set; }

        public DateTime ModifiedTime { get; set; }

        public string ModifiedBy { get; set; }

        public int VisitNum { get; set; }
        public IList<CommentModel> Comments { get; set; }
    }
}
