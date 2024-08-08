﻿using Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quan_ly_danh_ba.Services.Interfaces
{
    public interface IUserService
    {
        List<UserDto> ListUser();
        UserDto FindByUser(UserDto user,string Type);
        UserDto Insert(UserDto user);
        UserDto FindById(UserDto user);
        UserDto CheckPassword(Guid id, string pass);
        UserDto Update(UserDto user,string type);
    }
}