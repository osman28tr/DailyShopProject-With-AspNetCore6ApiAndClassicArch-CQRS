using Core.CrossCuttingConcerns.Exceptions;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Middlewares.Auth
{
    public static class AuthMiddlewareExtension
    {
        public static void ConfigureCustomAuthExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<AuthMiddleware>();
        }
    }
}
