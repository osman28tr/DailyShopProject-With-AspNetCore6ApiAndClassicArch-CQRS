using DailyShop.API.Helpers;
using DailyShop.Business.Features.Categories.Commands.DeleteCategory;
using DailyShop.Business.Features.Categories.Commands.InsertCategory;
using DailyShop.Business.Features.Categories.Commands.UpdateCategory;
using DailyShop.Business.Features.Categories.Dtos;
using DailyShop.Business.Features.Categories.Models;
using DailyShop.Business.Features.Categories.Queries.GetListCategory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Areas.Admin.Controllers
{
    [Route("api/Admin/[controller]")]
    [Area("Admin")]
    [ApiController]
    [Authorize]
    public class CategoriesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
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
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] InsertedCategoryDto insertedCategoryDto)
        {
            var insertedCategory = await Mediator.Send(new InsertCategoryCommand { InsertedCategoryDto = insertedCategoryDto });
            return Ok(new { Message = $"{insertedCategory.Name} isimli kategori başarıyla eklendi." });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdatedCategoryDto updatedCategoryDto)
        {
            int id = GetIdByRequest();
            var updatedCategory = await Mediator.Send(new UpdateCategoryCommand { UpdatedCategoryDto = updatedCategoryDto, Id = id });
            return Ok(new { Message = $"{updatedCategory.Name} isimli kategori başarıyla güncellendi." });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete()
        {
            int id = GetIdByRequest();
            List<GetListCategoryDto> listCategory = await Mediator.Send(new GetListCategoryQuery());
            DeleteCategoryViewModel deleteCategoryViewModel = new DeleteCategoryViewModel();
            foreach (var category in listCategory)
            {
                if (category.Id == id)
                {
                      deleteCategoryViewModel= await Mediator.Send(new DeleteCategoryCommand { GetListCategoryDto = category });
                    break;
                }
            }
            //DeletedCategoryDto deletedCategoryDto = new() { Id = id };
            //var deletedCategory = await Mediator.Send(new DeleteCategoryCommand { DeletedCategoryDto = deletedCategoryDto });
            return Ok(new { Message = $"{deleteCategoryViewModel.Name} isimli kategori başarıyla silindi." });
        }
        private int GetIdByRequest()
        {
            return int.Parse(HttpContext.GetRouteData().Values["id"].ToString());
        }
    }
}
