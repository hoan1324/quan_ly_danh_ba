using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quan_ly_danh_ba.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
       public ActionResult SignIn()
        {
            return View();
        }
        public ActionResult SignUp() {
        return View();
        }
    }
}