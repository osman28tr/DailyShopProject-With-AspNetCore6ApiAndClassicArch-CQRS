using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Services.AuthService;
using DailyShop.Business.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DailyShop.Business.Middlewares.Auth
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        
        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context,IAuthService authService,IAppUserRepository userRepository)
        {
            if (context.Request.Path.StartsWithSegments("/api/Admin"))
            {
                string authorizationHeader = context.Request.Headers["Authorization"];
                if (authorizationHeader != null)
                {
                    string token = authorizationHeader.Split(' ')[1];
                    int userId = authService.VerifyToken(token);
                    string userRole = await userRepository.Query().Where(x => x.Id == userId).Select(x => x.Role)
                        .FirstOrDefaultAsync();
                    if (userRole.ToLower() != "admin")
                    {
                        throw new BusinessException("Hoopp Hemşerim nereye?");
                    }
                }
            }
            await _next(context);
        }
    }
}
