using Data.Entity;
using quan_ly_danh_ba.Respository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quan_ly_danh_ba.Respository.Implements
{
    public class GroupContactRespository :  IGroupContactRespository
    {
        public GroupContact Delete(GroupContact groupContact)
        {
            var position = Quan_ly_danh_baEntity.db.GroupContacts.FirstOrDefault(item => item.GroupContactID == groupContact.GroupContactID);
            if(position != null)
            {
                Quan_ly_danh_baEntity.db.GroupContacts.Remove(groupContact);
                Quan_ly_danh_baEntity.db.SaveChanges();
                return position;
            }
return null;
        }

        public GroupContact FindByName(string name)
        {

            var groupContact = Quan_ly_danh_baEntity.db.GroupContacts.FirstOrDefault(item =>item.UserID==SessionConfig.GetUser().UserID && item.GroupName.Equals(name,StringComparison.OrdinalIgnoreCase));
            if (groupContact == null)
            {
                return null;
            }
            return groupContact;
        }

        public GroupContact Insert(GroupContact groupContact,User user=null)
        {
            var userId = user.UserID != null ? user.UserID : SessionConfig.GetUser().UserID;

            var currentUser = Quan_ly_danh_baEntity.db.Users.FirstOrDefault(item => item.UserID == userId);
                    var position = Quan_ly_danh_baEntity.db.GroupContacts
                        .FirstOrDefault(item => item.GroupContactID == groupContact.GroupContactID && item.UserID == currentUser.UserID);
                    if (position == null)
                    {
                        position.User =currentUser;
                        Quan_ly_danh_baEntity.db.GroupContacts.Add(groupContact);
                        Quan_ly_danh_baEntity.db.SaveChanges();
                        return groupContact;
                    }
                    
            return null;

        }

        public List<GroupContact> ListGroupContact()
        {
            var currentUser = Quan_ly_danh_baEntity.db.Users.FirstOrDefault(item => item.UserID == SessionConfig.GetUser().UserID);

            return Quan_ly_danh_baEntity.db.GroupContacts.Where(item=>item.UserID==currentUser.UserID).ToList();
        }

        public GroupContact Update(GroupContact groupContact)
        {
            var currentUser = Quan_ly_danh_baEntity.db.Users.FirstOrDefault(item => item.UserID == SessionConfig.GetUser().UserID);
            var position = Quan_ly_danh_baEntity.db.GroupContacts.FirstOrDefault(item => item.GroupContactID == groupContact.GroupContactID && item.UserID==currentUser.UserID);
            if (position != null)
            {
               position.GroupName=groupContact.GroupName;
                Quan_ly_danh_baEntity.db.SaveChanges();
                return position;
            }
            return null;


        }
    }
}