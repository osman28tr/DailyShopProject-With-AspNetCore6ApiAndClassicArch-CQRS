using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
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

namespace DailyShop.Business.Features.Auths.Commands.RegisterUser
{
	public class RegisterUserCommand:IRequest<RegisteredDto>
	{
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public string IpAddress { get; set; }
		public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisteredDto>
		{
			private readonly IUserRepository _userRepository;
			private readonly IAppUserRepository _appUserRepository;
			private readonly IAuthService _authService;
			private readonly AuthBusinessRules _authBusinessRules;

			public RegisterUserCommandHandler(IUserRepository userRepository, IAppUserRepository appUserRepository, IAuthService authService, AuthBusinessRules authBusinessRules)
			{
				_userRepository = userRepository;
				_appUserRepository = appUserRepository;
				_authService = authService;
				_authBusinessRules = authBusinessRules;
			}

			public async Task<RegisteredDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
			{
				await _authBusinessRules.EmailCanNotBeDuplicatedWhenRegistered(request.UserForRegisterDto.Email);

				await _authBusinessRules.CheckPasswordConfirm(request.UserForRegisterDto);

				byte[] passwordHash, passwordSalt;
				HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);

				AppUser newUser = new()
				{
					FirstName = request.UserForRegisterDto.FirstName,
					LastName = request.UserForRegisterDto.LastName,
					Email = request.UserForRegisterDto.Email,
					ProfileImage = request.UserForRegisterDto.ProfileImage,
					PhoneNumber = request.UserForRegisterDto.PhoneNumber,
					PasswordHash = passwordHash,
					PasswordSalt = passwordSalt,
					Status = true,
				};
				AppUser createdAppUser = await _appUserRepository.AddAsync(newUser);

				AccessToken createdAccessToken = await _authService.CreateAccessToken(createdAppUser);
				RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdAppUser, request.IpAddress);
				RefreshToken addedRefreshToken = await _authService.AddRefreshTokenToDb(createdRefreshToken);

				RegisteredDto registeredDto = new()
				{
					RefreshToken = addedRefreshToken,
					AccessToken = createdAccessToken,
				};
				return registeredDto;
			}
		}
	}
}
