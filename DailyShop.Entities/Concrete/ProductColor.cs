using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Entities.Concrete
{
    public class ProductColor:Entity
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public Product Product { get; set; }
        public ProductColor()
        {
            
        }
        public ProductColor(int id):base(id)
        {
            Id = id;
        }
    }
}
