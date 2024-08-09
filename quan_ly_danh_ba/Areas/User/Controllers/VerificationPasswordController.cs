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
        public ActionResult SearchUser()
        {
            return View();
        }
        public ActionResult ChangePass() {
            return View();
         }

    }
}