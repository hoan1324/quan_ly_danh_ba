using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dtos
{
    public partial class GroupContactDto
    {
        public Guid GroupContactID { get; set; }
        public string GroupName { get; set; }
        public Guid UserID { get; set; }

    }
}