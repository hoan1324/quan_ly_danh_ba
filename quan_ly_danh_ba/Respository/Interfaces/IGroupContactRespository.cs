using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quan_ly_danh_ba.Respository.Interfaces
{
    interface IGroupContactRespository
    {
        List<GroupContact> ListGroupContact();
        GroupContact FindById(Guid id);
        GroupContact Insert(GroupContact groupContact);
        GroupContact Delete(GroupContact groupContact);
        GroupContact Update(GroupContact groupContact);
    }
}
