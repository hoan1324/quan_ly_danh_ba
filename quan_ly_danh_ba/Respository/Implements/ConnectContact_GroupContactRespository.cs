using Data.Entity;
using quan_ly_danh_ba.Respository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quan_ly_danh_ba.Respository.Implements
{
    public class ConnectContact_GroupContactRespository : IConnectContact_GroupContactRespository
    {
        public GroupContact AddContact(GroupContact groupContact, Contact contact)
        {
            groupContact.Contacts.Add(contact);
            return groupContact;
        }

        public Contact AddGroupContact(Contact contact, GroupContact groupContact)
        {
            contact.GroupContacts.Add(groupContact);
            return contact;
        }

        public void ConnectSave(quan_ly_danh_baEntity db)
        {
            db.SaveChanges();
        }

        public List<Contact> GetAllContacts(GroupContact groupContact)
        {
            return groupContact.Contacts.ToList();
        }

        public List<GroupContact> GetAllGroups(Contact contact)
        {
            return contact.GroupContacts.ToList();
        }

        public GroupContact RemoveContact(GroupContact groupContact, Contact contact)
        {
            groupContact.Contacts.Remove(contact);
            return groupContact;
        }

        public Contact RemoveGroupContact(Contact contact, GroupContact groupContact)
        {
            contact.GroupContacts.Remove(groupContact);
            return contact;
        }
    }
}