using DailyShop.API.Helpers;
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
	public class ProfilesController : BaseTokenController
	{
		private readonly IAuthService _authService;

		public ProfilesController(IAuthService authService)
		{
			_authService = authService;
		}
		[HttpGet("GetListAddressByUserId")]
		public async Task<IActionResult> GetListAddressByUserId([FromQuery] int id)
		{
			 var address = await Mediator.Send(new GetListAddressByUserIdQuery { Id = id });
			 return Ok(address);
		}
		[HttpPut("Update")]
		public async Task<IActionResult> Update([FromBody] UpdatedUserDto updatedUserDto)
		{
			int userId = _authService.VerifyToken(GetToken());

			UpdateUserCommand updateUserCommand = new() { UpdatedUserDto = updatedUserDto, Id = userId };
			var updatedUser = await Mediator.Send(updateUserCommand);

			List<AddressDto> updatedAddress = new();
			foreach (var updateAddressCommand in updatedUserDto.Addresses
				         .Select(address => new UpdateAddressCommand() 
					         { AddressDto = address, UserId = userId, AddressId = address.Id }))
			{
				
				var address = await Mediator.Send(updateAddressCommand);
				updatedAddress.Add(address);
			}

			UpdatedUserDto newUser = new() { Email = updatedUser.Email, FirstName = updatedUser.FirstName, LastName = updatedUser.LastName, PhoneNumber = updatedUser.PhoneNumber, ProfileImage = updatedUser.ProfileImage, Addresses = updatedAddress };

			return Ok(new { data = newUser, Message = "Kullanıcı Bilgileriniz Başarıyla Güncellendi." });
		}

		[HttpPut("UpdateAddress")]
		public async Task<IActionResult> UpdateAddress([FromBody] AddressDto addressDto)
		{
			int userId = _authService.VerifyToken(GetToken());
			UpdateAddressCommand updateAddressCommand = new() { AddressDto = addressDto, UserId = userId, AddressId = addressDto.Id };
			var address = await Mediator.Send(updateAddressCommand);
			return Ok(new {data = address,Message = "Adresiniz Başarıyla Güncellendi." });
		}

		[HttpGet("Block")]
		public async Task<IActionResult> Block()
		{
			int id = _authService.VerifyToken(GetToken());
            BlockedUserDto blockedUserDto = new() { Id =  id};
			string message = await Mediator.Send(new BlockUserCommand { BlockedUserDto = blockedUserDto });
			return Ok(new { Message = message });
		}
	}
}
