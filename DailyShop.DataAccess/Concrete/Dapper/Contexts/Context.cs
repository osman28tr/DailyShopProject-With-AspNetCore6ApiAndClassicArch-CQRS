using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DailyShop.DataAccess.Concrete.Dapper.Contexts
{
    public static class Context
    {
        private static IConfiguration Configuration { get; set; }

        public static IServiceCollection GetConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            Configuration = configuration;
            return services;
        }
        public static string Connection()
        {
            return Configuration.GetConnectionString("MSSQL");
        }
    }
}
