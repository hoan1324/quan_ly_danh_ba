using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quan_ly_danh_ba.CommonHelper.Base64
{
    public static class ValidateBase64
    {
        private static string CleanBase64String(string base64String)
        {
            // Loại bỏ các ký tự không hợp lệ
            base64String = base64String.Trim();
            base64String = base64String.Replace("\r", "").Replace("\n", ""); // Xóa bỏ các ký tự xuống dòng nếu có

            // Chỉ giữ lại các ký tự hợp lệ cho Base64
            base64String = new string(base64String.Where(c =>
                (c >= 'A' && c <= 'Z') ||
                (c >= 'a' && c <= 'z') ||
                (c >= '0' && c <= '9') ||
                c == '+' ||
                c == '/' ||
                c == '=').ToArray());

            return base64String;
        }
        private static string PadBase64String(string base64String)
        {
            // Đảm bảo chuỗi có độ dài là bội số của 4
            while (base64String.Length % 4 != 0)
            {
                base64String += "=";
            }
            return base64String;
        }
        public static byte[] DecodeBase64String(string base64String)
        {
            // Làm sạch và padding chuỗi Base64
            base64String = CleanBase64String(base64String);
            base64String = PadBase64String(base64String);

            try
            {
                return Convert.FromBase64String(base64String);
            }
            catch (FormatException ex)
            {
                // Xử lý lỗi nếu chuỗi vẫn không hợp lệ
                Console.WriteLine("Lỗi giải mã: " + ex.Message);
                return null;
            }
        }
    }
}