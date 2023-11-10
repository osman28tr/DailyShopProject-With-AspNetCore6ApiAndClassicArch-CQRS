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
            
        }
        public Cart(int id)
        {
            Id = id;
        }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public ICollection<Product>? Products { get; set; }
        public AppUser? User { get; set; }
    }
}
