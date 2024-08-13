using DailyShop.API.Helpers;
using DailyShop.Business.Features.Favorites.Commands.DeleteFavorite;
using DailyShop.Business.Features.Favorites.Commands.InsertFavorite;
using DailyShop.Business.Features.Favorites.Queries.GetListFavoriteByUserId;
using DailyShop.Business.Services.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : BaseTokenController
    {
        private readonly IAuthService _authService;
        private readonly ImageHelper _imageHelper;

        public FavoritesController(IAuthService authService, ImageHelper imageHelper)
        {
            _authService = authService;
            _imageHelper = imageHelper;
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite([FromQuery]int productId)
        {
            int userId = _authService.VerifyToken(GetToken());
            await Mediator.Send(new InsertFavoriteCommand() { UserId = userId, ProductId = productId });
            return Ok(new { Message = "Seçtiğiniz ürün favorilerinize başarıyla eklendi." });
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteFavorite([FromQuery] int favoriteId)
        {
            int userId = _authService.VerifyToken(GetToken());
            await Mediator.Send(new DeleteFavoriteCommand() { FavoriteId = favoriteId });
            return Ok(new { Message = "Seçtiğiniz ürün favorilerinizden başarıyla kaldırıldı." });
        }
        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            int userId = _authService.VerifyToken(GetToken());
            var favoriteList = await Mediator.Send(new GetListFavoriteByUserIdQuery() { UserId = userId });
            foreach (var favorite in favoriteList)
            {
                favorite.Product.BodyImage = GetImageByHelper(favorite.Product.BodyImage);
            }
            return Ok(new { Message = "Favori verileri başarıyla getirildi.", data = favoriteList });
        }
        [NonAction]
        public string GetImageByHelper(string imageName)
        {
            string getImage = _imageHelper.GetImage(Request.Scheme, Request.Host, Request.PathBase, imageName);
            return getImage;
        }
    }
}
