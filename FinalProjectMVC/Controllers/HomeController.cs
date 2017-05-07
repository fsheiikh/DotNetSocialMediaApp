
using FinalProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using FinalProjectMVC.Context;

namespace FinalProjectMVC.Controllers
{
    [RoutePrefix("")]
    public class HomeController : Controller
    {   
        

        [Route("")]
        public ActionResult Index()
        {
            ViewBag.Errors = "";
            return View();
        }


        [Route("Register")]
        public ActionResult Register() //This method and the previous return the registration view page
        {
            return View("Index");
        }

        [HttpPost]
        [Route("Register")]
        public ActionResult Register(User user)
        {
            var db = new FinalProjectDbContext();

            if (ModelState.IsValid)
            {
                //
               // var Hasher = new PasswordHasher<User>();
                //user.password = Hasher.HashPassword(user, user.password);

                //TryValidateModel(user);

                //var user1 = new User { name = user.name, email = user.email, password = user.password };

                //
                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("LoginPage", "Home");
            }

            ViewBag.Errors = ModelState.Values;

            return View("Index");
        }

        
        [Route("Validate")]
        public JsonResult IsEmailExists(string email) //remote validation on client side (stackoverflow)
        {
            var db = new FinalProjectDbContext();
            return Json(!db.Users.Any(x => x.email == email), JsonRequestBehavior.AllowGet);
        }


        [Route("Login")]
        public ActionResult LoginPage()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(string email, string password)
        {
            var db = new FinalProjectDbContext();


            if (db.Users.Any(u => u.email == email))
            {

                var user = db.Users.FirstOrDefault(u => u.email == email);

                if (user.password == password)
                {
                    TempData["Login"] = String.Format("Successfully Logged in as {0}", user.name);
                    Session["User"] = user.id;
                    return RedirectToAction("UserPage", new { id = user.id});
                }
                else //if password entered is incorrect
                {
                    TempData["Login"] = "Invalid Credentials";
                    return View("LoginPage");
                }

            }
            //if email does not exists
            TempData["Login"] = "Invalid Credentials";
            return View("LoginPage");
        }

       


        [HttpGet]
        [Route("User/{id}")]
        public ActionResult UserPage(int id)
        {
            if (Session["User"] == null) //ensures that someone must be logged in to view this page
            {
                TempData["Login"] = "Must be logged in to view that page";
                return View("LoginPage");
            }
            var db = new FinalProjectDbContext();
            var userid = Convert.ToInt32(Session["User"]);

            var user = db.Users.FirstOrDefault(u => u.id == id); //gets user object based on url id
            var messages = db.Messages.Where(m => m.messaged_id == id); //gets all messages sent to this user
            var comments = db.Comments.Where(c => c.id > 0); //not a good way to get "all"

            var isfollow = db.Followers.Any(f => (f.followed_id == id) && (f.follower_id == userid)); //gets true or false based on whether the user logged in folows the user whose page we are on
            var follow = db.Followers.FirstOrDefault(f => (f.followed_id == id) && (f.follower_id == userid)); 

            var commentWithLikes = new List<CommentViewModel>(); //list of each comment with its likes

           

            foreach (var comment in comments)
            {
                var likes = db.Likes.Where(l => l.comment_id == comment.id).ToList();

                var likesCount = db.Likes.Where(l => l.comment_id == comment.id).Count(); //count of likes per comment

                var newCommentWithLikes = new CommentViewModel { Comment = comment, Likes = likes, LikesCount = likesCount }; //sets up each comment with its likes in a new object 
                commentWithLikes.Add(newCommentWithLikes); //adding each comment with its likes to the list we initialized

            }


            var viewUser = new UserAndMessages { User = user, Messages = messages.ToList(), Comments = commentWithLikes, Follow = follow, IsFollowing = isfollow}; //sends everything we need to the user page
            return View("User", viewUser);
        }       
    }
}