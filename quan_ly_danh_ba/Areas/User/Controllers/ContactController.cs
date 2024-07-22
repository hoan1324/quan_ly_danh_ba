using Data.Entity;
using Data.mapdata.contactData;
using Data.mapdata.ContactData;
using Newtonsoft.Json;
using PagedList;
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
        public ActionResult Index(int? page,int? pageSize,string FullName,string PhoneNumber, string groupContact)
        {
            page = page ?? 1;
            pageSize = pageSize ?? 10;
            var danhsach = new MapContact();
            return View(danhsach.Search(FullName,PhoneNumber,groupContact).ToPagedList((int)page,(int)pageSize));
        }
        [HttpGet]
        public JsonResult DataJson(string FullName, string PhoneNumber, string groupContact)
        {
            var data = new MapContact().Search(FullName, PhoneNumber, groupContact);

            // Chọn các thuộc tính cần thiết
            var result = data.Select(c => new
            {
                c.ContactID,
                c.FullName,
                c.PhoneNumber,
                GroupContacts = c.GroupContacts.Select(gc => new
                {
                    gc.GroupContactID,
                    gc.GroupName
                })
            }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Detail(Guid id)
        {
            return View(new MapContact().FindById(id));
        }
        public ActionResult Create()
        {
            var danhsach=new mapGroupContact().listGroupContacts().ToList();
            return View(danhsach);
        }
        [HttpPost]
        public ActionResult Create(Contact contact,string[] GroupName, string newGroupName) {

            var position = new MapContact().InsertContact(contact, GroupName.ToList(),newGroupName);
            if (position != null)
            {
				TempData["Message"] = "Tạo mới thành công!";
				return RedirectToAction("Index");
            }
			TempData["SuccessError"] = "Tạo mới thất bại!";
			return View(new mapGroupContact().listGroupContacts().ToList());
        }
        public ActionResult Edit(Guid id) {
            if (id == null)
            {
				TempData["Message"] = "Lỗi hệ thống vui lòng thử lại";
				return RedirectToAction("Index");

			}
			var model = Tuple.Create(new MapContact().FindById(id), new mapGroupContact().listGroupContacts().ToList());
			return View(model);
		}
		[HttpPost]
		public ActionResult Edit(Contact contact, string[] GroupName, string newGroupName)
		{

			var position = new MapContact().UpdateContact(contact, GroupName.ToList(), newGroupName);
			if (position != null)
			{
				TempData["Message"] = "Sửa thành công!";
				return RedirectToAction("Index");
			}
			TempData["SuccessError"] = "Sửa thất bại!";
			return View(Tuple.Create(new MapContact().FindById(position.ContactID), new mapGroupContact().listGroupContacts().ToList()));
		}
		public ActionResult Delete(Guid id) {
        var position=new MapContact().DeleteContact(id);
            if (position != null) {
				TempData["Message"] = "Xóa thành công!";
				return RedirectToAction("Index");
            }
			TempData["Message"] = "Xóa thất bại!";
			return RedirectToAction("Index");
        }

    }
}