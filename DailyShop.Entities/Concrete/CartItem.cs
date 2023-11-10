using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Entities.Concrete
{
    public class CartItem:Entity
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public CartItem()
        {
            
        }
        public CartItem(int id)
        {
            Id = id;
        }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}
