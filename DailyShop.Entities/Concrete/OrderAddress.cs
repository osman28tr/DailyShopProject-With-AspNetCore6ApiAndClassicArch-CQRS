using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Repositories;

namespace DailyShop.Entities.Concrete
{
    public class OrderAddress:Entity
    {
        public string? Title { get; set; }
        public string? Adres { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public OrderAddress()
        {
        }

        public OrderAddress(int id):base(id)
        {
            Id = id;
        }
    }
}
