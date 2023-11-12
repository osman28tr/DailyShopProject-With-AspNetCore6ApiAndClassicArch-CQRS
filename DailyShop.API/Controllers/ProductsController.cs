using DailyShop.API.Helpers;
using DailyShop.Business.Features.Products.Commands.InsertProduct;
using DailyShop.Business.Features.Products.Dtos;
using DailyShop.Business.Features.Products.Queries.GetListProduct;
using DailyShop.Business.Services.AuthService;
using Microsoft.AspNetCore.Mvc;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Products.Queries.GetByIdProduct;
using DailyShop.Business.Features.Products.Queries.GetByIdProductDetail;

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
            // Return the result as a JSON response
            return Ok(new { data = productValues, message = "Ürün verileri başarıyla getirildi." });
            //return Ok(result);
        }
		[HttpPost]
        public async Task<IActionResult> Add([FromForm] InsertedProductDto insertedProductDto)
        {
            List<string> productImagesPath = new List<string>();
            //var userId = _authService.VerifyToken(GetToken());
            if (insertedProductDto.BodyImage != null && insertedProductDto.ProductImages.Any())
            {
                string bodyImageName = await AddProductImageToFile(insertedProductDto.BodyImage);
                //insertedProductDto.BodyImage = imageName;
                
                foreach (var productImage in insertedProductDto.ProductImages)
                {
                    string imageName = await AddProductImageToFile(productImage);
                    //productImage.Name = imageName;
                }
            }
            await Mediator?.Send(new InsertProductCommand { InsertedProductDto = insertedProductDto, UserId = 1, BodyImagePath = "", ProductImagesPath = productImagesPath })!;
            return Ok(new { message = "Ürün ekleme işlemi başarıyla gerçekleştirildi." });
        }
        [HttpGet("/{categoryId}/{isDeleteShow}")]
        public async Task<IActionResult> GetListByCategoryId(int categoryId, bool isDeleteShow)
        {
            var productValues = await Mediator.Send(new GetByIdProductQuery { CategoryId = categoryId, IsDeleted = isDeleteShow });
            return Ok(new { data = productValues, message = "Ürün verileri başarıyla getirildi." });
        }

        [HttpGet("{productId:int}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var productValues = await Mediator?.Send( new GetByIdProductDetailQuery() { ProductId = productId })!;
            if (productValues == null)
	            throw new BusinessException("Böyle bir ürün bulunamadı veya kaldırıldı! ");

            return Ok(new { data = productValues, message = "Ürün verileri başarıyla getirildi." });
        }
        private async Task<string> AddProductImageToFile(IFormFile imageFile)
        {
            string imageName = "";

            var resourcePath = Directory.GetCurrentDirectory();
            var extension = Path.GetExtension(imageFile.FileName);
            imageName = Guid.NewGuid() + extension;
            var saveLocation = resourcePath + "/wwwroot/ProductImages/" + imageName;
            var stream = new FileStream(saveLocation, FileMode.Create);
            await imageFile.CopyToAsync(stream);

            return imageName;
        }
    }
}
