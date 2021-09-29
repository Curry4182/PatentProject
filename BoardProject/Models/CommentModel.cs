using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BoardProject.Models
{
    public class CommentModel
    {
        [Key]
        public int CommentModelId{ get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "UserName too long(30 char limit)")]
        public string Author { get; set; }

        public string Avatar { get; set; }

        [Required]
        [StringLength(300, ErrorMessage ="Comment too long (300 char limit)")]
        public string Content { get; set; }

        //[Required]
        //[StringLength(16,
        //    MinimumLength = 4,
        //    ErrorMessage = "Password should be minimumLength 4 characters and a maximum of 16 characters"
        //)]
        public string Password { get; set; }

        public int LikeNum { get; set; }
        public int PostId { get; set; }
        public Post Post{ get; set; }

        public DateTime CommentDateTime;

        [NotMapped]
        public bool IsEdit { get; set; }
    }
}
