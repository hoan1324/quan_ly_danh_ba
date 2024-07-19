using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.map
{
    public class mapContact:Quan_ly_danh_baEntity
    {
        //list
        public List<Contact> listContact()
        {
            return db.Contacts.ToList();
        }
        //tìm kiếm theo id
        //thêm
        //sửa
        //xóa
    }
}
