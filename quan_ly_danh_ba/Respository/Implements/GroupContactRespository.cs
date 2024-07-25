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
            var position = db.GroupContacts.FirstOrDefault(item => item.GroupContactID == groupContact.GroupContactID);
if(position != null)
            {
                db.GroupContacts.Remove(groupContact);
                db.SaveChanges();
                return position;
            }
return null;
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

        public GroupContact Insert(GroupContact groupContact)
        {
            var position = db.GroupContacts.FirstOrDefault(item => item.GroupContactID == groupContact.GroupContactID);
            if (position != null)
            {
                db.GroupContacts.Add(groupContact);
                db.SaveChanges();
                return groupContact;
            }
            return null;
            
        }

        public List<GroupContact> ListGroupContact()
        {
            return db.GroupContacts.ToList();
        }

        public GroupContact Update(GroupContact groupContact)
        {
            var position = db.GroupContacts.FirstOrDefault(item => item.GroupContactID == groupContact.GroupContactID);
            if (position != null)
            {
               position.GroupName=groupContact.GroupName;
                db.SaveChanges();
                return position;
            }
            return null;


        }
    }
}