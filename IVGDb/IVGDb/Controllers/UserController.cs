using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IVGDb.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;

namespace IVGDb.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("LoginErr", "The information provided does not match an existing user.");
                return RedirectToAction("Index", "Home");
            }

            bool userExists = UserViewModel.UserExists(Username);

            if (userExists)
            {
                bool passwordsMatch = UserViewModel.VerifyPassword(Username, Password);
                if (passwordsMatch)
                {
                    Session["LoggedIn"] = "true";
                    Session["Username"] = Username;
                    Session["ImageURL"] = UserViewModel.GetUserImageURL(Username);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("LoginErr", "The password entered is incorrect.");
                return RedirectToAction("Index", "Home");
            }
            
            ModelState.AddModelError("LoginErr", "The Username entered doesn't exist.");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session.Remove("LoggedIn");
            Session.Remove("Username");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Manage(string username)
        {
            if (username != null)
            {
                User user = UserViewModel.GetUser(username);
                return View(user);
            }

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

        [HttpPost]
        public ActionResult SetUserPic(User user)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Manage");
            }
            if(user.UserProfilePicLink != null)
            {
                UserViewModel.SetProfilePicURL(user);
                return Manage(user.Username);
            }

            return RedirectToAction("Manage");            
        }
    }
}