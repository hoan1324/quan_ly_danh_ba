using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quan_ly_danh_ba.Areas.User.Controllers
{
    public class UserController : Controller
    {
        // GET: User/User
        public ActionResult Profile()
        {
            return View();
        }
        public ActionResult Logout() {
         SessionConfig.SaveUser(null);
        return RedirectToAction("SignUp", "Login", new { area = "" });
        }
    }
}