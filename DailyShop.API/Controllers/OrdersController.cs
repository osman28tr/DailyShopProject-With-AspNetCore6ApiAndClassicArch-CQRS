using DailyShop.API.Helpers;
using DailyShop.Business.Features.Orders.Commands.InsertOrder;
using DailyShop.Business.Features.Orders.Dtos;
using DailyShop.Business.Features.Payments.Commands.InsertPayment;
using DailyShop.Business.Services.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseTokenController
    {
        private readonly IAuthService _authService;

        public OrdersController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] InsertedOrderDto insertedOrderDto)
        {
            int userId = _authService.VerifyToken(GetToken());
            await Mediator.Send(new InsertOrderCommand() { InsertedOrderDto = insertedOrderDto, UserId = userId });
            await Mediator.Send(new InsertPaymentCommand() { InsertedPaymentDto = insertedOrderDto.InsertedCreditCardDto, UserId = userId });
            return Ok(new { Message = "Siparişiniz başarıyla oluşturuldu." });
        }
    }
}
