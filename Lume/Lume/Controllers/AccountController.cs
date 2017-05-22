using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lume.Models;
using System.Web.Security;

namespace Lume.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserViewModel user)
        {
            if (Membership.ValidateUser(user.Email, user.Password))
            {
                FormsAuthentication.SetAuthCookie(user.Email, user.RememberMe);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return Json(new { error = "Incorrect login or password" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}
