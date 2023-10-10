using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.JWT;
using DailyShop.Business.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Services.AuthService
{
	public class AuthManager : IAuthService
	{
		private readonly ITokenHelper _tokenHelper;
		private readonly IUserOperationClaimRepository _userOperationClaimRepository;
		private readonly IRefreshTokenRepository _refreshTokenRepository;

		public AuthManager(ITokenHelper tokenHelper, IUserOperationClaimRepository userOperationClaimRepository, IRefreshTokenRepository refreshTokenRepository)
		{
			_tokenHelper = tokenHelper;
			_userOperationClaimRepository = userOperationClaimRepository;
			_refreshTokenRepository = refreshTokenRepository;
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
	}
}
