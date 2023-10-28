using DailyShop.Business.Features.Categories.Commands.InsertCategory;
using DailyShop.Business.Features.Categories.Commands.UpdateCategory;
using DailyShop.Business.Features.Categories.Dtos;
using DailyShop.Business.Features.Categories.Queries.GetListCategory;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
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
            List<GetListCategoryDto> getListCategoryDtos = new List<GetListCategoryDto>();
            var result = await _mediator.Send(new GetListCategoryQuery());

            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].ParentCategoryId == null)
                {
                    getListCategoryDtos.Add(result[i]);
                }
            }
            return Ok(getListCategoryDtos);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] InsertedCategoryDto insertedCategoryDto)
        {
            var insertedCategory = _mediator.Send(new InsertCategoryCommand { InsertedCategoryDto = insertedCategoryDto });
            return Ok(new { Message = $"{insertedCategory.Result.Name} isimli kategori başarıyla eklendi." });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdatedCategoryDto updatedCategoryDto)
        {
            int id = int.Parse(HttpContext.GetRouteData().Values["id"].ToString());
            var updatedCategory = _mediator.Send(new UpdateCategoryCommand { UpdatedCategoryDto = updatedCategoryDto, Id = id });
            return Ok(new { Message = $"{updatedCategory.Result.Name} isimli kategori başarıyla güncellendi." });
        }
    }
}
