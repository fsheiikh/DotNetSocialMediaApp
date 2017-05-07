using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProjectMVC.Models
{   
    [Table("Likes")]
    public class Like
    {
        [Key]
        public int id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int user_id { get; set; }

        [Required]
        [ForeignKey("Comment")]
        public int comment_id { get; set; }

        public virtual User User { get; set; }
        public virtual Comment Comment { get; set; }
    }
}