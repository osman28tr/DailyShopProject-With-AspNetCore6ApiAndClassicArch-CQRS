using DailyShop.Business.Features.Addresses.Commands.UpdateAddress;
using DailyShop.Business.Features.Addresses.Dtos;
using DailyShop.Business.Features.Addresses.Queries.GetListAddressByUserId;
using DailyShop.Business.Features.AppUsers.Commands.BlockUser;
using DailyShop.Business.Features.AppUsers.Commands.UpdateUser;
using DailyShop.Business.Features.AppUsers.Dtos;
using DailyShop.Business.Features.Auths.Dtos;
using DailyShop.Business.Services.AuthService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ProfilesController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IAuthService _authService;

		public ProfilesController(IMediator mediator,IAuthService authService)
		{
			_mediator = mediator;
			_authService = authService;
		}
		[HttpGet("GetListAddressByUserId")]
		public async Task<IActionResult> GetListAddressByUserId([FromQuery] int id)
		{
			 var address = await _mediator.Send(new GetListAddressByUserIdQuery { Id = id });
			 return Ok(address);
		}
		[HttpPut("Update")]
		public async Task<IActionResult> Update([FromBody] UpdatedUserDto updatedUserDto)
		{
			int userId = _authService.VerifyToken(GetToken());

			UpdateUserCommand updateUserCommand = new() { UpdatedUserDto = updatedUserDto, Id = userId };
			var updatedUser = await _mediator.Send(updateUserCommand);

			UpdateAddressCommand updateAddressCommand = new() { AddressDtos = updatedUserDto.Addresses, UserId = userId };

			var updatedAddress = await _mediator.Send(updateAddressCommand);

			UpdatedUserDto newUser = new() { Email = updatedUser.Email, FirstName = updatedUser.FirstName, LastName = updatedUser.LastName, PhoneNumber = updatedUser.PhoneNumber, ProfileImage = updatedUser.ProfileImage, Addresses = updatedAddress };

			return Ok(new { data = newUser, Message = "Kullanıcı Bilgileriniz Başarıyla Güncellendi." });
		}
		[HttpGet("Block")]
		public async Task<IActionResult> Block()
		{
			int id = _authService.VerifyToken(GetToken());
            BlockedUserDto blockedUserDto = new() { Id =  id};
			string message = await _mediator.Send(new BlockUserCommand { BlockedUserDto = blockedUserDto });
			return Ok(new { Message = message });
		}
		private string GetToken()
		{
            var authorization = Request.Headers.Authorization;
            var token = authorization.ToString().Split(" ")[1];
			return token;
        }
	}
}
