using DailyShop.API.Helpers;
using DailyShop.Business.Features.Products.Commands.UpdateProductStatus;
using DailyShop.Business.Features.Products.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Areas.Admin.Controllers
{
    [Route("api/Admin/[controller]")]
    [Area("Admin")]
    [ApiController]
    [Authorize]
    public class ProductsController : BaseController
    {
        [HttpPut("UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(int id,bool status)
        {
            await Mediator.Send(new UpdateProductStatusCommand()
                { ProductId = id, Status = status });
            return Ok(new { Message = "Ürün durumu başarıyla güncellendi." });
        }
    }
}
