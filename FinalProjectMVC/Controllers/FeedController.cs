using FinalProjectMVC.Context;
using FinalProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectMVC.Controllers
{
    public class FeedController : Controller
    {
        [HttpGet]
        [Route("Feed")]
        public ActionResult Index()
        {
            var db = new FinalProjectDbContext();
            var userid = Convert.ToInt32(Session["User"]);

            var follow = db.Followers.Where(f => f.follower_id == userid).ToList();
            var feed = new FeedViewModel();
            feed.Messages = new List<Message>();
            feed.Comments = new List<Comment>();

            foreach (var x in follow)
            {   
                //for both of the following, need to implement ordering and also checking if the messaegs and comments were after the datetime of the follow
                var messages = db.Messages.Where(m => m.messager_id == x.followed_id).ToList(); 
                var comments = db.Comments.Where(c => c.user_id == x.followed_id).ToList(); 
                
                feed.Messages.AddRange(messages);
              
                feed.Comments.AddRange(comments);
                
            }

            return View(feed);
        }


    }
}