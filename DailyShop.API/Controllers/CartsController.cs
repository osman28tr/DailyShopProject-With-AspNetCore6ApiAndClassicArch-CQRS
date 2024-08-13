using DailyShop.API.Helpers;
using DailyShop.Business.Features.Carts.Commands.DeleteCart;
using DailyShop.Business.Features.Carts.Commands.InsertCart;
using DailyShop.Business.Features.Carts.Commands.UpdateCart;
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
        public async Task<IActionResult> Add(InsertedCartItemDto cartItemDto, [FromQuery] int productId)
        {
            int userId = _authService.VerifyToken(GetToken());
            await Mediator.Send(new InsertCartCommand { InsertedCartItemDto = cartItemDto, UserId = userId, ProductId = productId });
            return Ok("Ürün başarıyla sepete eklendi.");
        }

        [HttpGet]
        public async Task<IActionResult> GetListByUser()
        {
            int userId = _authService.VerifyToken(GetToken());
            var cartItems = await Mediator.Send(new GetListCartByUserQuery() { UserId = userId });
            foreach (var cartItem in cartItems)
            {
                cartItem.Product.BodyImage = GetImageByHelper(cartItem.Product.BodyImage);
            }
            return Ok(new { message = "Sepet verileri getirildi.", data = cartItems });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatedCartItemDto updatedCartItemDto)
        {
            int userId = _authService.VerifyToken(GetToken());
            await Mediator.Send(new UpdateCartCommand()
                { UpdatedCartItemDto = updatedCartItemDto, CartItemId = id, UserId = userId });
            return Ok(new { Message = "Sepetiniz başarıyla güncellendi." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int userId = _authService.VerifyToken(GetToken());
            await Mediator.Send(new DeleteCartCommand() { UserId = userId, CartItemId = id });
            return Ok(new { Message = "İlgili ürün sepetinizden başarıyla kaldırıldı." });
        }
        [NonAction]
        public string GetImageByHelper(string imageName)
        {
            string getImage = _imageHelper.GetImage(Request.Scheme, Request.Host, Request.PathBase, imageName);
            return getImage;
        }
    }
}
