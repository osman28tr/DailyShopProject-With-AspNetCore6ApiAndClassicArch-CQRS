using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.API.Helpers;
using DailyShop.Business.Features.Products.Queries.GetListProductByUserId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [Area("Admin")]
    [ApiController]
    public class UsersController : BaseController
    {
        [HttpGet("{userId:int}/Products")]
        public async Task<IActionResult> GetProductByUserId(int userId)
        {
            var productValues = await Mediator?.Send(new GetListProductByUserIdQuery() { UserId = userId })!;
            if (productValues == null)
                throw new BusinessException("Bu kullanıcıya ait bir ürün bulunamadı veya kaldırıldı! ");
            return Ok(new { data = productValues, message = "Ürün verileri başarıyla getirildi." });
        }
    }
}
