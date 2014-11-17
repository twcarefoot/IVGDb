using IVGDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IVGDb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            UserViewModel user = (UserViewModel)TempData["user"];
            if (user != null)
            {
                ViewBag.LoggedIn = true;
                return View(user);
            }            

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}