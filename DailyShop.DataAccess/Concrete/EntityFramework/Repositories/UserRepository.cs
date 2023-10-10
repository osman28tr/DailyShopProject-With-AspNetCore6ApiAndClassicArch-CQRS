using Core.Persistence.Repositories;
using Core.Security.Entities;
using DailyShop.Business.Services.Repositories;
using DailyShop.DataAccess.Concrete.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.DataAccess.Concrete.EntityFramework.Repositories
{
	public class UserRepository : EfRepositoryBase<User, DailyShopContext>, IUserRepository
	{
		public UserRepository(DailyShopContext context) : base(context)
		{
		}
	}
}
