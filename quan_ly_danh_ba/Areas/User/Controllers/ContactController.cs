using Data.Entity;
using Data.mapdata.contactData;
using Data.mapdata.ContactData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quan_ly_danh_ba.Areas.User.Controllers
{
    public class ContactController : Controller
    {
        // GET: User/Contact
        public ActionResult Index()
        {
            var danhsach = new mapContact().listContacts().ToList();
            return View(danhsach);
        }
        public ActionResult Create()
        {
            var danhsach=new mapGroupContact().listGroupContacts().ToList();
            return View(danhsach);
        }
        [HttpPost]
        public ActionResult Create(Contact contact,string[] GroupName, string newGroupName) {

            var position = new mapContact().insertContact(contact, GroupName.ToList(),newGroupName);
            if (position != null)
            {
				TempData["Message"] = "Tạo mới thành công!";
				return RedirectToAction("Index");
            }
			TempData["SuccessError"] = "Tạo mới thất bại!";
			return View(new mapGroupContact().listGroupContacts().ToList());
        }
        public ActionResult Delete(Guid id) {
        var position=new mapContact().deleteContact(id);
            if (position != null) {
				TempData["Message"] = "Xóa thành công!";
				return RedirectToAction("Index");
            }
			TempData["Message"] = "Xóa thất bại!";
			return RedirectToAction("Index");
        }
    }
}