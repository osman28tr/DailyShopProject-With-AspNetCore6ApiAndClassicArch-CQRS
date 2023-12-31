using DailyShop.API.Helpers;
using DailyShop.Business.Features.Reviews.Commands.DeleteReview;
using DailyShop.Business.Features.Reviews.Commands.InsertReportReview;
using DailyShop.Business.Features.Reviews.Commands.InsertReview;
using DailyShop.Business.Features.Reviews.Commands.InsertReviewToReview;
using DailyShop.Business.Features.Reviews.Dtos;
using DailyShop.Business.Features.Reviews.Queries.GetListReviewByProductId;
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
        private readonly ImageHelper _imageHelper;
        public ReviewsController(IAuthService authService, ImageHelper imageHelper)
        {
            _authService = authService;
            _imageHelper = imageHelper;
        }
        [HttpGet("GetListByUserId")]
        public async Task<IActionResult> GetListByUserId([FromQuery] int userId)
        {
            var reviews = await Mediator.Send(new GetListReviewByUserIdQuery() { UserId = userId });
            reviews.ForEach(review =>
            {
                review.Product.BodyImage = GetImageByHelper(review.Product.BodyImage);
            });
            return Ok(new { message = "Kullanıcının yorumları getirildi.", data = reviews });
        }
        [HttpGet("GetReviewByProductId/{productId:int}")]
        public async Task<IActionResult> GetReviewByProductId(int productId)
        {
            var reviews = await Mediator.Send(new GetListReviewByProductIdQuery() { ProductId = productId });
            return Ok(new { Message = "Ürüne ait yorumlar getirildi.", data = reviews });
        }

        [HttpPost("AddReviewToProduct/{id}")]
        public async Task<IActionResult> AddReviewToProduct(int id, [FromBody] InsertedReviewDto insertedReviewDto)
        {
            int userId = _authService.VerifyToken(GetToken());
            await Mediator.Send(new InsertReviewCommand()
                { ProductId = id, UserId = userId, InsertedReviewDto = insertedReviewDto });
            return Ok(new { message = "Yorumunuz onaylanmak üzere sisteme gönderilmiştir." });
        }
        [HttpPost("AddReviewToReview/{id}")]
        public async Task<IActionResult> AddReviewToReview(int id, [FromBody] InsertedReviewToReviewDto insertedReviewToReviewDto)
        {
            int userId = _authService.VerifyToken(GetToken());
            await Mediator.Send(new InsertReviewToReviewCommand() { AppUserId = userId, ProductId = id, InsertedReviewToReview = insertedReviewToReviewDto });
            return Ok(new { Message = "Yorumunuz onaylanmak üzere sisteme gönderilmiştir." });
        }
        [HttpPost("ReportReview/{reviewId}")]
        public async Task<IActionResult> ReportReview(int reviewId, [FromBody] ReportReviewDto reportReviewDto)
        {
            int userId = _authService.VerifyToken(GetToken());

            await Mediator.Send(new ReportReviewCommand() { ReportReviewDto = reportReviewDto, ReviewId = reviewId, ReporterUserId = userId });

            return Ok(new { Message = "Bildiriminiz başarıyla alındı." });
        }
        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            await Mediator.Send(new DeleteReviewCommand() { ReviewId = reviewId });
            return Ok(new { Message = "Yorumunuz başarıyla kaldırıldı." });
        }
        [NonAction]
        public string GetImageByHelper(string imageName)
        {
            string getImage = _imageHelper.GetImage(Request.Scheme, Request.Host, Request.PathBase, imageName);
            return getImage;
        }
    }
}
