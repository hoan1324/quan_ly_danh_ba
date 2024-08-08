﻿using AutoMapper;
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
    public class GroupContactService :  IGroupContactService
    {
        private readonly IGroupContactRespository _groupContactRepo;
        private readonly IMapper _mapper;
        private readonly IConnectModelRespository _connect;
        public GroupContactService(IGroupContactRespository groupContactRepo, IMapper mapper,IConnectModelRespository connect)
        {
            _groupContactRepo = groupContactRepo;
            _mapper = mapper;
            _connect = connect;
        }

        public Boolean DeleteList(List<string> groupNames)
        {
            foreach (var groupName in groupNames) {
               
            var position =_groupContactRepo.FindByName(groupName);
                if (position == null) {
                    return false;
                 }
                
                if (position != null ) {
                    if(_connect.GetAllContacts(position).Count > 0) {
                        return false;
                    }
                    var delete = _groupContactRepo.Delete(position);
                    if (delete == null)
                    {
                        return false;
                    }
                }
                
            }
            return true;
        }

        public GroupContactDto FindByName(string groupName, Guid? userId = null)
        {
           var userID=userId ?? SessionConfig.GetUser().UserID;
            var position =  _groupContactRepo.FindByName(groupName, userID);
            return _mapper.Map<GroupContactDto>(position);
        }

        public GroupContactDto Insert(string groupName, Guid? userId = null)
        {
            var userID = userId ?? SessionConfig.GetUser().UserID;

            var position = _groupContactRepo.FindByName(groupName, userID);
            if (position == null)
            {
                var newGroupContactDto = new GroupContactDto
                {
                    GroupContactID = Guid.NewGuid(),
                    GroupName = groupName.Substring(0, 1).ToUpper() + groupName.Substring(1, groupName.Length - 1),
                    UserID= userID,
                };
                var done = _groupContactRepo.Insert(_mapper.Map<GroupContact>(newGroupContactDto),userId);
                return _mapper.Map<GroupContactDto>(done);
            }
            return null;
        }

        public List<GroupContactDto> ListGroupContact()
        {
            return _mapper.Map<List<GroupContactDto>>(_groupContactRepo.ListGroupContact());
        }

    }
}