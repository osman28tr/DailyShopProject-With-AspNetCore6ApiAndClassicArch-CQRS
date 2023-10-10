using Core.Application.Pipelines.Authorization;
using DailyShop.Business.Features.Auths.Rules;
using DailyShop.Business.Services.AuthService;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business
{
	public static class ApplicationServiceRegistration
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

			services.AddScoped<AuthBusinessRules>();
			//services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
			services.AddScoped<IAuthService, AuthManager>();
			return services;
		}
	}
}
