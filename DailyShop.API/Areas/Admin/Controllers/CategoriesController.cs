using DailyShop.Business.Features.Categories.Commands.DeleteCategory;
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
    [Route("api/Admin/[controller]")]
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
            var insertedCategory = await _mediator.Send(new InsertCategoryCommand { InsertedCategoryDto = insertedCategoryDto });
            return Ok(new { Message = $"{insertedCategory.Name} isimli kategori başarıyla eklendi." });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdatedCategoryDto updatedCategoryDto)
        {
            int id = GetIdByRequest();
            var updatedCategory = await _mediator.Send(new UpdateCategoryCommand { UpdatedCategoryDto = updatedCategoryDto, Id = id });
            return Ok(new { Message = $"{updatedCategory.Name} isimli kategori başarıyla güncellendi." });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete()
        {
            int id = GetIdByRequest();
            DeletedCategoryDto deletedCategoryDto = new() { Id = id };
            var deletedCategory = await _mediator.Send(new DeleteCategoryCommand { DeletedCategoryDto = deletedCategoryDto });
            return Ok(new { Message = $"{deletedCategory.Name} isimli kategori başarıyla silindi." });
        }
        private int GetIdByRequest()
        {
            return int.Parse(HttpContext.GetRouteData().Values["id"].ToString());
        }
    }
}
