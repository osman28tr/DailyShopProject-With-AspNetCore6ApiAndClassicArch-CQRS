using DailyShop.Business.Features.Reviews.Queries.GetListReviewByUserId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetListByUserId")]
        public async Task<IActionResult> GetListByUserId([FromQuery] GetListReviewByUserIdQuery getListReviewByUserIdQuery)
        {
            var reviews = await _mediator.Send(getListReviewByUserIdQuery);
            return Ok(reviews);
        }
    }
}
