using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Entities.Concrete
{
    public class Cart:Entity
    {
        public int UserId { get; set; }
        public Cart()
        {
            CartItems = new List<CartItem>();
        }
        public Cart(int id)
        {
            Id = id;
        }       
        public string Status { get; set; }
        public AppUser? User { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
