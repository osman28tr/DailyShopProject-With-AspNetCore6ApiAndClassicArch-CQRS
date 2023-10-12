using DailyShop.Business.Features.Addresses.Commands.DeleteAddress;
using DailyShop.Business.Features.Addresses.Commands.InsertAddress;
using DailyShop.Business.Features.Addresses.Commands.UpdateAddress;
using DailyShop.Business.Features.Addresses.Dtos;
using DailyShop.Business.Features.Addresses.Queries.GetListAddressByUserId;
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
		[HttpPut("UpdateAddress")]
		public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressCommand updateAddressCommand)
		{
			UpdatedAddressDto updatedAddressDto = await _mediator.Send(updateAddressCommand);
			return Ok(updatedAddressDto);
		}
	}
}
