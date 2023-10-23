using DailyShop.Business.Features.AppUsers.Queries.GetListUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Areas.Admin.Controllers
{
    [Route("Admin/api/[controller]")]
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
    }
}
