using DailyShop.Business.Services.Repositories;
using DailyShop.DataAccess.Concrete.EntityFramework.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyShop.Business.Services.Repositories.Dapper;
using DailyShop.DataAccess.Concrete.Dapper.Repositories;

namespace DailyShop.DataAccess
{
	public static class PersistanceServiceRegistration
	{
		public static IServiceCollection AddPersistanceRegistration(this IServiceCollection services)
		{
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();
			services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
			services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
			services.AddScoped<IAppUserRepository, AppUserRepository>();
			services.AddScoped<IAddressRepository, AddressRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IProductColorRepository, ProductColorRepository>();
			services.AddScoped<IProductSizeRepository, ProductSizeRepository>();
			services.AddScoped<IProductImageRepository, ProductImageRepository>();
			services.AddScoped<IReviewRepository, ReviewRepository>();
			services.AddScoped<IWebSiteSettingRepository, WebSiteSettingRepository>();
			services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IDpProductRepository, DpProductRepository>();
			services.AddScoped<IOrderRepository, OrderRepository>();
			services.AddScoped<IPaymentRepository, PaymentRepository>();
			services.AddScoped<IFavoriteRepository, FavoriteRepository>();
			
			return services;
		}
	}
}
