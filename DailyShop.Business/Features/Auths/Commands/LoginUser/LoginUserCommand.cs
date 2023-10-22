using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Dtos;
using Core.Security.Hashing;
using DailyShop.Business.Features.Auths.DailyFrontends;
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
        public UserForLoginFrontendDto UserForLoginFrontendDto { get; set; }
        public string IpAddress { get; set; }
		public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoggedInDto>
		{
			private readonly IAppUserRepository _appUserRepository;
			private readonly IUserRepository _userRepository;
			private readonly IAuthService _authService;
			private readonly IMapper _mapper;
			private readonly AuthBusinessRules _authBusinessRules;

			public LoginUserCommandHandler(IAppUserRepository appUserRepository, IUserRepository userRepository, IAuthService authService, IMapper mapper,AuthBusinessRules authBusinessRules)
			{
				_appUserRepository = appUserRepository;
				_userRepository = userRepository;
				_authService = authService;
				_authBusinessRules = authBusinessRules;
				_mapper = mapper;
			}

			public async Task<LoggedInDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
			{
				await _authBusinessRules.EmailMustExist(request.UserForLoginFrontendDto.email);

				//UserForLoginFrontendDto userForLoginFrontendDto = _mapper.Map<UserForLoginFrontendDto>(request.UserForLoginFrontendDto);

				AppUser appUserToLogin = await _appUserRepository.GetAsync(u => u.Email == request.UserForLoginFrontendDto.email);

			
				LoggedUserFrontendDto loggedUserDto =  _mapper.Map<LoggedUserFrontendDto>(appUserToLogin);

				//List<Address> address = await _authBusinessRules.GetAddress(appUserToLogin.Id);

				//foreach (Address addressDto in address)
				//{
				//	AddressFrontendDto addressFrontendDto = _mapper.Map<AddressFrontendDto>(addressDto);
				//	loggedUserDto.addresses.Add(addressFrontendDto);
				//}

				bool isPasswordCorrect = HashingHelper.VerifyPasswordHash(request.UserForLoginFrontendDto.password, appUserToLogin.PasswordHash, appUserToLogin.PasswordSalt);
				if (!isPasswordCorrect)
					throw new BusinessException("Password is incorrect");
				
				LoggedInDto loggedInDto = new()
				{
					AccessToken = await _authService.CreateAccessToken(appUserToLogin),
					RefreshToken = await _authService.CreateRefreshToken(appUserToLogin, request.IpAddress),
					LoggedUserDto = loggedUserDto
				};

				return loggedInDto;
			}
		}
	}
}
