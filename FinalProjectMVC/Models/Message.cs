using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProjectMVC.Models
{   
    [Table("Messages")]
    public class Message
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string message { get; set; }

        [Required]
        [ForeignKey("Messager")]
        public int messager_id { get; set; }

        [Required]
        [ForeignKey("Messaged")]
        public int messaged_id { get; set; }

        public DateTime created_at { get; set; }

        public virtual User Messager { get; set; }
        public virtual User Messaged { get; set; }

    }
}