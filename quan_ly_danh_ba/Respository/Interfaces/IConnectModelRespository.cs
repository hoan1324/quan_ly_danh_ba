using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quan_ly_danh_ba.Respository.Interfaces
{
     public interface IConnectModelRespository
    {
        GroupContact AddContact(GroupContact groupContact, Contact contact);
        void AddContact(User user, Contact contact);
        Contact AddGroupContact(Contact contact,GroupContact groupContact);
        void AddGroupContact(User user, GroupContact groupContact);
        GroupContact RemoveContact(GroupContact groupContact, Contact contact);
        Contact RemoveGroupContact(Contact contact, GroupContact groupContact);
        List<Contact> GetAllContacts(GroupContact groupContact);
        List<GroupContact> GetAllGroups(Contact contact);
        void ConnectSave();


    }
}
