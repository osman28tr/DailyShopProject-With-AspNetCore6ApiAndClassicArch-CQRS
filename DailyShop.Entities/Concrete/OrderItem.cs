using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Repositories;

namespace DailyShop.Entities.Concrete
{
    public class OrderItem:Entity
    {
        public int? ProductId { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public OrderItem()
        {
        }
        public OrderItem(int id):base(id)
        {
            Id = id;
        }
        public Product? Product { get; set; }
    }
}
