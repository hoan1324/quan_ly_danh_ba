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
            var groupContact = Quan_ly_danh_baEntity.db.GroupContacts.FirstOrDefault(item => item.GroupName.Equals(name,StringComparison.OrdinalIgnoreCase));
            if (groupContact == null)
            {
                return null;
            }
            return groupContact;
        }

        public GroupContact Insert(GroupContact groupContact)
        {
            using (var transaction = Quan_ly_danh_baEntity.db.Database.BeginTransaction())
            {
                try
                {
                    var position = Quan_ly_danh_baEntity.db.GroupContacts
                        .FirstOrDefault(item => item.GroupContactID == groupContact.GroupContactID);
                    if (position == null)
                    {
                        Quan_ly_danh_baEntity.db.GroupContacts.Add(groupContact);
                        Quan_ly_danh_baEntity.db.SaveChanges();
                        transaction.Commit();
                        return groupContact;
                    }
                    else
                    {
                        transaction.Rollback();
                        // Thêm xử lý khi nhóm liên hệ đã tồn tại
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Bắt và xử lý ngoại lệ
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            return null;

        }

        public List<GroupContact> ListGroupContact()
        {
            return Quan_ly_danh_baEntity.db.GroupContacts.ToList();
        }

        public GroupContact Update(GroupContact groupContact)
        {
            var position = Quan_ly_danh_baEntity.db.GroupContacts.FirstOrDefault(item => item.GroupContactID == groupContact.GroupContactID);
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