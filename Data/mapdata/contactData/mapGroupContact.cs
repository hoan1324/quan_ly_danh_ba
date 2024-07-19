using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.mapdata.ContactData
{
    public class mapGroupContact:Quan_ly_danh_baEntity
    {
        public List<GroupContact> listGroupContacts()
        {
            return db.GroupContacts.ToList();
        }
        
		public GroupContact findByName(string GroupContactName)
		{
			var groupContact = db.GroupContacts.FirstOrDefault(item => item.GroupName.ToUpper() == GroupContactName.ToUpper());
			if (groupContact == null)
			{
				return null;
			}
			return groupContact;
		}
		
      
    }
}
