using DailyShop.API.Helpers;
using DailyShop.Business.Features.Products.Queries.GetListProductByUserId;
using DailyShop.Business.Services.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Areas.Seller.Controllers
{
    [Route("api/Seller/[controller]")]
    [Area("Seller")]
    [ApiController]
    public class ProductsController : BaseTokenController
    {
        private readonly IAuthService _authService;
        private readonly ImageHelper _imageHelper;

        public ProductsController(IAuthService authService, ImageHelper imageHelper)
        {
            _authService = authService;
            _imageHelper = imageHelper;
        }
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            int userId = _authService.VerifyToken(GetToken());
            var products = await Mediator.Send(new GetListProductByUserIdQuery { UserId = userId });
            foreach (var product in products)
            {
                product.BodyImage = GetImageByHelper(product.BodyImage);
                if (product.ProductImages != null)
                {
                    product.ProductImages = product.ProductImages.Select(x =>
                                               GetImageByHelper(x)).ToList();
                }
            }
            return Ok(new { message = "ürün verileri getirildi.", data = products });
        }
        [NonAction]
        public string GetImageByHelper(string imageName)
        {
            string getImage = _imageHelper.GetImage(Request.Scheme, Request.Host, Request.PathBase, imageName);
            return getImage;
        }
    }
}
