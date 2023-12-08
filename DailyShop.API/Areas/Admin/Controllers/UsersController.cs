using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.API.Helpers;
using DailyShop.Business.Features.Products.Queries.GetListProductByUserId;
using DailyShop.Entities.Concrete;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Areas.Admin.Controllers
{
    [Route("api/Admin/[controller]")]
    [Area("Admin")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly ImageHelper _imageHelper;

        public UsersController(ImageHelper imageHelper)
        {
            _imageHelper = imageHelper;
        }
        [HttpGet("{userId:int}/Products")]
        public async Task<IActionResult> GetProductByUserId(int userId)
        {
            var productValues = await Mediator?.Send(new GetListProductByUserIdQuery() { UserId = userId })!;
            if (productValues == null)
                throw new BusinessException("Bu kullanıcıya ait bir ürün bulunamadı veya kaldırıldı! ");
            foreach (var product in productValues)
            {
                product.BodyImage = GetImageByHelper(product.BodyImage);
                if (product.ProductImages != null)
                {
                    product.ProductImages = product.ProductImages.Select(x =>
                        GetImageByHelper(x)).ToList();
                }
            }
            return Ok(new { data = productValues, message = "Ürün verileri başarıyla getirildi." });
        }
        [NonAction]
        public string GetImageByHelper(string imageName)
        {
            string getImage = _imageHelper.GetImage(Request.Scheme, Request.Host, Request.PathBase, imageName);
            return getImage;
        }
    }
}
