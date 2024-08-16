using System;
using System.Collections.Generic;
using System.Linq;

using System.Net;
using System.Web;
using MailKit.Net.Smtp;
using MimeKit;

namespace CommonHelper
{
    public static class EmailHelper
    {
        
        public static string SendEmail(string toEmail, string subject = null, string body = null)
        {
            string randomNumber = new Random().Next(100000, 1000000).ToString();

            if (subject == null)
            {
                subject = "Hỗ trợ quên mật khẩu";
            }
            if (body == null)
            {
                body = randomNumber + " là mã xác minh của bạn";
            }
            var fromEmail = "nvhoffice235@gmail.com";
            var password = "xjwl sjhi ksmk xqhl"; // Mật khẩu ứng dụng hoặc mật khẩu email

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sender Name", fromEmail));
            message.To.Add(new MailboxAddress("Recipient Name", toEmail));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate(fromEmail, password);
                client.Send(message);
                client.Disconnect(true);
            }

            return randomNumber;
        }
        
    }
}