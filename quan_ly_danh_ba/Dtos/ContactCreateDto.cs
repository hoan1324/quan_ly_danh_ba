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
       public Guid _contactId;
       public  string _fullName;
       public string _phoneNumber;
       public string _address;
       public string _email;
       public List<string> groupContactNames;
       
    }
}
