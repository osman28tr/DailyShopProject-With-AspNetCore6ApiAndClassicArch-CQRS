using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.DataAccess
{
	public static class PersistanceServiceRegistration
	{
		public static IServiceCollection AddPersistanceRegistration(this IServiceCollection services)
		{
			return services;
		}
	}
}
