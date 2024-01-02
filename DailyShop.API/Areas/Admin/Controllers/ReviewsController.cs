using DailyShop.API.Helpers;
using DailyShop.Business.Features.AppUsers.Queries.GetListUserByReport;
using DailyShop.Business.Features.Reviews.Commands.DeleteReportReview;
using DailyShop.Business.Features.Reviews.Queries.GetListReviewByReport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Areas.Admin.Controllers
{
    [Route("api/Admin/[controller]")]
    [Area("Admin")]
    [ApiController]
    [Authorize]
    public class ReviewsController : BaseController
    {
        private readonly ImageHelper _imageHelper;
        public ReviewsController(ImageHelper imageHelper)
        {
            _imageHelper = imageHelper;
        }
        [HttpGet("ReportedReviews")]
        public async Task<IActionResult> ReportedReviews()
        {
            var reportedReviews = await Mediator.Send(new GetListReviewByReportQuery());
            foreach (var reportedReview in reportedReviews)
            {
                if (reportedReview.ReporterUser.ProfileImage != null)
                    reportedReview.ReporterUser.ProfileImage = GetUserImageByHelper(reportedReview.ReporterUser.ProfileImage);
                if (reportedReview.Review.User.ProfileImage != null)
                    reportedReview.Review.User.ProfileImage = GetUserImageByHelper(reportedReview.Review.User.ProfileImage);
            }
            return Ok(new { Message = reportedReviews.Any() ? "Raporlanan yorumlar başarıyla getirildi." : "Raporlanan bir yorum bulunamadı.", data = reportedReviews });
        }
        [HttpDelete("ReportedReviews/{reportId:int}")]
        public async Task<IActionResult> ReportedReviews(int reportId)
        {
            await Mediator.Send(new DeleteReportReviewCommand() { ReportId = reportId });
            return Ok(new { Message = "Yoruma ait rapor başarıyla kaldırıldı." });
        }
        [NonAction]
        public string GetUserImageByHelper(string imageName)
        {
            string getImage = _imageHelper.GetImage(Request.Scheme, Request.Host, Request.PathBase, imageName, "UserImages");
            return getImage;
        }
    }
}
