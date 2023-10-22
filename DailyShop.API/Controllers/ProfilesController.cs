using DailyShop.Business.Features.Addresses.Commands.DeleteAddress;
using DailyShop.Business.Features.Addresses.Commands.InsertAddress;
using DailyShop.Business.Features.Addresses.Commands.UpdateAddress;
using DailyShop.Business.Features.Addresses.Dtos;
using DailyShop.Business.Features.Addresses.Queries.GetListAddressByUserId;
using DailyShop.Business.Features.AppUsers.Commands.DeleteUser;
using DailyShop.Business.Features.AppUsers.Commands.UpdateUser;
using DailyShop.Business.Features.AppUsers.Dtos;
using DailyShop.Business.Features.Auths.DailyFrontends;
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
		//public async Task<IActionResult> Update([FromBody] UpdatedUserFrontendDto updatedUserFrontendDto)
		//{
		//	UpdateUserCommand updateUserCommand = new() { UpdatedUserFrontendDto = updatedUserFrontendDto };
		//	int userId =  await _mediator.Send(updateUserCommand);

		//	UpdateAddressCommand updateAddressCommand = new() { AppUserId = userId, City = updatedUserFrontendDto.addresses.city, Adres = updatedUserFrontendDto.addresses.address, Country = updatedUserFrontendDto.addresses.country, Description = updatedUserFrontendDto.addresses.description, Title = updatedUserFrontendDto.addresses.title, ZipCode = updatedUserFrontendDto.addresses.zipcode };

		//	await _mediator.Send(updateAddressCommand);
		//	return Ok(new { Message = "Kullanıcı başarıyla güncellendi." });
		//}
		[HttpDelete("Delete")]
		public async Task<IActionResult> Delete([FromQuery] DeletedUserFrontendDto deletedUserFrontendDto)
		{
			DeleteUserCommand deleteUserCommand = new() { DeletedUserFrontendDto = deletedUserFrontendDto };
			string message =  await _mediator.Send(deleteUserCommand);
			return Ok(new { Message = message });
		}
	}
}
