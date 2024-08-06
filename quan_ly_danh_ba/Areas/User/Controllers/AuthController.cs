using Dtos;
using CommonHelper;
using quan_ly_danh_ba.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quan_ly_danh_ba.Areas.User.Controllers
{
    [RoleUser]
    public class AuthController : Controller
    {

        private readonly IUserService _userService;
        private readonly IContactService _contactService;
        private readonly IGroupContactService _groupContactService;
        public AuthController(IUserService userService, IContactService contactService, IGroupContactService groupContactService)
        {
            _userService = userService;
            _contactService = contactService;
            _groupContactService = groupContactService;
        }
        // GET: User/User
        public ActionResult ProfileUser()
        {
            var model = Tuple.Create(_contactService.ListContact().Count(), _groupContactService.ListGroupContact().Count()-3);

            return View(model);
        }
        [HttpPost]
        public ActionResult ProfileUser(HttpPostedFileBase Avatar, string Type)
        {
            var model = Tuple.Create(_contactService.ListContact().Count(), _groupContactService.ListGroupContact().Count() - 3);
            byte[] avatarData = null;
            if (Avatar != null && Avatar.ContentLength > 0)
            { 
                if(!ImageConst.permittedExtensions.Contains(Avatar.ContentType) )
                {
                    if(! ImageConst.permittedMimeTypes.Contains(Avatar.ContentType)) {
                        ModelState.AddModelError("file", "Chỉ chấp nhận các định dạng ảnh: .jpg, .jpeg, .png, .gif");
                        return View(model);
                    }
                }
                using (var binaryReader = new BinaryReader(Avatar.InputStream))
                {
                    avatarData = binaryReader.ReadBytes(Avatar.ContentLength);
                }
                
            }
            UserDto user = new UserDto
            {
                Avatar = avatarData,
            };

            var done = _userService.Update(user, Type);
            if (done != null)
            {
                SessionConfig.SaveUser(done);
               return View(model);
            }
            return View(model);
        }
        public ActionResult EditProfile()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult EditProfile(UserDto user,string Type)
        {
            
            var done = _userService.Update(user,Type);
            if (done != null)
            {
                SessionConfig.SaveUser(done);
                return RedirectToAction("ProfileUser");
            }
            return View();
        }
        public ActionResult Logout() {
        SessionConfig.SaveUser(null);
        return RedirectToAction("SignIn", "Login", new { area = "" });
        }

    }
}