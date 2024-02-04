using DailyShop.API.Helpers;
using DailyShop.Business.Features.Categories.Dtos;
using DailyShop.Business.Features.Categories.Queries.GetListCategory;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            List<GetListCategoryDto> getListCategoryDtos = new List<GetListCategoryDto>();
            var result = await Mediator.Send(new GetListCategoryQuery());
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].ParentCategoryId == null)
                {
                    getListCategoryDtos.Add(result[i]);
                }
            }
            return Ok(getListCategoryDtos);
        }
    }
}
