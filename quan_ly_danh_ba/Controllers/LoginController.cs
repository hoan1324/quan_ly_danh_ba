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
        public ActionResult SignIn(UserDto user,string Type)
        {
            var done = _userService.FindByUser(user,Type);
            if (done != null)
            {
                SessionConfig.SaveUser(done);
                TempData["SuccessMessage"] = "Đăng nhập thành công";
                return RedirectToAction("Index", "Home", new { area = "User" });
            }
            TempData["ErrorMessage"] = "Tài khoản hoặc mật khẩu không chính xác";
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(UserDto user)
        {
            var done = _userService.FindByUser(user,"email");
            if (done != null)
            {
                TempData["ErrorMessage"] = "Tài khoản đã tồn tại!";
                return View();

            }
            _userService.Insert(user);
            TempData["SuccessMessage"] = "Đăng ký thành công!";
            return RedirectToAction("SignIn");
        }
    }
}