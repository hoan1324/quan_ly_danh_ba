using Data.Entity;
using quan_ly_danh_ba.Respository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quan_ly_danh_ba.Respository.Implements
{
    public class ContactRespository : Quan_ly_danh_baEntity, IContactRespository
    {
        public Contact Delete(Contact contact)
        {
            var position = db.Contacts.FirstOrDefault(item => item.ContactID == contact.ContactID);
            if (position != null)
            {
                db.Contacts.Remove(position);
                db.SaveChanges();
                return position;
            }
            return null;
        }

        public Contact FindById(Guid id)
        {
            var contact = db.Contacts.FirstOrDefault(item => item.ContactID == id);
            if (contact == null)
            {
                return null;
            }
            return contact;
        }

        public Contact Insert(Contact contact)
        {
            var position = db.Contacts.FirstOrDefault(item => item.ContactID == contact.ContactID);
            if (position == null)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                return contact;
            }
            return null;
        }

        public List<Contact> ListContact()
        {
            return db.Contacts.ToList();
        }

        public List<Contact> ListContactSearch(List<Contact> search )
        {
           return search.ToList();
        }

        public Contact Update(Contact contact)
        {
            var position = db.Contacts.FirstOrDefault(item => item.ContactID == contact.ContactID);
           if(position != null)
            {
                position.FullName = position.FullName;
                position.PhoneNumber = contact.PhoneNumber;
                position.Address = contact.Address;
                position.Email = contact.Email;

                db.SaveChanges();
                return position;
            }
           return null;

        }
    }
}