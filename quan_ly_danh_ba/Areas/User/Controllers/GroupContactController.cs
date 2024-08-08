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
            if (GroupNames.ToList().Any(item => item.Equals("Công việc") || item.Equals("Bạn bè") || item.Equals("Gia đình")))
            {
                TempData["ErrorMessage"] = "Không thể xóa các groupContact Công việc,bạn bè,gia đình vì nó là mặc định ";

                return View();
            }
          var check= _groupContactService.DeleteList(GroupNames.ToList());
            if (check == true) {
                TempData["SuccessMessage"] = "Xóa Thành công";
                return View();
            }
            TempData["ErrorMessage"] = "Không thể xóa các groupContact đã có 1 hoặc nhiều liên hệ";

            return View();
        }

    }
}