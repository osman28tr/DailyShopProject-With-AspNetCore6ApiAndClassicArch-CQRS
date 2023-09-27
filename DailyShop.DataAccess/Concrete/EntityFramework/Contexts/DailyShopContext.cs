using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.DataAccess.Concrete.EntityFramework.Contexts
{
	public class DailyShopContext:DbContext
	{
        public DailyShopContext(DbContextOptions<DailyShopContext> options):base(options)
        { }
    }
}
