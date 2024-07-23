using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.mapdata.ContactData
{
    public class MapGroupContact:Quan_ly_danh_baEntity
    {
        public List<GroupContact> ListGroupContacts()
        {
            return db.GroupContacts.ToList();
        }
        
		public GroupContact FindByName(string GroupContactName)
		{
			var groupContact = db.GroupContacts.FirstOrDefault(item => item.GroupName.ToLower() == GroupContactName.ToLower());
			if (groupContact == null)
			{
				return null;
			}
			return groupContact;
		}
		
      
    }
}
