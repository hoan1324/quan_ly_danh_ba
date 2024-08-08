using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quan_ly_danh_ba.Respository.Interfaces
{
     public interface IUserRespository
    {
        List<User> ListUser();
        List<User> ListUserSearch(List<User> search);
        User FindByUser(string Type,string email=null ,string password = null,string phone = null );
        User FindById(Guid id);
        User Insert(User user);
        User  Delete(User user);
        User Update(User user);

    }
}
