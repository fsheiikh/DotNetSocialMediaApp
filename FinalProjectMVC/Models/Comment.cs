using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProjectMVC.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string comment { get; set; }

        [Required]
        [ForeignKey("Message")]
        public int message_id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int user_id { get; set; }

        public DateTime created_at { get; set; }

        public virtual User User { get; set; }
        public virtual Message Message { get; set; }

    }
}