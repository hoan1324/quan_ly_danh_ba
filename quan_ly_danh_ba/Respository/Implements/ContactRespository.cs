﻿using Data.Entity;
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
            var userId = SessionConfig.GetUser().UserID;
            var position = Quan_ly_danh_baEntity.db.Contacts.FirstOrDefault(item => item.ContactID == contact.ContactID && item.UserID == userId);
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
            var userId = SessionConfig.GetUser().UserID;
            var contact = Quan_ly_danh_baEntity.db.Contacts.FirstOrDefault(item =>item.UserID==userId && item.ContactID == id);
            if (contact == null)
            {
                return null;
            }
            return contact;
        }

        public Contact Insert(Contact contact)
        {
            var userId = SessionConfig.GetUser().UserID;
            var currentUser = Quan_ly_danh_baEntity.db.Users.FirstOrDefault(item => item.UserID == userId);

            var position = Quan_ly_danh_baEntity.db.Contacts.FirstOrDefault(item => item.UserID == currentUser.UserID && item.ContactID == contact.ContactID);
            if (position == null)
            {
                contact.User = currentUser;
                Quan_ly_danh_baEntity.db.Contacts.Add(contact);
                Quan_ly_danh_baEntity.db.SaveChanges();
                return contact;
            }
            return null;
        }

        public List<Contact> ListContact()
        {
            var currentUser=SessionConfig.GetUser().UserID;
            return Quan_ly_danh_baEntity.db.Contacts.Where(item=> item.UserID == currentUser).ToList();
        }

        public List<Contact> ListContactSearch(List<Contact> search )
        {
           return search.ToList();
        }

        public Contact Update(Contact contact)
        {
            var userId = SessionConfig.GetUser().UserID;
            var position = Quan_ly_danh_baEntity.db.Contacts.FirstOrDefault(item =>item.UserID == userId && item.ContactID == contact.ContactID);
           if(position != null)
            {
                position.Avatar = contact.Avatar;
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