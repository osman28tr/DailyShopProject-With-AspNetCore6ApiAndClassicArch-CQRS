using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.DataAccess.Concrete.EntityFramework.Repositories
{
	public class Entity
	{
        public int Id { get; set; }
        public Entity(int id)
        {
            Id = id;
        }
    }
}
