using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectMVC.Models
{
    public class UserAndMessages
    {
        public User User { get; set; }

        public List<Message> Messages { get; set; }
        public Message Message { get; set; }

        public List<CommentViewModel> Comments { get; set; }
        public Comment Comment { get; set; }

        public List<Follower> Followers { get; set; }
        public Follower Follow { get; set; }
        public Boolean IsFollowing { get; set; }

        //public CommentViewModel Comments { get; set; }
    }
}