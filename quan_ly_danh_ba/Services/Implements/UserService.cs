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
  
    public class UserService :  IUserService
    {
        private readonly IUserRespository _UserRepo;
        private readonly IMapper _mapper;
        public UserService(IUserRespository UserRepo, IMapper mapper)
        {
            _UserRepo = UserRepo;
            _mapper = mapper;
        }

        public UserDto CheckPassword(Guid id, string pass)
        {
            var position=_UserRepo.FindById(id);
            if(position != null)
            {
                var check=_UserRepo.FindByUser(position.UserName, pass);
                if (position != null)
                {
                    return _mapper.Map<UserDto>(check);
                }
                return null;
            }
            return null;
        }

        public UserDto FindById(UserDto user)
        {
            return _mapper.Map<UserDto>(_UserRepo.FindById(user.UserID));

        }

        public UserDto FindByUser(UserDto user)
        {
            return _mapper.Map<UserDto>(_UserRepo.FindByUser(user.Email,user.Password));
        }

        public UserDto Insert(UserDto user)
        {

            var position = _UserRepo.FindById(user.UserID);
            if (position == null)
            {
                user.UserID=Guid.NewGuid();
                var done = _UserRepo.Insert(_mapper.Map<User>(user));
                return _mapper.Map<UserDto>(done);
            }
            return null;
        }

        public List<UserDto> ListUser()
        {
            return _mapper.Map<List<UserDto>>(_UserRepo.ListUser());
        }

        public UserDto Update(UserDto user,string type)
        {
            var currentUser = SessionConfig.GetUser();
            if (currentUser == null)
            {
                throw new InvalidOperationException("Current user is not available.");
            }
            //if (type == "profile") {
            //    user.Password = currentUser.Password;
            //    user.Avatar = currentUser.Avatar;
            //}
            //else if (type == "password" || type=="avatar"){ 
            //if(type == "password")
            //    {
            //        user.Avatar=currentUser.Avatar;
            //    }
            //else if (type == "avatar") {
            //    user.Password  =currentUser.Password;
            //    }
            //    user.UserID = currentUser.UserID;
            //    user.UserName = user.UserName;
            //    user.PhoneNumber = user.PhoneNumber;
            //    user.Address = user.Address;
            //    user.Email = user.Email;
            //    user.LinkFacebook = user.LinkFacebook;
            //    user.LinkTikTok = user.LinkTikTok;
            //    user.LinkInstagram = user.LinkInstagram;
            //}
            switch (type)
            {
                case "profile":
                    user.Password = currentUser.Password;
                    user.Avatar = currentUser.Avatar;
                    break;

                case "password":
                    user.Avatar = currentUser.Avatar;
                    // Preserve other necessary fields
                    break;

                case "avatar":
                    user.Password = currentUser.Password;
                    // Preserve other necessary fields
                    break;

                default:
                    throw new ArgumentException("Invalid update type", nameof(type));
            }

            // Preserve other user information
            user.UserID = currentUser.UserID;
            user.UserName = user.UserName ?? currentUser.UserName;
            user.PhoneNumber = user.PhoneNumber ?? currentUser.PhoneNumber;
            user.Address = user.Address ?? currentUser.Address;
            user.Email = user.Email ?? currentUser.Email;
            user.LinkFacebook = user.LinkFacebook ?? currentUser.LinkFacebook;
            user.LinkTikTok = user.LinkTikTok ?? currentUser.LinkTikTok;
            user.LinkInstagram = user.LinkInstagram ?? currentUser.LinkInstagram;

            var update = _UserRepo.Update(_mapper.Map<User>(user));
            return _mapper.Map<UserDto>(update);
        }
    }
}