using BotDetect.Web.Mvc;
using CommonHelper;
using Dtos;
using quan_ly_danh_ba.Services.Implements;
using quan_ly_danh_ba.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace quan_ly_danh_ba.Areas.User.Controllers
{
    public class VerificationPasswordController : Controller
    {
        // GET: User/VerificationPassword
        private readonly IUserService _userService;

        public VerificationPasswordController(IUserService userService)
        {
            _userService=userService;
        }
        public ActionResult SearchUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchUser(UserDto user,string Type)
        {
            var done=_userService.FindByUser(user,Type);
            if (done != null)
            {
                if (Type == "phone")
                {
                    return RedirectToAction("ChangePass", new { id = done.UserID });
                }
                else if (Type == "email")
                {
                    return RedirectToAction("Verification", new { id = done.UserID });
                }
                   //return RedirectToAction("ChangePass", new { id = done.UserID });

            }
            TempData["ErrorMessage"] = "Không tìm thấy tài khoản chứa thông tin trên"; 
 
            return View();
        }
        public ActionResult Verification(Guid id)
        {
            var user = _userService.FindById(id);
            var confirmationCode = EmailHelper.SendEmail(user.Email);
            TempData["confirmationCode"] = confirmationCode;
            return View(user);
        }
        [HttpPost]
        public ActionResult Verification(Guid ID,string VerificationCode)
        {
           var user = _userService.FindById(ID);
            var getCode = TempData["confirmationCode"];
            if (VerificationCode == getCode.ToString())
            {
                return RedirectToAction("ChangePass");
            }
            TempData["ErrorMessage"] = "Mã xác nhận không chính xác";
            return View(user);
        }
        [HttpPost]
        public JsonResult DataVerificationJson(Guid id)
        {
            var user = _userService.FindById(id);
            var confirmationCode = EmailHelper.SendEmail(user.Email);
            TempData["confirmationCode"] = confirmationCode;
            return Json(confirmationCode);
        }
        
        public ActionResult ChangePass(Guid id) {
            var user= _userService.FindById(id);
            if (user != null) { 
            return View(user);
            }
            return View();
         }

        [HttpPost]
        public ActionResult ChangePass(UserDto user,string Type)
        {
            string userInput = HttpContext.Request.Form["CaptchaCode"];

            MvcCaptcha mvcCaptcha = new MvcCaptcha("ExampleCaptcha");

            if (!mvcCaptcha.Validate(userInput))
            {
                ModelState.AddModelError("CaptchaCode", "CaptchaCode sai");
                return View(user);
            }

            var update = _userService.Update(user, Type);

            if (update == null)
            {
                TempData["ErrorMessage"] = "Không đổi được mật khẩu";
                return View(user);
            }

            // Reset Captcha
            MvcCaptcha.ResetCaptcha("ExampleCaptcha");

            // Save user to session if not null
            if (SessionConfig.GetUser() != null)
            {
                SessionConfig.SaveUser(update);
                return RedirectToAction("index", "home", new { area = "user" });
            }

            return RedirectToAction("signin", "login");


        }
    }
}