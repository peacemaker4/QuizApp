using Newtonsoft.Json;
using QuizApp.DB;
using QuizApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizApp.Controllers
{
    public class UserController : Controller
    {
        Context db;
        public UserController()
        {
            db = new Context();
        }
        
        [HttpGet]
        public ActionResult Enter()
        {
            if (Request.Cookies["user"] != null)
            {
                return RedirectToAction("Index", "Test", null);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Enter(User user)
        {
            if (ModelState.IsValid)
            {
                var dbuser = db.Users.Add(user);
                db.SaveChanges();
                var cookie = new HttpCookie("user", JsonConvert.SerializeObject(dbuser));
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.AppendCookie(cookie);
                return RedirectToAction("Index", "Test", null);
            }
            return View();
        }
        [HttpGet]
        public ActionResult Leave()
        {
            if (Request.Cookies["user"] != null)
            {
                Response.Cookies["user"].Expires = DateTime.Now.AddDays(-1);
            }
            return Redirect("Enter");
        }
    }
}