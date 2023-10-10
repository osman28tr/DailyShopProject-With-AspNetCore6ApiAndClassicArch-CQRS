using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Entities.Concrete
{
	public class AppUser:User
	{
        public AppUser()
        {
            
        }
        public AppUser(int id)
        {
            Id = id;
        }
        
    }
}
