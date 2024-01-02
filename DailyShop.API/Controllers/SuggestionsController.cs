using DailyShop.API.Helpers;
using DailyShop.API.Models;
using DailyShop.Business.Features.Products.Queries.GetListProductBySuggestion;
using DailyShop.Business.Services.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionsController : BaseTokenController
    {
        private readonly IAuthService _authService;
        private readonly ImageHelper _imageHelper;
        public SuggestionsController(IAuthService authService, ImageHelper imageHelper)
        {
            _authService = authService;
            _imageHelper = imageHelper;
        }
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] List<ProductCookieViewModel> productCookie)
        {
            int userId = _authService.VerifyToken(GetToken());
            var suggestionProducts = await Mediator.Send(new GetListProductBySuggestionQuery() { UserId = userId });
            if(suggestionProducts.Count > 0)
            {
                foreach (var suggestionProduct in suggestionProducts)
                {
                    suggestionProduct.BodyImage = GetImageByHelper(suggestionProduct.BodyImage);
                    if (suggestionProduct.ProductImages != null)
                    {
                        suggestionProduct.ProductImages = suggestionProduct.ProductImages.Select(x =>
                            GetImageByHelper(x)).ToList();
                    }
                }
                return Ok(new { Message = "Öneri ürünleri başarıyla getirildi.", data = suggestionProducts });
            }
            return Ok(new { Message = "Öneri ürünleri bulunamadı.", data = suggestionProducts });
        }
        [NonAction]
        public string GetImageByHelper(string imageName)
        {
            string getImage = _imageHelper.GetImage(Request.Scheme, Request.Host, Request.PathBase, imageName);
            return getImage;
        }
    }
}
