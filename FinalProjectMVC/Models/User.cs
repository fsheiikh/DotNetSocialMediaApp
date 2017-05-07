using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectMVC.Models
{
    [Table("Users")]
    public class User
    {
        
            [Key]
            public int id { get; set; }

            [Required]
            public string name { get; set; }

            [Required]
            [EmailAddress]
            [Remote("IsEmailExists", "Home", ErrorMessage = "Email already in use")]
            public string email { get; set; }
            
            public string avatar { get; set; }

            [Required]
            public string password { get; set; }

            [Required]
            [NotMapped]
            [System.Web.Mvc.Compare("password", ErrorMessage = "Confirm password doesn't match, Type again !")]
            public string confirm_password { get; set; }

            //public DateTime created_at { get; set; }



    }
}