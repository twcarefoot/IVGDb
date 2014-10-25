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
        public ActionResult Login(User model)
        {
            if (ModelState.IsValid)
            {
                using (ivgdb_Entities db = new ivgdb_Entities())
                {
                    string username = model.Username;
                    string password = model.Password;

                    bool userValid = db.Users.Any(p => p.Username == username && p.Password == password);

                    if (userValid)
                    {
                        FormsAuthentication.SetAuthCookie(username, false);
                        Session["LoggedIn"] = true;
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return View(model);
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