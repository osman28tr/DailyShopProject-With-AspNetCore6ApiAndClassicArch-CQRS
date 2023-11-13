using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Entities.Concrete
{
    public class Color:Entity
    {
        public string? Name { get; set; }
        public Color()
        {
            Products = new HashSet<ProductColor>();
        }
        public Color(int id):base(id)
        {
            Id = id;
        }
        public ICollection<ProductColor>? Products { get; set; }
    }
}
