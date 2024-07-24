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
        public void Delete(GroupContact groupContact)
        {
            db.GroupContacts.Remove(groupContact);
            db.SaveChanges();
        }

        public GroupContact FindByName(string name)
        {
            var groupContact = db.GroupContacts.FirstOrDefault(item => item.GroupName.Equals(name,StringComparison.OrdinalIgnoreCase));
            if (groupContact == null)
            {
                return null;
            }
            return groupContact;
        }

        public void Insert(GroupContact groupContact)
        {
            db.GroupContacts.Add(groupContact);
            db.SaveChanges();

        }

        public List<GroupContact> ListGroupContact()
        {
            return db.GroupContacts.ToList();
        }

        public void Update(GroupContact newGroupContact)
        {
            var groupContact = db.GroupContacts.FirstOrDefault(item => item.GroupContactID == newGroupContact.GroupContactID);
            groupContact.GroupName = newGroupContact.GroupName;
           
            db.SaveChanges();
            

        }
    }
}