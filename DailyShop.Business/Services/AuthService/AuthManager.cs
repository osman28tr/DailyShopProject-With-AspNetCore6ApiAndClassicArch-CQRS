using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Encryption;
using Core.Security.Entities;
using Core.Security.JWT;
using DailyShop.Business.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Services.AuthService
{
	public class AuthManager : IAuthService
	{
		private readonly ITokenHelper _tokenHelper;
		private readonly IUserOperationClaimRepository _userOperationClaimRepository;
		private readonly IRefreshTokenRepository _refreshTokenRepository;
		private readonly IConfiguration _configuration;
		public AuthManager(ITokenHelper tokenHelper, IUserOperationClaimRepository userOperationClaimRepository, IRefreshTokenRepository refreshTokenRepository,IConfiguration configuration)
		{
			_tokenHelper = tokenHelper;
			_userOperationClaimRepository = userOperationClaimRepository;
			_refreshTokenRepository = refreshTokenRepository;
			_configuration = configuration;
		}

		public async Task<RefreshToken> AddRefreshTokenToDb(RefreshToken refreshToken)
		{
			RefreshToken addedRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
			return addedRefreshToken;
		}

		public async Task<AccessToken> CreateAccessToken(User user)
		{
			IPaginate<UserOperationClaim> userOperationClaims =
			   await _userOperationClaimRepository.GetListAsync(
				   u => u.UserId == user.Id,
				   include: u => u.Include(u => u.OperationClaim)
				   );

			IList<OperationClaim> operationClaims =
				userOperationClaims.Items.Select(u => new OperationClaim
				{ Id = u.OperationClaim.Id, Name = u.OperationClaim.Name }).ToList();

			AccessToken accessToken = _tokenHelper.CreateToken(user, operationClaims);
			return accessToken;
		}

		public Task<RefreshToken> CreateRefreshToken(User user, string ipAddress)
		{
			RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(user, ipAddress);
			return Task.FromResult(refreshToken);
		}

		public int VerifyToken(string token)
		{
			if (token == null)
			{
				throw new BusinessException("Lütfen giriş yapınız.");
			}
			
			var validatedToken = GetValidatedToken(token);
			int userId = int.Parse(validatedToken.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
			return userId;
		}
		private JwtSecurityToken GetValidatedToken(string token)
		{
            var tokenHandler = new JwtSecurityTokenHandler();
            TokenOptions? tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = tokenOptions?.Issuer,
                ValidateAudience = true,
                ValidAudience = tokenOptions?.Audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions?.SecurityKey),
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;
			return jwtToken;
        }
	}
}
