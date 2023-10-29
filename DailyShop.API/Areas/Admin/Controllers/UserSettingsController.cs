using DailyShop.Business.Features.AppUsers.Commands.BlockUser;
using DailyShop.Business.Features.AppUsers.Dtos;
using DailyShop.Business.Features.AppUsers.Queries.GetListUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Areas.Admin.Controllers
{
    [Route("api/Admin/[controller]")]
    [Area("Admin")]
    [ApiController]
    public class UserSettingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserSettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var users = await _mediator.Send(new GetListUserQuery());
            return Ok(users);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> BlockUser()
        {
            BlockedUserDto blockedUserDto = new() { Id = int.Parse(HttpContext.GetRouteData().Values["id"].ToString()) };
            var message = await _mediator.Send(new BlockUserCommand { BlockedUserDto = blockedUserDto });
            return Ok(message);
        }
    }
}
