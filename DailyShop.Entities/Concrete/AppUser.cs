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
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public ICollection<Address> Addresses { get; set; }
        public AppUser()
        {
            
        }
        public AppUser(int id)
        {
            Id = id;
        }      
    }
}
