using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.DataAccess.Concrete.Dapper.Contexts
{
    public class Context
    {
        public static string Connection()
        {
            return "Data Source=DESKTOP-7LR4Q85\\SQLEXPRESS;Initial Catalog=DailyShopDb;Integrated Security=true;TrustServerCertificate=true;";
        }
    }
}
