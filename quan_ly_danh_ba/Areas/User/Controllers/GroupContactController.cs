using quan_ly_danh_ba.Services.Implements;
using quan_ly_danh_ba.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quan_ly_danh_ba.Areas.User.Controllers
{
    public class GroupContactController : Controller
    {
        private readonly IGroupContactService _groupContactService;

        public GroupContactController(IGroupContactService groupContactService)
        {

            _groupContactService = groupContactService;
        }
        // GET: User/GroupContact
        public ActionResult Index()
        {
            return View(_groupContactService.ListGroupContact());
        }

    }
}