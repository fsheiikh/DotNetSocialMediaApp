using FinalProjectMVC.Context;
using FinalProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectMVC.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Messages/{id:int}")]
        public ActionResult PostMessage(Message message)
        {
            var db = new FinalProjectDbContext();
            var userid = (int)Session["User"]; // user id of the user sending the message
            var postedid = Convert.ToInt32(RouteData.Values["id"]); //user id of the user being messaged

            var msg = new Message { message = message.message, messaged_id = postedid, messager_id = userid, created_at = DateTime.Now}; //coulve added the properties to the message object recived from the form , but this method requried less lines of code
   
            db.Messages.Add(msg);
            db.SaveChanges();


            return RedirectToAction("UserPage", "Home", new { id = postedid });
        }

       

        [HttpPost, ActionName("Delete")]
        [Route("Message/{id:int}")]
        public ActionResult Delete()
        {
            var db = new FinalProjectDbContext();
            var id = Convert.ToInt32(RouteData.Values["id"]); //id of the message

            
            var message = db.Messages.SingleOrDefault(m => m.id == id); //get that message from databse based on the id

            var url = message.Messaged.id; //id of the user whose page we were on (for the redirect)

            var userid = Convert.ToInt32(Session["User"]); //user id of user logged in

            var comments = db.Comments.Where(c => c.message_id == id).ToList(); // get comments of teh message we are deleting
            db.Comments.RemoveRange(comments); //delete those comments before deleteign the message
            //The method of deleting comments before the message is most liekly incorrect. implementing a cascading deletetion based on foreign keys would be better

            db.Messages.Remove(message);
            db.SaveChanges();

            return RedirectToAction("UserPage", "Home", new { id = url });
        }

        
    }
}