using DailyShop.API.Helpers;
using DailyShop.Business.Features.Reviews.Queries.GetListReviewByUserId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : BaseController
    {
        [HttpGet("GetListByUserId")]
        public async Task<IActionResult> GetListByUserId([FromQuery] GetListReviewByUserIdQuery getListReviewByUserIdQuery)
        {
            var reviews = await Mediator.Send(getListReviewByUserIdQuery);
            return Ok(reviews);
        }
    }
}
