using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IVGDb.Models;

namespace IVGDb.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
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
                if((user.Password == user.ConfirmPassword) && !UserViewModel.MatchingPasswords(user.Email, user.Password))
                {
                    UserViewModel.Register(user);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
    }
}