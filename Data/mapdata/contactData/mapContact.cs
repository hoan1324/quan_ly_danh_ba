﻿using Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.mapdata.contactData
{
	public class MapContact : Quan_ly_danh_baEntity
	{
		//list
		public List<Contact> ListContacts()
		{
			return db.Contacts.ToList();
		}
		//find by id
		public Contact FindById(Guid contactID)
		{
			var contact = db.Contacts.FirstOrDefault(item => item.ContactID == contactID);
			if (contact == null)
			{
				return null;
			}
			return contact;
		}
		//INSERT
		public Contact InsertContact(Contact contact, List<string> groupContactName, string newGroupContactNames)
		{
			try
			{
				if (!string.IsNullOrEmpty(newGroupContactNames))
				{
					var newGroupContactName = newGroupContactNames.Split(',')
											 .Select(item => item.Trim())
											 .Distinct(StringComparer.OrdinalIgnoreCase)  // Loại bỏ trùng lặp không phân biệt hoa thường
										      .ToList();

					// Lấy danh sách các giá trị cần loại bỏ, những giá trị này đã tồn tại trong db.GroupContacts
					var valuesToRemove = newGroupContactName
										 .Except(db.GroupContacts.Select(g => g.GroupName), StringComparer.OrdinalIgnoreCase)
										 .ToList();

					// Lọc những giá trị từ valuesToRemove mà không tồn tại trong groupContactName
					var valuesAdd = valuesToRemove
						            .Where(item => !groupContactName.Any(gItem => string.Equals(gItem, item, StringComparison.OrdinalIgnoreCase)))
						            .ToList();

					// Xóa các giá trị cần loại bỏ khỏi newGroupContactName
					newGroupContactName.RemoveAll(item => valuesToRemove.Contains(item, StringComparer.OrdinalIgnoreCase));

					// Thêm các giá trị từ valuesAdd vào groupContactName
					groupContactName.AddRange(valuesAdd);


					foreach (var item in newGroupContactName)
					{
						GroupContact newGroupContact = new GroupContact
						{
							GroupContactID = Guid.NewGuid(),
							GroupName = item.Substring(0, 1).ToUpper() + item.Substring(1, item.Length - 1)
						};

						contact.GroupContacts.Add(newGroupContact);
						newGroupContact.Contacts.Add(contact);
						db.GroupContacts.Add(newGroupContact);
					}

				}
				foreach (var groupName in groupContactName)
				{
					var position = db.GroupContacts.FirstOrDefault(gc => gc.GroupName.Equals(groupName, StringComparison.OrdinalIgnoreCase));
					contact.GroupContacts.Add(position);
					position.Contacts.Add(contact);

				}
				contact.ContactID=Guid.NewGuid();
				db.Contacts.Add(contact);
				db.SaveChanges();
				return contact;
			}
			catch (DbEntityValidationException ex)
			{
				foreach (var validationErrors in ex.EntityValidationErrors)
				{
					foreach (var validationError in validationErrors.ValidationErrors)
					{
						Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
					}
				}
				return null;
			}
		}

		//Delete
		public Contact DeleteContact(Guid contactID)
		{
			var oldContact = db.Contacts.FirstOrDefault(item => item.ContactID == contactID);
			if (oldContact != null)
			{
				db.Contacts.Remove(oldContact);
				db.SaveChanges();
				return oldContact;
			}
			return null;

		}
		//Update
		public Contact UpdateContact(Contact contact, List<string> groupContactName, string newGroupContactNames)
		{
			var oldContact = db.Contacts.FirstOrDefault(item => item.ContactID.ToString() == contact.ContactID.ToString());
			if (oldContact != null)
			{
				oldContact.FullName = contact.FullName;
				oldContact.PhoneNumber = contact.PhoneNumber;
				oldContact.Address = contact.Address;
				oldContact.Email = contact.Email;
				if (!string.IsNullOrEmpty(newGroupContactNames))
				{
					var newGroupContactName = newGroupContactNames.Split(',')
											 .Select(item => item.Trim())
											 .Distinct(StringComparer.OrdinalIgnoreCase)  // Loại bỏ trùng lặp không phân biệt hoa thường
											 .ToList();

					// Lấy danh sách các giá trị cần loại bỏ, những giá trị này đã tồn tại trong db.GroupContacts
					var valuesToRemove = newGroupContactName
							             .Except(db.GroupContacts.Select(g => g.GroupName), StringComparer.OrdinalIgnoreCase)
										 .ToList();

					// Lọc những giá trị từ valuesToRemove mà không tồn tại trong groupContactName
					var valuesAdd = valuesToRemove
									.Where(item => !groupContactName.Any(gItem => string.Equals(gItem, item, StringComparison.OrdinalIgnoreCase)))
									.ToList();

					// Xóa các giá trị cần loại bỏ khỏi newGroupContactName
					newGroupContactName.RemoveAll(item => valuesToRemove.Contains(item, StringComparer.OrdinalIgnoreCase));

					// Thêm các giá trị từ valuesAdd vào groupContactName
					groupContactName.AddRange(valuesAdd);
                    


					foreach (var item in newGroupContactName)
					{
						GroupContact newGroupContact = new GroupContact
						{
							GroupContactID = Guid.NewGuid(),
							GroupName = char.ToUpper(item[0]) + item.Substring(1)
						};

						oldContact.GroupContacts.Add(newGroupContact);
						newGroupContact.Contacts.Add(oldContact);
						db.GroupContacts.Add(newGroupContact);
					}

				}
				var toRemove = contact.GroupContacts
							 .Where(gc => !groupContactName.Contains(gc.GroupName, StringComparer.OrdinalIgnoreCase))
							 .ToList();
				foreach (var item in toRemove)
				{
					oldContact.GroupContacts.Remove(item);
					item.Contacts.Remove(oldContact);
				}
				foreach (var groupName in groupContactName)
				{
					var groupContact = db.GroupContacts.FirstOrDefault(gc => gc.GroupName.Equals(groupName, StringComparison.OrdinalIgnoreCase));
					if (groupContact != null && !contact.GroupContacts.Contains(groupContact))
					{
						oldContact.GroupContacts.Add(groupContact);
						groupContact.Contacts.Add(oldContact);
					}

				}
				db.SaveChanges();
				return contact;
			}
			return null;
		}
	}
}