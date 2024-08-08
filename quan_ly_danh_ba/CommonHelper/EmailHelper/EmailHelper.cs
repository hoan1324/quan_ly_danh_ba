using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace CommonHelper
{
    public static class EmailHelper
    {
        private static string GenerateRandom()
        {
            var random = new Random();
            return random.Next(100000, 1000000).ToString();
        }
        public static string SendEmail(string email,string subject=null,string body=null)
        {
            string randomNumber=GenerateRandom();
            if (subject == null)
            {
                subject = "Hỗ trợ quên mật khẩu ";
            }
            if (body == null)
            {
                body = randomNumber + "là mã xác minh của bạn";
            }
            var fromEmail = "nvhoffice235@gmail.com";
            var password = "hoan0348966964";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587, // Cổng cho TLS/STARTTLS
                Credentials = new NetworkCredential(fromEmail, password),
                EnableSsl = true, // Bật SSL/TLS,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(email),
                Subject = subject,
                Body = body,
                IsBodyHtml = false, // Nếu bạn muốn gửi email HTML
            };

            mailMessage.To.Add(email);

            smtpClient.Send(mailMessage);
            return randomNumber;
        }
    }
}