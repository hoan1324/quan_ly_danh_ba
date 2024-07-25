using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quan_ly_danh_ba.Respository.Interfaces
{
     public interface IConnectContact_GroupContactRespository
    {
        GroupContact AddContact(GroupContact groupContact, Contact contact);
        Contact AddGroupContact(Contact contact,GroupContact groupContact);
        GroupContact RemoveContact(GroupContact groupContact, Contact contact);
        Contact RemoveGroupContact(Contact contact, GroupContact groupContact);
        List<Contact> GetAllContacts(GroupContact groupContact);
        List<GroupContact> GetAllGroups(Contact contact);
        void ConnectSave(quan_ly_danh_baEntity db);


    }
}
