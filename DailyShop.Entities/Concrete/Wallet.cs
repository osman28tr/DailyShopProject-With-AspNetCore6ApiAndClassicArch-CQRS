using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Entities.Concrete
{
    public class Wallet:Entity
    {
        public Wallet()
        {
            
        }
        public Wallet(int id):base(id) { Id = id; }
        public int? UserId { get; set; }
        public int? Balance { get; set; }
        public AppUser? User { get; set; }
    }
}
