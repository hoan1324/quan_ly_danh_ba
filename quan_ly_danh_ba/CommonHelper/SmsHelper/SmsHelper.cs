using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace quan_ly_danh_ba.CommonHelper.SmsHelper
{
    public static class SmsHelper
    {
        public static  string SendSms(string phone,string subject=null)
        {
            string randomNumber = new Random().Next(100000, 1000000).ToString();
            if (subject == "")
            {
                subject = "Mã xác minh của bạn là " + randomNumber;
            }
            var client = new HttpClient();
            var values = new Dictionary<string, string>
        {
            { "phone",phone },
            { "message", subject },
            { "key", "textbelt" }  // API key mặc định cho tin nhắn miễn phí
        };

            var content = new FormUrlEncodedContent(values);
              client.PostAsync("https://textbelt.com/text", content);

            return randomNumber;
        }
    }
}