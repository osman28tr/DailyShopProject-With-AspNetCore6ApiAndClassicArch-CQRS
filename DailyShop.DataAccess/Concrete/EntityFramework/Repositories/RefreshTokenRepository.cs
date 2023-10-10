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
	public class RefreshTokenRepository : EfRepositoryBase<RefreshToken, DailyShopContext>, IRefreshTokenRepository
	{
		public RefreshTokenRepository(DailyShopContext context) : base(context)
		{
		}
	}
}
