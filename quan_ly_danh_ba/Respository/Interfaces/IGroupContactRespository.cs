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
        void Insert(GroupContact groupContact);
        void  Delete(GroupContact groupContact);
        void Update(GroupContact groupContact);
        

    }
}
