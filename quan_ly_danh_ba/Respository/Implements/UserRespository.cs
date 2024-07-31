using Data.Entity;
using quan_ly_danh_ba.Respository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quan_ly_danh_ba.Respository.Implements
{
    public class UserRespository :  IUserRespository
    {
        public User Delete(User user)
        {
            var position = Quan_ly_danh_baEntity.db.Users.FirstOrDefault(item => item.UserID == user.UserID);
            if (position != null)
            {
                Quan_ly_danh_baEntity.db.Users.Remove(position);
                Quan_ly_danh_baEntity.db.SaveChanges();
                return position;
            }
            return null;
        }

       public User FindById(Guid id)
        {
            var User = Quan_ly_danh_baEntity.db.Users.FirstOrDefault(item => item.UserID == id);
            if (User == null)
            {
                return null;
            }
            return User;
        }

        public User FindByUser(string Email, string password)
        {
            var User = Quan_ly_danh_baEntity.db.Users.FirstOrDefault(item => item.Email==Email && item.Password==password);
            if (User == null)
            {
                return null;
            }
            return User;
        }

        public User Insert(User user)
        {
            var position = Quan_ly_danh_baEntity.db.Users.FirstOrDefault(item => item.UserID == user.UserID);
            if (position == null)
            {
                Quan_ly_danh_baEntity.db.Users.Add(user);
                Quan_ly_danh_baEntity.db.SaveChanges();
                return user;
            }
            return null;
        }

        public List<User> ListUser()
        {
            return Quan_ly_danh_baEntity.db.Users.ToList();
        }

        public List<User> ListUserSearch(List<User> search )
        {
           return search.ToList();
        }

        public User Update(User user)
        {
            var position = Quan_ly_danh_baEntity.db.Users.FirstOrDefault(item => item.UserID == user.UserID);
           if(position != null)
            {
                position.UserName = user.UserName;
                position.Password = user.Password;
                position.PhoneNumber = user.PhoneNumber;
                position.Address = user.Address;
                position.Email = user.Email;
                position.LinkFacebook=user.LinkFacebook;
                position.LinkTikTok=user.LinkTikTok;
                position.LinkInstagram = user.LinkInstagram;
                position.Avatar = user.Avatar;
                Quan_ly_danh_baEntity.db.SaveChanges();
                return position;
            }
           return null;

        }
    }
}