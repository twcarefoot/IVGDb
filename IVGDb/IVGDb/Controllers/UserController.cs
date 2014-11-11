using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IVGDb.Models;
using System.Web.Security;

namespace IVGDb.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                if (UserViewModel.VerifyPassword(username, password))
                {
                    Session["loggedIn"] = "true";
                    Session["username"] = username;
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                if(user.Password.Equals(user.ConfirmPassword))
                {
                    string hash = UserViewModel.HashPassword(user.Password);
                    user.Password = hash.ToString();
                    UserViewModel.Register(user);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
    }
}