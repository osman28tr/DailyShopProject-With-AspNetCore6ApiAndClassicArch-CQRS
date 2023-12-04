using DailyShop.API.Helpers;
using DailyShop.Business.Features.Reviews.Commands.InsertReview;
using DailyShop.Business.Features.Reviews.Dtos;
using DailyShop.Business.Features.Reviews.Queries.GetListReviewByUserId;
using DailyShop.Business.Services.AuthService;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : BaseTokenController
    {
        private readonly IAuthService _authService;
        public ReviewsController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet("GetListByUserId")]
        public async Task<IActionResult> GetListByUserId([FromQuery] GetListReviewByUserIdQuery getListReviewByUserIdQuery)
        {
            var reviews = await Mediator.Send(getListReviewByUserIdQuery);
            return Ok(reviews);
        }

        [HttpPost("AddReviewToProduct/{id}")]
        public async Task<IActionResult> AddReviewToProduct(int id, [FromBody] InsertedReviewDto insertedReviewDto)
        {
            int userId = _authService.VerifyToken(GetToken());
            await Mediator.Send(new InsertReviewCommand()
                { ProductId = id, UserId = userId, InsertedReviewDto = insertedReviewDto });
            return Ok("Yorumunuz onaylanmak üzere sisteme gönderilmiştir.");
        }
    }
}
