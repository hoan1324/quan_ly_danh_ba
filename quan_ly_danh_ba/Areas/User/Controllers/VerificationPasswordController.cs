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
            if (done != null) {
                if (Type == "phone")
                {
                    return RedirectToAction("ChangePass", new { id = done.UserID });
                }
                else if(Type =="email")
                {
                    return RedirectToAction("Verification", new { id = done.UserID });
                }
            }
            TempData["ErrorMessage"] = "Không tìm thấy tài khoản chứa thông tin trên"; 
 
            return View();
        }
        public ActionResult Verification(Guid id)
        {
            var user = _userService.FindById(id);
            var confirmationCode = EmailHelper.SendEmail(user.Email);
            var model = Tuple.Create(user, confirmationCode);
            return View(model);
        }
        [HttpPost]
        public JsonResult DataVerificationJson(Guid id)
        {
            var user = _userService.FindById(id);
            var confirmationCode = EmailHelper.SendEmail(user.Email);
           
            
            return Json(confirmationCode);
        }
        public ActionResult ChangePass(Guid id) {
            var user= _userService.FindById(id);
            if (user != null) { 
            return View(user);
            }
            return View();
         }

    }
}