using DailyShop.API.Helpers;
using DailyShop.Business.Features.Wallets.Commands.InsertBalance;
using DailyShop.Business.Features.Wallets.Dtos;
using DailyShop.Business.Services.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : BaseTokenController
    {
        private readonly IAuthService _authService;
        public WalletsController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("AddMoneyToWallet")]
        public async Task<IActionResult> AddMoneyToWallet([FromBody] InsertedBalanceDto insertedBalanceDto)
        {
            int userId = _authService.VerifyToken(GetToken());
            await Mediator.Send(new InsertBalanceCommand() { UserId = userId, InsertedBalanceDto = insertedBalanceDto });
            return Ok(new { Message = "Cüzdanınıza bakiye yükleme işlemi başarıyla gerçekleştirildi." });
        }
    }
}
