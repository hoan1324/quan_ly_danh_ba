using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Data.Dto
{
    public partial class ContactCreateDto
    {
        private readonly Guid _contactId;
        private readonly string _fullName;
        private readonly string _phoneNumber;
        private readonly string _address;
        private readonly string _email;
        private readonly List<string> groupContactNames;
       
    }
}
