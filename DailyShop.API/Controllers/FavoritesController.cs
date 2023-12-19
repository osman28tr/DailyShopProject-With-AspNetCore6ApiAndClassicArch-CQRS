using DailyShop.API.Helpers;
using DailyShop.Business.Features.Favorites.Commands.DeleteFavorite;
using DailyShop.Business.Features.Favorites.Commands.InsertFavorite;
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

        public FavoritesController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("AddFavorite")]
        public async Task<IActionResult> AddFavorite([FromQuery]int productId)
        {
            int userId = _authService.VerifyToken(GetToken());
            await Mediator.Send(new InsertFavoriteCommand() { UserId = userId, ProductId = productId });
            return Ok(new { Message = "Seçtiğiniz ürün favorilerinize başarıyla eklendi." });
        }
        [HttpDelete("DeleteFavorite")]
        public async Task<IActionResult> DeleteFavorite([FromQuery] int productId)
        {
            int userId = _authService.VerifyToken(GetToken());
            await Mediator.Send(new DeleteFavoriteCommand() { UserId = userId, ProductId = productId });
            return Ok(new { Message = "Seçtiğiniz ürün favorilerinizden başarıyla kaldırıldı." });
        }
    }
}
