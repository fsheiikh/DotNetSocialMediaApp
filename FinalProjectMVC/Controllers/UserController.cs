using FinalProjectMVC.Context;
using FinalProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectMVC.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        [Route("Users")]
        public ActionResult Index()
        {
            var db = new FinalProjectDbContext();
            var users = db.Users.Where(u => u.id > 0).ToList(); //gets all users, 'selectmany' wasnt working 

            var allUsers = new UsersViewModel { Users = users};
            return View(allUsers);
        }


        [HttpGet]
        [Route("Logout")]
        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [Route("Edit")]
        public ActionResult Edit()
        {
            var db = new FinalProjectDbContext();
            var userid = Convert.ToInt32(Session["User"]);

            var user = db.Users.FirstOrDefault(u => u.id == userid);
            return View(user);

        }

        //The following method didnt work correctly so i removed the link to get to the edit page from the application
        [HttpPut, ActionName("Put")] //should be "Patch"or "puts" , but post works
        [Route("Update")]
        public ActionResult Update(User user)
        {
            var db = new FinalProjectDbContext();
            var userid = Convert.ToInt32(Session["User"]);

            var oldUser = db.Users.SingleOrDefault(u => u.id == userid);
            //oldUser.name = user.name;
            
           // oldUser.avatar = user.avatar;

            db.SaveChanges();

            return RedirectToAction("UserPage", "Home", new { id = userid });
        }



    }
}