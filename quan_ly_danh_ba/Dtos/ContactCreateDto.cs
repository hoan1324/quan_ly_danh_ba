using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace Dtos
{
    public partial class ContactCreateDto
    {
        
       public Guid ContactID { get; set; }
       public  string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public byte[] Avatar { get; set; }

        public List<string> GroupNames { get; set; }
        public Guid UserID { get; set; }

    }
}
