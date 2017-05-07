using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectMVC.Models
{
    public class CommentViewModel
    {
        public Comment Comment { get; set; }

        public List<Like> Likes { get; set; }

        public int LikesCount { get; set; }
    }
}