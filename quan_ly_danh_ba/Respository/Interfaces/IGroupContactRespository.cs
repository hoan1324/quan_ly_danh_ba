using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quan_ly_danh_ba.Respository.Interfaces
{
    public interface IGroupContactRespository
    {
        List<GroupContact> ListGroupContact();
        GroupContact FindByName(string name);
        GroupContact Insert(GroupContact groupContact,User user=null);
        GroupContact  Delete(GroupContact groupContact);
        GroupContact Update(GroupContact groupContact);
        

    }
}
