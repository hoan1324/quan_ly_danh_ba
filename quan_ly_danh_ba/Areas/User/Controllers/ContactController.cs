using Data.Entity;

using Dtos;
using Newtonsoft.Json;
using PagedList;
using quan_ly_danh_ba.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quan_ly_danh_ba.Areas.User.Controllers
{
    [RoleUser]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IGroupContactService _groupContactService;
        public ContactController(IContactService contactService, IGroupContactService groupContactService)
        {
            _contactService = contactService;
            _groupContactService = groupContactService;
        }
        // GET: User/Contact
        public ActionResult Index(int? page,int? pageSize,string FullName,string PhoneNumber, string groupContact)
        {
            page = page ?? 1;
            pageSize = pageSize ?? 10;
            return View(_contactService.Search(FullName,PhoneNumber,groupContact).ToPagedList((int)page,(int)pageSize));
        }
        [HttpGet]
        public JsonResult DataJson(string FullName, string PhoneNumber, string groupContact)
        {
            var data = _contactService.Search(FullName, PhoneNumber, groupContact);

            return Json(data,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Detail(Guid id)
        {
            var user= SessionConfig.GetUser();
            return View(_contactService.FindById(id));
        }
        public ActionResult Create()
        {
            var danhsach=_groupContactService.ListGroupContact();
            return View(danhsach);
        }
        [HttpPost]
        public ActionResult Create(ContactCreateDto contactDto, string newGroupName) {

            var position =_contactService.Insert(contactDto,  newGroupName);
            if (position != null)
            {
                TempData["Message"] = "Tạo mới thành công!";
				return RedirectToAction("Index");
            }
			TempData["SuccessError"] = "Tạo mới thất bại!";
			return View(_groupContactService.ListGroupContact());
        }
       
        public ActionResult Edit(Guid id) {
            if (id == null)
            {
                TempData["Message"] = "Lỗi hệ thống vui lòng thử lại";
				return RedirectToAction("Index");

			}
			var model = Tuple.Create(_contactService.FindById(id), _groupContactService.ListGroupContact());
			return View(model);
		}
		[HttpPost]
		public ActionResult Edit(ContactCreateDto contactDto,  string newGroupName)
		{

			var position = _contactService.Update(contactDto, newGroupName);
			if (position != null)
			{
                TempData["Message"] = "Sửa thành công!";
				return RedirectToAction("Index");
			}
			TempData["SuccessError"] = "Sửa thất bại!";
			return View(Tuple.Create(_contactService.FindById(contactDto.ContactID), _groupContactService.ListGroupContact()));
		}
		public ActionResult Delete(Guid id) {
        var position=_contactService.Delete(id);
            if (position != null) {
                TempData["Message"] = "Xóa thành công!";
				return RedirectToAction("Index");
            }
			TempData["Message"] = "Xóa thất bại!";
			return RedirectToAction("Index");
        }

    }
}