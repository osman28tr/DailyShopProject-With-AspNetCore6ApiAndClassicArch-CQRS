using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Repositories;

namespace DailyShop.Entities.Concrete
{
    public class Order:Entity
    {
        public int? UserId { get; set; }
        public int? OrderAddressId { get; set; }
        public string? OrderNumber { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? Status { get; set; } = "New";
        public bool? IsPaymentCompleted { get; set; }
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        public Order(int id):base(id)
        {
            Id = id;
        }
        public AppUser? User { get; set; }
        public OrderAddress? OrderAddress { get; set; }
        public Payment? Payment { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
