using DailyShop.API.Helpers;
using DailyShop.Business.Features.Orders.Queries.GetListOrderByUserId;
using DailyShop.Business.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Areas.Admin.Controllers
{
    [Route("api/Admin/[controller]")]
    [Area("Admin")]
    [ApiController]
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly ImageHelper _imageHelper;
        public OrdersController(ImageHelper imageHelper)
        {
            _imageHelper = imageHelper;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetListByUser(int userId)
        {
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
