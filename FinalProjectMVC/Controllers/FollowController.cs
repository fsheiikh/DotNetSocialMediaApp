using FinalProjectMVC.Context;
using FinalProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectMVC.Controllers
{
    public class FollowController : Controller
    {
        // GET: Follow
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Follow")]
        public ActionResult Follow(Follower follow)
        {
            var db = new FinalProjectDbContext();
            //var userid = Convert.ToInt32(Session["User"]);
            follow.created_at = DateTime.Now;
            db.Followers.Add(follow);
            db.SaveChanges();

            return RedirectToAction("UserPage", "Home", new { id = follow.followed_id });

        }

        [HttpPost, ActionName("Delete")]
        [Route("Unfollow/{id:int}")]
        public ActionResult Delete()
        {
            var db = new FinalProjectDbContext();
            //var userid = Convert.ToInt32(Session["User"]);

            var id = Convert.ToInt32(RouteData.Values["id"]);
            var follow = db.Followers.FirstOrDefault(f => f.id == id);

            db.Followers.Remove(follow);
            db.SaveChanges();

            return RedirectToAction("UserPage", "Home", new { id = follow.followed_id});

        }

   
        [HttpGet]
        [Route("Followers")]
        public ActionResult Followers()
        {
            var db = new FinalProjectDbContext();
            var userid = Convert.ToInt32(Session["User"]);
            var followers = db.Followers.Where(f => f.followed_id == userid).Distinct().ToList();

            var allFollowers = new FollowerViewModel { Follow = followers };

            return View(allFollowers);
        }

        [HttpGet]
        [Route("Following")]
        public ActionResult Following()
        {
            var db = new FinalProjectDbContext();
            var userid = Convert.ToInt32(Session["User"]);
            var following = db.Followers.Where(f => f.follower_id == userid).Distinct().ToList();

            var allFollowing = new FollowerViewModel { Follow = following };

            return View(allFollowing);
        }
    }
}