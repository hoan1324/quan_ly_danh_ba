using Data.Entity;
using quan_ly_danh_ba.Respository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quan_ly_danh_ba.Respository.Implements
{
    public class GroupContactRespository : Quan_ly_danh_baEntity, IGroupContactRespository
    {
        public GroupContact Delete(GroupContact groupContact)
        {
            db.GroupContacts.Remove(groupContact);
            db.SaveChanges();
            return groupContact;
        }

        public GroupContact FindById(Guid id)
        {
            var groupContact = db.GroupContacts.FirstOrDefault(item => item.GroupContactID == id);
            if (groupContact == null)
            {
                return null;
            }
            return groupContact;
        }

        public GroupContact Insert(GroupContact groupContact)
        {
            db.GroupContacts.Add(groupContact);
            db.SaveChanges();
            return groupContact;
        }

        public List<GroupContact> ListGroupContact()
        {
            return db.GroupContacts.ToList();
        }

        public GroupContact Update(GroupContact newGroupContact)
        {
            var groupContact = db.GroupContacts.FirstOrDefault(item => item.GroupContactID == newGroupContact.GroupContactID);
            var newContact = newGroupContact.Contacts.ToList();
            groupContact.GroupName = newGroupContact.GroupName;
            
            foreach (var item in newContact)
            {
                groupContact.Contacts.Add(item);
            }
            db.SaveChanges();
            return groupContact;

        }
    }
}