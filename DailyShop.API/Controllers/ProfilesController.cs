using DailyShop.Business.Features.Addresses.Commands.DeleteAddress;
using DailyShop.Business.Features.Addresses.Commands.InsertAddress;
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
		[HttpDelete("DeleteAddress")]
		public async Task<IActionResult> DeleteAddress([FromQuery] int userId,string title)
		{
			DeletedAddressDto deletedAddressDto = await _mediator.Send(new DeleteAddressCommand { UserId = userId, Title = title });
			return Ok(deletedAddressDto);
		}
		[HttpPost("AddAddress")]
		public async Task<IActionResult> AddAddress([FromBody] InsertAddressCommand insertAddressCommand)
		{
			InsertedAddressDto insertedAddressDto = await _mediator.Send(insertAddressCommand);
			return Created("", insertedAddressDto);
		}
		[HttpPut("Update")]
		public async Task<IActionResult> Update([FromBody] UpdatedUserDto updatedUserDto)
		{
			var authorization = Request.Headers.Authorization;
			var token = authorization.ToString().Split(" ")[1];

			int userId =  _authService.VerifyToken(token);

			UpdateUserCommand updateUserCommand = new() { UpdatedUserDto = updatedUserDto, Id = userId };
			var updatedUser = await _mediator.Send(updateUserCommand);

			UpdateAddressCommand updateAddressCommand = new() { AddressDtos = updatedUserDto.Addresses, UserId = userId };

			var updatedAddress = await _mediator.Send(updateAddressCommand);

			UpdatedUserDto newUser = new() { Email = updatedUser.Email, FirstName = updatedUser.FirstName, LastName = updatedUser.LastName, PhoneNumber = updatedUser.PhoneNumber, ProfileImage = updatedUser.ProfileImage, Addresses = updatedAddress };

			return Ok(new { data = newUser, Message = "Kullanıcı Bilgileriniz Başarıyla Güncellendi." });
		}
		[HttpDelete("Delete")]
		public async Task<IActionResult> Delete([FromQuery] BlockedUserDto blockedUserDto)
		{
			BlockUserCommand blockUserCommand = new() { BlockedUserDto = blockedUserDto };
			string message =  await _mediator.Send(blockUserCommand);
			return Ok(new { Message = message });
		}
	}
}
