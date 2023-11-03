using DailyShop.API.Helpers;
using DailyShop.Business.Features.Products.Commands.InsertProduct;
using DailyShop.Business.Features.Products.Dtos;
using DailyShop.Business.Features.Products.Models;
using DailyShop.Business.Features.Products.Queries.GetListProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        //deneme
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var productValues = await Mediator.Send(new GetListProductQuery());
            return Ok(new { data = productValues, message = "Ürün verileri başarıyla getirildi." });
        }
        [HttpPost]
        public async Task<IActionResult> Add(InsertedProductDto insertedProductDto)
        {
            await Mediator.Send(new InsertProductCommand { InsertedProductDto = insertedProductDto });
            return Ok(new { message = "Ürün ekleme işlemi başarıyla gerçekleştirildi." });
        }
    }
}
