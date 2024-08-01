using Dtos;
using quan_ly_danh_ba.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quan_ly_danh_ba.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: Login
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(UserDto user)
        {
            var done = _userService.FindByUser(user);
            if (done != null)
            {
                SessionConfig.SaveUser(done);
                return RedirectToAction("Index", "Home", new { area = "User" });
            }
            
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(UserDto user)
        {
            var done = _userService.FindByUser(user);
            if (done != null)
            {
                return View();

            }
            _userService.Insert(user);
            return RedirectToAction("SignIn");
        }
    }
}