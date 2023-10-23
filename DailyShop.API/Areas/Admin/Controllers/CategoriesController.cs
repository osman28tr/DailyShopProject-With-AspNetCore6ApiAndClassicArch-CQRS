using DailyShop.Business.Features.Categories.Queries.GetListCategory;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Areas.Admin.Controllers
{
    [Route("Admin/api/[controller]")]
    [Area("Admin")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new GetListCategoryQuery());
            return Ok(result);
        }
    }
}
