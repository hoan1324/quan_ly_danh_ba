using Data.Entity;
using Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quan_ly_danh_ba
{
    public static class SessionConfig
    {
        public static void SaveCaptcha(string captcha)
        {
            HttpContext.Current.Session["captcha"] =captcha;
        }
        public static string GetCaptcha(string captcha) {
            return (string)HttpContext.Current.Session["captcha"];

        }
        public static void SaveUser(UserDto user)
        {
            HttpContext.Current.Session["user"] = user;
        }
        public static UserDto GetUser() { 
        return (UserDto)HttpContext.Current.Session["user"];
        }
    }
}