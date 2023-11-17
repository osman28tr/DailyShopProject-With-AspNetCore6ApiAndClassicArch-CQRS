using DailyShop.API.Helpers;
using DailyShop.Business.Features.Carts.Commands.InsertCart;
using DailyShop.Business.Features.Carts.Dtos;
using DailyShop.Business.Features.Carts.Queries.GetListCartByUser;
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
        private readonly ImageHelper _imageHelper;

        public CartsController(IAuthService authService, ImageHelper imageHelper)
        {
            _authService = authService;
            _imageHelper = imageHelper;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] List<InsertedCartItemDto> cartItemDtos, [FromQuery] int productId)
        {
            int userId = _authService.VerifyToken(GetToken());
            await Mediator.Send(new InsertCartCommand { InsertedCartItemDtos = cartItemDtos, UserId = userId, ProductId = productId });
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetListByUser()
        {
            int userId = _authService.VerifyToken(GetToken());
            var cartItems = await Mediator.Send(new GetListCartByUserQuery() { UserId = userId });
            foreach (var cartItem in cartItems)
            {
                cartItem.BodyImage = GetImageByHelper(cartItem.BodyImage);
                if (cartItem.ProductImages != null)
                {
                    cartItem.ProductImages = cartItem.ProductImages.Select(x =>
                        GetImageByHelper(x)).ToList();
                }
            }
            return Ok(new { message = "Sepet verileri getirildi.", data = cartItems });
        }
        [NonAction]
        public string GetImageByHelper(string imageName)
        {
            string getImage = _imageHelper.GetImage(Request.Scheme, Request.Host, Request.PathBase, imageName);
            return getImage;
        }
    }
}
