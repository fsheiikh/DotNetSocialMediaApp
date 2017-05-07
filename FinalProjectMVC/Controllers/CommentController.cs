using FinalProjectMVC.Context;
using FinalProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectMVC.Controllers
{
    public class CommentController : Controller
    {
        [HttpPost]
        [Route("Comment")]
        public ActionResult PostComment(Comment comment)
        {
            var db = new FinalProjectDbContext();
            var msg = db.Messages.First(m => m.id == comment.message_id);
            var pageid = msg.messaged_id; //getting ID of the user that was messaged so that the page will remain the same after comment submission

            var userid = (int)Session["User"];
            comment.user_id = userid;
            comment.created_at = DateTime.Now;

            db.Comments.Add(comment);
            db.SaveChanges();

            return RedirectToAction("UserPage", "Home", new { id = pageid });
        }

        [HttpPost, ActionName("Delete")]
        [Route("Comment/{id:int}")]
        public ActionResult DeleteComment()
        {
            var db = new FinalProjectDbContext();
            var id = Convert.ToInt32(RouteData.Values["id"]);
           

            var comment = db.Comments.SingleOrDefault(c => c.id == id);
            var userid = Convert.ToInt32(comment.Message.Messaged.id);

            db.Comments.Remove(comment);
            db.SaveChanges();

            return RedirectToAction("UserPage", "Home", new { id = userid });
        }
    }
}