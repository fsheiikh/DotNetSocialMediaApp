using FinalProjectMVC.Context;
using FinalProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectMVC.Controllers
{
    public class LikeController : Controller
    {
        // GET: Like
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Like/{id:int}")]
        public ActionResult Like()
        {
            var commentid = Convert.ToInt32(RouteData.Values["id"]); 
            var userid = Convert.ToInt32(Session["User"]);

            var db = new FinalProjectDbContext();
            var comment = db.Comments.FirstOrDefault(c => c.id == commentid);
            var like = new Like { user_id = userid, comment_id = commentid};

            db.Likes.Add(like);
            db.SaveChanges();

            return RedirectToAction("UserPage", "Home", new { id = comment.Message.Messaged.id });

        }


    }
}