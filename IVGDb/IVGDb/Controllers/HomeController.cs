using IVGDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IVGDb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            UserViewModel user = new UserViewModel();

            if(TempData["user"] != null)
                user = (UserViewModel)TempData["user"];

            user.TopFiveLatest = VideoGameViewModel.GetFiveNewest();

            return View(user);
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

        public ActionResult Banned()
        {
            FormsAuthentication.SignOut();
            Session.Remove("ImgURL");
            return View();
        }
    }
}