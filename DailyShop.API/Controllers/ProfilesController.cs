using DailyShop.Business.Features.Addresses.Commands.DeleteAddress;
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
		[HttpGet]
		public async Task<IActionResult> GetListByUserId([FromQuery] int id)
		{
			 var address = await _mediator.Send(new GetListAddressByUserIdQuery { Id = id });
			 return Ok(address);
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteAddress([FromQuery] int userId,string title)
		{
			DeletedAddressDto deletedAddressDto = await _mediator.Send(new DeleteAddressCommand { UserId = userId, Title = title });
			return Ok(deletedAddressDto);
		}
	}
}
