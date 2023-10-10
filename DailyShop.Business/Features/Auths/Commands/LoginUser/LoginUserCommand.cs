using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Dtos;
using Core.Security.Hashing;
using DailyShop.Business.Features.Auths.Dtos;
using DailyShop.Business.Features.Auths.Rules;
using DailyShop.Business.Services.AuthService;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Auths.Commands.LoginUser
{
	public class LoginUserCommand:IRequest<LoggedInDto>
	{
        public UserForLoginDto UserForLoginDto { get; set; }
        public string IpAddress { get; set; }
		public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoggedInDto>
		{
			private readonly IAppUserRepository _appUserRepository;
			private readonly IUserRepository _userRepository;
			private readonly IAuthService _authService;
			private readonly AuthBusinessRules _authBusinessRules;

			public LoginUserCommandHandler(IAppUserRepository appUserRepository, IUserRepository userRepository, IAuthService authService, AuthBusinessRules authBusinessRules)
			{
				_appUserRepository = appUserRepository;
				_userRepository = userRepository;
				_authService = authService;
				_authBusinessRules = authBusinessRules;
			}

			public async Task<LoggedInDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
			{
				await _authBusinessRules.EmailMustExist(request.UserForLoginDto.Email);

				AppUser appUserToLogin = await _appUserRepository.GetAsync(u => u.Email == request.UserForLoginDto.Email);
				bool isPasswordCorrect = HashingHelper.VerifyPasswordHash(request.UserForLoginDto.Password, appUserToLogin.PasswordHash, appUserToLogin.PasswordSalt);
				if (!isPasswordCorrect)
					throw new BusinessException("Password is incorrect");

				LoggedInDto loggedInDto = new()
				{
					AccessToken = await _authService.CreateAccessToken(appUserToLogin),
					RefreshToken = await _authService.CreateRefreshToken(appUserToLogin, request.IpAddress)
				};

				return loggedInDto;
			}
		}
	}
}
