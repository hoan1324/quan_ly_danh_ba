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
        private readonly IGroupContactService _groupContactService;
        public UserService(IUserRespository UserRepo, IMapper mapper,  IGroupContactService groupContactService)
        {
            _UserRepo = UserRepo;
            _mapper = mapper;
            _groupContactService = groupContactService;
        }

        public UserDto CheckPassword(Guid id, string pass)
        {
            var position=_UserRepo.FindById(id);
            if(position != null)
            {
                var check=_UserRepo.FindByUser(position.Email, pass,"signIn");
                if (position != null)
                {
                    return _mapper.Map<UserDto>(check);
                }
                return null;
            }
            return null;
        }

        public UserDto FindById(Guid id)
        {
            return _mapper.Map<UserDto>(_UserRepo.FindById(id));

        }

        public UserDto FindByUser(UserDto user,string Type)
        {
            return _mapper.Map<UserDto>(_UserRepo.FindByUser(Type,user.Email,user.Password,user.PhoneNumber));
        }

        public UserDto Insert(UserDto user)
        {
            var defaultGroupContacts = new List<string> { "Bạn bè", "Công Việc", "Gia đình" };
            var position = _UserRepo.FindById(user.UserID);
            if (position == null)
            {
                user.UserID=Guid.NewGuid();
                var done = _UserRepo.Insert(_mapper.Map<User>(user));
               
               foreach (var item in defaultGroupContacts)
                {
                    _groupContactService.Insert(item,done.UserID);
                }
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
            if (user.UserID !=Guid.Empty)
            {
                currentUser = _mapper.Map<UserDto>(_UserRepo.FindById(user.UserID));
            }


            switch (type)
            {
                case "profile":
                    user.Password = currentUser.Password;
                    user.Avatar = currentUser.Avatar;
                    user.PhoneNumber= currentUser.PhoneNumber;
                    user.Email = currentUser.Email;
                    break;

                case "password":
                    user.Email = currentUser.Email;
                    user.Avatar= currentUser.Avatar;
                    user.PhoneNumber = currentUser.PhoneNumber;
                    break;

                case "avatar":
                    user.UserID = currentUser.UserID;
                    user.Email = currentUser.Email;
                    user.Password = currentUser.Password;
                    user.PhoneNumber = currentUser.PhoneNumber;
                    // Preserve other necessary fields
                    break;

                case "email":
                    user.UserID = currentUser.UserID;
                    user.Avatar = currentUser.Avatar;
                    user.Password = currentUser.Password;
                    user.PhoneNumber= currentUser.PhoneNumber;
                    break;

                case "phonenumber":
                    user.UserID = currentUser.UserID;
                    user.Avatar = currentUser.Avatar;
                    user.Password = currentUser.Password;
                    user.Email = currentUser.Email;
                    break;
                default:
                    throw new ArgumentException("Invalid update type", nameof(type));
            }

            // Preserve other user information
            user.UserName = user.UserName ?? currentUser.UserName;
            user.Address = user.Address ?? currentUser.Address;
            user.LinkFacebook = user.LinkFacebook ?? currentUser.LinkFacebook;
            user.LinkTikTok = user.LinkTikTok ?? currentUser.LinkTikTok;
            user.LinkInstagram = user.LinkInstagram ?? currentUser.LinkInstagram;

            var update = _UserRepo.Update(_mapper.Map<User>(user));
            return _mapper.Map<UserDto>(update);
        }
    }
}