using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Entities.Concrete
{
    public class Favorite:Entity
    {
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public AppUser? User { get; set; }
        public Favorite() { }
        public Favorite(int id) : base(id) { Id = id; }
    }
}
