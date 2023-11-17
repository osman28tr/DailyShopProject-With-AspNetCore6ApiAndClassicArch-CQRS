using DailyShop.API.Helpers;
using DailyShop.Business.Features.Carts.Commands.InsertCart;
using DailyShop.Business.Features.Carts.Dtos;
using DailyShop.Business.Services.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : BaseTokenController
    {
        private readonly IAuthService _authService;

        public CartsController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] List<InsertedCartItemDto> cartItemDtos, [FromQuery] int productId)
        {
            int userId = _authService.VerifyToken(GetToken());
            await Mediator.Send(new InsertCartCommand { InsertedCartItemDtos = cartItemDtos, UserId = userId, ProductId = productId });
            return Ok();
        }
    }
}
