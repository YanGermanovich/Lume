using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lume.Models;
using System.Web.Security;
using BLL.Services_Interface;
using BLL.Entities;
using Lume.Providers;

namespace Lume.Controllers
{
    public class AccountController : Controller
    {
        IService<BllUser> _userService;
        IService<BllCity> _cityService;
        IService<BllCountry> _countryService;

        public AccountController(IService<BllUser> userService, IService<BllCity> cityService, IService<BllCountry> countryService)
        {
            _userService = userService;
            _cityService = cityService;
            _countryService = countryService;
        }

        public JsonResult GetCities()
        {
            var allCities = _cityService.GetAllEntities().ToList();
            var allCountries = _countryService.GetAllEntities().ToList();
            var cities = allCities.Select(c => new
            {
                name = c.Name,
                Id = c.Id,
                idCountry = c.Id_Country
            });
            var countries = allCountries.Select(c => new
            {
                name = c.Name,
                Id = c.Id
            });

            return Json(new { cities = cities, countries = countries }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserViewModel user)
        {
            if (Membership.ValidateUser(user.Email, user.Password))
            {
                FormsAuthentication.SetAuthCookie(_userService.GetFirstByPredicate(u => u.Email == user.Email).UserName, user.RememberMe);
                return Json(new { success = "Logged in successfully", Url = Url.Action("Index", "Home") }, JsonRequestBehavior.AllowGet);

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

        [HttpPost]
        public ActionResult Register(UserViewModel model)
        {
            var anyUser = _userService.GetAllEntities().Any(u => u.Email.Contains(model.Email));

            if (anyUser)
            {
                return Json(new { error = "User with this login already exists" }, JsonRequestBehavior.AllowGet);
            }

            var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                .CreateUser(model);

            if (membershipUser != null)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return Json(new { success = "Register in successfully", Url = Url.Action("Index", "Home") }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { error = "Error occurred during registration(" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return Json(new { Url = Url.Action("Index", "Home") }, JsonRequestBehavior.AllowGet);

        }
    }
}
