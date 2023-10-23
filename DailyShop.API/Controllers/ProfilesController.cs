using DailyShop.Business.Features.Addresses.Commands.DeleteAddress;
using DailyShop.Business.Features.Addresses.Commands.InsertAddress;
using DailyShop.Business.Features.Addresses.Commands.UpdateAddress;
using DailyShop.Business.Features.Addresses.Dtos;
using DailyShop.Business.Features.Addresses.Queries.GetListAddressByUserId;
using DailyShop.Business.Features.AppUsers.Commands.DeleteUser;
using DailyShop.Business.Features.AppUsers.Commands.UpdateUser;
using DailyShop.Business.Features.AppUsers.Dtos;
using DailyShop.Business.Features.Auths.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ProfilesController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ProfilesController(IMediator mediator)
		{
			_mediator = mediator;
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
			UpdateUserCommand updateUserCommand = new() { UpdatedUserDto = updatedUserDto };
			int userId = await _mediator.Send(updateUserCommand);

			UpdateAddressCommand updateAddressCommand = new() { AppUserId = userId, City = updatedUserDto.Addresses.City, Adres = updatedUserDto.Addresses.Address, Country = updatedUserDto.Addresses.Country, Description = updatedUserDto.Addresses.Description, Title = updatedUserDto.Addresses.Title, ZipCode = updatedUserDto.Addresses.Zipcode };

			await _mediator.Send(updateAddressCommand);
			return Ok(new { Message = "Kullanıcı başarıyla güncellendi." });
		}
		[HttpDelete("Delete")]
		public async Task<IActionResult> Delete([FromQuery] DeletedUserDto deletedUserDto)
		{
			DeleteUserCommand deleteUserCommand = new() { DeletedUserDto = deletedUserDto };
			string message =  await _mediator.Send(deleteUserCommand);
			return Ok(new { Message = message });
		}
	}
}
