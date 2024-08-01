using Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quan_ly_danh_ba.Services.Interfaces
{
    public interface IGroupContactService
    {
        List<GroupContactDto> ListGroupContact();
        GroupContactDto Insert(string groupName,UserDto user=null);
        GroupContactDto FindByName(string groupName);
        Boolean DeleteList(List<string> groupNames);
    }
}