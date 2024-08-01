using Data.Entity;
using quan_ly_danh_ba.Respository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quan_ly_danh_ba.Respository.Implements
{
    public class ContactRespository :  IContactRespository
    {
        public Contact Delete(Contact contact)
        {
            var position = Quan_ly_danh_baEntity.db.Contacts.FirstOrDefault(item => item.ContactID == contact.ContactID && item.UserID == SessionConfig.GetUser().UserID);
            if (position != null)
            {
                Quan_ly_danh_baEntity.db.Contacts.Remove(position);
                Quan_ly_danh_baEntity.db.SaveChanges();
                return position;
            }
            return null;
        }

       public Contact FindById(Guid id)
        {
            var contact = Quan_ly_danh_baEntity.db.Contacts.FirstOrDefault(item =>item.UserID==SessionConfig.GetUser().UserID && item.ContactID == id);
            if (contact == null)
            {
                return null;
            }
            return contact;
        }

        public Contact Insert(Contact contact)
        {
            var position = Quan_ly_danh_baEntity.db.Contacts.FirstOrDefault(item => item.UserID == SessionConfig.GetUser().UserID && item.ContactID == contact.ContactID);
            if (position == null)
            {
                Quan_ly_danh_baEntity.db.Contacts.Add(contact);
                Quan_ly_danh_baEntity.db.SaveChanges();
                return contact;
            }
            return null;
        }

        public List<Contact> ListContact()
        {
            return Quan_ly_danh_baEntity.db.Contacts.Where(item=> item.UserID == SessionConfig.GetUser().UserID ).ToList();
        }

        public List<Contact> ListContactSearch(List<Contact> search )
        {
           return search.ToList();
        }

        public Contact Update(Contact contact)
        {
            var position = Quan_ly_danh_baEntity.db.Contacts.FirstOrDefault(item => item.ContactID == contact.ContactID);
           if(position != null)
            {

                position.FullName = contact.FullName;
                position.PhoneNumber = contact.PhoneNumber;
                position.Address = contact.Address;
                position.Email = contact.Email;

                Quan_ly_danh_baEntity.db.SaveChanges();
                return position;
            }
           return null;

        }
    }
}