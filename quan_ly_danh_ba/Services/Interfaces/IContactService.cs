using Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quan_ly_danh_ba.Services.Interfaces
{
    public interface IContactService
    {
       List<ContactCreateDto> ListContact();
       List<ContactCreateDto> Search(string name, string phone, string groupContact);
       ContactCreateDto FindById(Guid id);
       ContactCreateDto Insert(ContactCreateDto contactCreateDto,  string newGroupContactNames);
       ContactCreateDto Update(ContactCreateDto contactCreateDto, string newGroupContactNames);
       ContactCreateDto Delete(Guid id);

    }
}
