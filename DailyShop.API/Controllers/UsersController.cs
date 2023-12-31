using DailyShop.API.Helpers;
using DailyShop.Business.Features.AppUsers.Commands.InsertReportUser;
using DailyShop.Business.Features.AppUsers.Dtos;
using DailyShop.Business.Services.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseTokenController
    {
        private readonly IAuthService _authService;
        public UsersController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("ReportUser/{reporterUserId:int}")]
        public async Task<IActionResult> ReportUser(int reporterUserId, [FromBody] ReportUserDto reportUserDto)
        {
            int userId = _authService.VerifyToken(GetToken());
            await Mediator.Send(new InsertReportUserCommand() { ReporterUserId = userId, UserId = reporterUserId, ReportUserDto = reportUserDto });
            return Ok(new { Message = "Bildiriminiz başarıyla gerçekleştirildi." });
        }
    }
}
