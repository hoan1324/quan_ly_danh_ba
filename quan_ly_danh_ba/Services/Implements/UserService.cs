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

    }
}