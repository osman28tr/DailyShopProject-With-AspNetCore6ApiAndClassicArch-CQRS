using DailyShop.Business.Features.ProductColors.Commands.InsertProductColor;
using DailyShop.Business.Features.ProductImages.Commands.InsertProductImage;
using DailyShop.Business.Features.Products.Commands.InsertProduct;
using DailyShop.Business.Features.Products.Dtos;
using DailyShop.Business.Features.Products.Models;
using DailyShop.Business.Features.Products.Queries.GetListProduct;
using DailyShop.Business.Features.ProductSizes.Commands.InsertProductSize;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var productValues = await _mediator.Send(new GetListProductQuery());
            return Ok(new { data = productValues, message = "Ürün verileri başarıyla getirildi." });
        }
        [HttpPost]
        public async Task<IActionResult> Add(InsertedProductDto insertedProductDto)
        {
            await _mediator.Send(new InsertProductCommand { InsertedProductDto = insertedProductDto });
            //await _mediator.Send(new InsertProductImageCommand { InsertedProductImageDto = insertedProductDto.ProductImages });
            //await _mediator.Send(new InsertProductColorCommand { InsertedProductColorDto = insertedProductDto.Colors });
            //await _mediator.Send(new InsertProductSizeCommand { InsertedProductSizeDto = insertedProductDto.Sizes });
            return Ok(new { message = "Ürün ekleme işlemi başarıyla gerçekleştirildi." });
        }
    }
}
