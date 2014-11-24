using IVGDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IVGDb.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Update(string username, int role)
        {
            if (username != null && role != null)
            {
                RoleModel.Update(username, role);
                return RedirectToAction("Manage", "User", new { username = username });
            }

            return RedirectToAction("Manage", "User");
        }
    }
}