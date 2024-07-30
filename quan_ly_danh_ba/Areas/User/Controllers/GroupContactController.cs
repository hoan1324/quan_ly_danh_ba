using quan_ly_danh_ba.Services.Implements;
using quan_ly_danh_ba.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quan_ly_danh_ba.Areas.User.Controllers
{
    [RoleUser]
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
        public ActionResult Delete(string[] GroupNames) { 
          var check= _groupContactService.DeleteList(GroupNames.ToList());
            if (check == true) {
                TempData["Message"] = "Xóa Thành công";
                return View();
            }
            TempData["Message"] = "Lỗi hệ thống vui lòng thử lại";

            return View();
        }

    }
}