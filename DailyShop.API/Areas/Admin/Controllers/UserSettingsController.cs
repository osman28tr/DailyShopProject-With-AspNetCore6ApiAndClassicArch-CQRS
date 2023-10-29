using DailyShop.API.Helpers;
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
    public class UserSettingsController : BaseController
    {
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var users = await Mediator.Send(new GetListUserQuery());
            return Ok(users);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> BlockUser()
        {
            BlockedUserDto blockedUserDto = new() { Id = int.Parse(HttpContext.GetRouteData().Values["id"].ToString()) };
            var message = await Mediator.Send(new BlockUserCommand { BlockedUserDto = blockedUserDto });
            return Ok(message);
        }
    }
}
