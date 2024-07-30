using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dtos
{
    public partial class UserDto
    {
        public System.Guid UserID { get; set; }
        public string UserName { get; set; }
        public byte[] Avatar { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string LinkFacebook { get; set; }
        public string LinkTikTok { get; set; }
        public string LinkInstagram { get; set; }
    }
}