using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Entities.Concrete
{
    public class Payment:Entity
    {
        public int UserId { get; set; }
        public string CardNumber { get; set; }
        public string CardOwner { get; set; }
        public string LastDate { get; set; }
        public string Cvv { get; set; }
        public AppUser User { get; set; }
        public Payment()
        {
            
        }
        public Payment(int id):base(id)
        {
            Id = id;
        }
    }
}
