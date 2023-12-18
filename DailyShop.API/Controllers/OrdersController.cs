using DailyShop.API.Helpers;
using DailyShop.Business.Features.Orders.Commands.InsertOrder;
using DailyShop.Business.Features.Orders.Dtos;
using DailyShop.Business.Features.Orders.Queries.GetListOrderByUserId;
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
	    private readonly ImageHelper _imageHelper;

        public OrdersController(IAuthService authService,ImageHelper imageHelper)
        {
            _authService = authService;
            _imageHelper = imageHelper;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] InsertedOrderDto insertedOrderDto)
        {
            int userId = _authService.VerifyToken(GetToken());
            await Mediator.Send(new InsertOrderCommand() { InsertedOrderDto = insertedOrderDto, UserId = userId });
            return Ok(new { Message = "Siparişiniz başarıyla oluşturuldu." });
        }
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var userId = _authService.VerifyToken(GetToken());
            var order = await Mediator.Send(new GetListOrderByUserIdQuery() { UserId = userId });
            order.ForEach(x => x.GetListOrderItemByOrderViewModels.ToList().ForEach(y =>
            {
	            if (y.GetListProductByOrderViewModel?.BodyImage != null)
		            y.GetListProductByOrderViewModel.BodyImage =
			            GetImageByHelper(y.GetListProductByOrderViewModel.BodyImage);
            }));


            return Ok(new { Message = "Sipariş verileri başarıyla getirildi.", data = order });
        }

        [NonAction]
        public string GetImageByHelper(string imageName)
        {
	        string getImage = _imageHelper.GetImage(Request.Scheme, Request.Host, Request.PathBase, imageName);
	        return getImage;
        }
	}
}
