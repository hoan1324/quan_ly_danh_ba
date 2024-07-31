using Dtos;
using quan_ly_danh_ba.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quan_ly_danh_ba.Areas.User.Controllers
{
    [RoleUser]
    public class AuthController : Controller
    {

        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: User/User
        public ActionResult ProfileUser()
        {
            return View();
        }
        public ActionResult EditProfile()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult EditProfile(UserDto user)
        {
            
            var done = _userService.Update(user,"profile");
            if (done != null)
            {
                SessionConfig.SaveUser(done);
                return RedirectToAction("ProfileUser");
            }
            return View();
        }
        public ActionResult Logout() {
        SessionConfig.SaveUser(null);
        return RedirectToAction("SignUp", "Login", new { area = "" });
        }

    }
}