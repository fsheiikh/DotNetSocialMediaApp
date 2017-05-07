using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectMVC.Models
{
    public class FeedViewModel
    {
        public List<Message> Messages { get; set; }
        public List<Comment> Comments { get; set; }

    }
}