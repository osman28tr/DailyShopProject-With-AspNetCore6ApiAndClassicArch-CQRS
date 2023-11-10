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
        public string? Role { get; set; }
        public ICollection<Address>? Addresses { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Product>? Products { get; set; }
        public ICollection<Cart>? Carts { get; set; }
        public AppUser()
        {
            
        }
        public AppUser(int id)
        {
            Id = id;
        }      
    }
}
