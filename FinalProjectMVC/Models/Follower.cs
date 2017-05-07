using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProjectMVC.Models
{
    [Table("Followers")]
    public class Follower
    {
        
        [Key]
        public int id { get; set; }

        [Required]
        [ForeignKey("Following")]
        public int follower_id { get; set; }

        [Required]
        [ForeignKey("Followed")]
        public int followed_id { get; set; }

        public DateTime created_at { get; set; }

        public virtual User Following { get; set; }

        public virtual User Followed { get; set; }
    }
}