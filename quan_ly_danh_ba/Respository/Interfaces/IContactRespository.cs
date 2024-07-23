using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quan_ly_danh_ba.Respository.Interfaces
{
     interface IContactRespository
    {
        List<Contact> ListContact();
        List<Contact> ListContactSearch(List<Contact> search);
        Contact FindById(Guid id);
        Contact Insert(Contact contact);
        Contact Delete(Contact contact);
        Contact Update(Contact contact);

    }
}
