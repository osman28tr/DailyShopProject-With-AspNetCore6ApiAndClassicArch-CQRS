using DailyShop.API.Helpers;
using DailyShop.Business.Features.Products.Commands.InsertProduct;
using DailyShop.Business.Features.Products.Dtos;
using DailyShop.Business.Features.Products.Models;
using DailyShop.Business.Features.Products.Queries.GetListProduct;
using DailyShop.Business.Services.AuthService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseTokenController
    {
        private readonly IAuthService _authService;

        public ProductsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var productValues = await Mediator.Send(new GetListProductQuery());
            return Ok(new { data = productValues, message = "Ürün verileri başarıyla getirildi." });
        }
        [HttpPost]
        public async Task<IActionResult> Add(InsertedProductDto insertedProductDto)
        {
            int userId = _authService.VerifyToken(GetToken());
            await Mediator.Send(new InsertProductCommand { InsertedProductDto = insertedProductDto, UserId = userId });
            return Ok(new { message = "Ürün ekleme işlemi başarıyla gerçekleştirildi." });
        }
    }
}
