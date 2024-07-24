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
        public void Delete(Contact contact)
        {
                db.Contacts.Remove(contact);
                db.SaveChanges();
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

        public void Insert(Contact contact)
        {
            db.Contacts.Add(contact);
            db.SaveChanges();
        }

        public List<Contact> ListContact()
        {
            return db.Contacts.ToList();
        }

        public List<Contact> ListContactSearch(List<Contact> search )
        {
           return search.ToList();
        }

        public void Update(Contact newContact)
        {
            var contact = db.Contacts.FirstOrDefault(item => item.ContactID == newContact.ContactID);
            contact.FullName = newContact.FullName;
            contact.PhoneNumber = newContact.PhoneNumber;
            contact.Address = newContact.Address;
            contact.Email = newContact.Email;
           
            db.SaveChanges();

        }
    }
}