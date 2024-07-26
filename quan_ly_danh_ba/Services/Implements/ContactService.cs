using AutoMapper;
using Data.Entity;
using Dtos;
using quan_ly_danh_ba.Respository.Interfaces;
using quan_ly_danh_ba.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quan_ly_danh_ba.Services.Implements
{
    public class ContactService : IContactService
    {
        private readonly IContactRespository _contactRepo;
        private readonly IGroupContactService _groupContactService;
        private readonly IConnectContact_GroupContactRespository _connect;
        private readonly IMapper _mapper;
        
      public ContactService(IContactRespository contactRepo, IMapper mapper, IGroupContactService groupContactService,IConnectContact_GroupContactRespository connect)
        {
            _contactRepo = contactRepo;
            _mapper = mapper;
            _groupContactService = groupContactService;
            _connect=connect;
        }

        public ContactCreateDto Delete(Guid id)
        {
            var position = _contactRepo.FindById(id);
            if(position != null)
            {
                _contactRepo.Delete(position);
                return _mapper.Map<ContactCreateDto>(position);
            }
            return null;
        }

        public ContactCreateDto FindById(Guid id)
        {
            var position= _contactRepo.FindById(id);
            if (position != null) {
                return _mapper.Map<ContactCreateDto>(position);
            }
            return null;
        }

        public ContactCreateDto Insert(ContactCreateDto contactCreateDto,  string newGroupContactNames)
        {
            contactCreateDto.ContactID = Guid.NewGuid();
            var position =_contactRepo.Insert(_mapper.Map<Contact>(contactCreateDto));
            
            if (!string.IsNullOrEmpty(newGroupContactNames))
            {
                var newGroupContactName = newGroupContactNames.Split(',')
                                         .Select(item => item.Trim())
                                         .Distinct(StringComparer.OrdinalIgnoreCase)  // Loại bỏ trùng lặp không phân biệt hoa thường
                                          .ToList();

                // Lấy danh sách các giá trị cần loại bỏ, những giá trị này đã tồn tại trong db.GroupContacts
                var valuesToRemove = _groupContactService.ListGroupContact().Select(g => g.GroupName)
                         .Except(newGroupContactName, StringComparer.OrdinalIgnoreCase)
                                     .ToList();

                // Lọc những giá trị từ valuesToRemove mà không tồn tại trong groupContactName
                var valuesAdd = valuesToRemove
                                .Where(item => !contactCreateDto.GroupNames.Any(gItem => string.Equals(gItem, item, StringComparison.OrdinalIgnoreCase)))
                                .ToList();

                // Xóa các giá trị cần loại bỏ khỏi newGroupContactName
                newGroupContactName.RemoveAll(item => valuesToRemove.Contains(item, StringComparer.OrdinalIgnoreCase));

                // Thêm các giá trị từ valuesAdd vào groupContactName
                contactCreateDto.GroupNames.AddRange(valuesAdd);


                foreach (var item in newGroupContactName)
                {
                   var newGroupContact=_groupContactService.Insert(item);
                    var transform = _mapper.Map<GroupContact>(newGroupContact);
                    _connect.AddContact(transform, position);
                    _connect.AddGroupContact(position, transform);
                }

            }
            foreach (var groupName in contactCreateDto.GroupNames)
            {
                var oldGroupContact = _groupContactService.FindByName(groupName);
                var transform = _mapper.Map<GroupContact>(oldGroupContact);
                _connect.AddContact(transform, position);
                _connect.AddGroupContact(position, transform);
            }
            return _mapper.Map<ContactCreateDto>(position);
        }

        public List<ContactCreateDto> ListContact()
        {
            return _mapper.Map<List<ContactCreateDto>>(_contactRepo.ListContact());
        }

        public List<ContactCreateDto> Search(string name, string phone, string groupContact)
        {
            var lowerName = name?.ToLower().Trim();
            var lowerPhone = phone?.Trim().ToLower();
            var lowerGroupContact = groupContact?.ToLower().Trim();

            var searchResults = _contactRepo.ListContact()
                .Where(item =>
                    (string.IsNullOrEmpty(lowerName) || item.FullName.ToLower().Contains(lowerName)) &&
                    (string.IsNullOrEmpty(lowerPhone) || item.PhoneNumber.Contains(lowerPhone)) &&
                    (string.IsNullOrEmpty(lowerGroupContact) || item.GroupContacts.Any(itemG => itemG.GroupName.ToLower() == lowerGroupContact))
                )
                .OrderBy(item => item.FullName).ToList();
            return _mapper.Map<List<ContactCreateDto>>(searchResults);
         }

        public ContactCreateDto Update(ContactCreateDto contactCreateDto, string newGroupContactNames)
        {
             _contactRepo.Update(_mapper.Map<Contact>(contactCreateDto));
            //var oldContact = db.Contacts.FirstOrDefault(item => item.ContactID.ToString() == contactCreateDto.ContactID.ToString());
            var oldContact = _contactRepo.FindById(contactCreateDto.ContactID);

            if (oldContact != null)
            {
               
                if (!string.IsNullOrEmpty(newGroupContactNames))
                {
                    var newGroupContactName = newGroupContactNames.Split(',')
                                             .Select(item => item.Trim())
                                             .Distinct(StringComparer.OrdinalIgnoreCase)  // Loại bỏ trùng lặp không phân biệt hoa thường
                                             .ToList();

                    // Lấy danh sách các giá trị cần loại bỏ, những giá trị này đã tồn tại trong db.GroupContacts
                    var valuesToRemove = newGroupContactName.Where(item => _groupContactService.ListGroupContact().Any(gItem => string.Equals(gItem.GroupName, item, StringComparison.OrdinalIgnoreCase))).ToList();
                                         
                    if (contactCreateDto.GroupNames == null)
                    {
                        contactCreateDto.GroupNames = new List<string>() ;
                    }
                    // Lọc những giá trị từ valuesToRemove mà không tồn tại trong groupContactName
                    var valuesAdd = valuesToRemove?
                                    .Where(item => contactCreateDto.GroupNames?.Any(gItem => string.Equals(gItem, item, StringComparison.OrdinalIgnoreCase))==false)
                                    .ToList();

                    // Xóa các giá trị cần loại bỏ khỏi newGroupContactName
                    newGroupContactName.RemoveAll(item => valuesToRemove.Contains(item, StringComparer.OrdinalIgnoreCase));

                    // Thêm các giá trị từ valuesAdd vào groupContactName
                    
                        contactCreateDto.GroupNames.AddRange(valuesAdd);

                    


                    foreach (var item in newGroupContactName)
                    {
                        var existingContact = _groupContactService.GetById(item.Id);
                        if (existingContact == null)
                        {
                            var newGroupContact = _groupContactService.Insert(item);
                            var transform = _mapper.Map<GroupContact>(newGroupContact);
                            if (transform != null)
                            {
                                _connect.AddGroupContact(oldContact, transform);
                                _connect.AddContact(transform, oldContact);
                                _connect.ConnectSave();
                            }
                        }

                    }

                }
                var toRemove = oldContact.GroupContacts
                             .Where(gc => !contactCreateDto.GroupNames.Contains(gc.GroupName, StringComparer.OrdinalIgnoreCase))
                             .ToList();
                foreach (var item in toRemove)
                {
                    _connect.RemoveContact(item, oldContact);
                    _connect.RemoveGroupContact(oldContact, item);
                }
                foreach (var groupName in contactCreateDto.GroupNames)
                {
                    var groupContact = Quan_ly_danh_baEntity.db.GroupContacts.FirstOrDefault(gc => gc.GroupName.Equals(groupName, StringComparison.OrdinalIgnoreCase));
                    if (groupContact != null && !oldContact.GroupContacts.Contains(groupContact))
                    {
                        _connect.AddContact(groupContact, oldContact);
                        _connect.AddGroupContact(oldContact, groupContact);
                    }

                }
               _connect.ConnectSave();
              
                return _mapper.Map<ContactCreateDto>(oldContact);
            }
            return null;

            //if (oldContact != null)
            //{

            //    if (!string.IsNullOrEmpty(newGroupContactNames))
            //    {
            //        var newGroupContactName = newGroupContactNames.Split(',')
            //                                 .Select(item => item.Trim())
            //                                 .Distinct(StringComparer.OrdinalIgnoreCase)  // Loại bỏ trùng lặp không phân biệt hoa thường
            //                                 .ToList();

            //        // Lấy danh sách các giá trị cần loại bỏ, những giá trị này đã tồn tại trong db.GroupContacts
            //        var valuesToRemove = _groupContactService.ListGroupContact().Select(g => g.GroupName)
            //                             .Except(newGroupContactName, StringComparer.OrdinalIgnoreCase)
            //                             .ToList();

            //        // Lọc những giá trị từ valuesToRemove mà không tồn tại trong groupContactName
            //        var valuesAdd = valuesToRemove
            //                        .Where(item => !contactCreateDto.GroupNames.Any(gItem => string.Equals(gItem, item, StringComparison.OrdinalIgnoreCase)))
            //                        .ToList();

            //        // Xóa các giá trị cần loại bỏ khỏi newGroupContactName
            //        newGroupContactName.RemoveAll(item => valuesToRemove.Contains(item, StringComparer.OrdinalIgnoreCase));

            //        // Thêm các giá trị từ valuesAdd vào groupContactName
            //        contactCreateDto.GroupNames.AddRange(valuesAdd);



            //        foreach (var item in newGroupContactName)
            //        {
            //            var newGroupContact = _groupContactService.Insert(item);
            //            var transform = _mapper.Map<GroupContact>(newGroupContact);
            //            _connect.AddContact(transform,oldContact);
            //            _connect.AddGroupContact(oldContact, transform);
            //        }

            //    }
            //    var toRemove = _connect.GetAllGroups(oldContact)
            //                 .Where(gc => contactCreateDto.GroupNames.Contains(gc.GroupName, StringComparer.OrdinalIgnoreCase))
            //                 .ToList();
            //    foreach (var item in toRemove)
            //    {
            //        _connect.RemoveContact(item, oldContact);
            //        oldContact=_connect.RemoveGroupContact(oldContact, item);
            //    }
            //    foreach (var groupName in contactCreateDto.GroupNames)
            //    {
            //        var groupContact = _groupContactService.FindByName(groupName);
            //        var transform = _mapper.Map<GroupContact>(groupContact);
            //        if (groupContact != null && !_connect.GetAllGroups(oldContact).Contains(transform))
            //        {
            //            _connect.AddContact(transform, oldContact);
            //            oldContact=_connect.AddGroupContact(oldContact, transform);
            //        }

            //    }

            //    return _mapper.Map<ContactCreateDto>(oldContact);
            //}
            //return null;
        }
    }
}