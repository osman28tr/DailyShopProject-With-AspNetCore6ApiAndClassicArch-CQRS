using DailyShop.API.Helpers;
using DailyShop.Business.Features.Products.Commands.InsertProduct;
using DailyShop.Business.Features.Products.Dtos;
using DailyShop.Business.Features.Products.Queries.GetListProduct;
using DailyShop.Business.Services.AuthService;
using Microsoft.AspNetCore.Mvc;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Products.Commands.DeleteProduct;
using DailyShop.Business.Features.Products.Models;
using DailyShop.Business.Features.Products.Queries.GetListProductByCategoryAndIsDelete;
using DailyShop.Business.Features.Products.Queries.GetProductDetailById;
using DailyShop.Business.Features.Products.Queries.GetListProductByUserId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using DailyShop.Business.Features.Products.Commands.UpdateProduct;
using DailyShop.Entities.Concrete;
using DailyShop.Business.Features.Categories.Queries.GetListCategory;

namespace DailyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseTokenController
    {
        private readonly IAuthService _authService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ImageHelper _imageHelper;
        public ProductsController(IAuthService authService, IWebHostEnvironment webHostEnvironment, ImageHelper imageHelper)
        {
            _authService = authService;
            _webHostEnvironment = webHostEnvironment;
            _imageHelper = imageHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var productValues = await Mediator.Send(new GetListProductQuery());
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
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] InsertedProductDto insertedProductDto)
        {
            List<string> productImagesPath = new List<string>();
            string bodyImagePath = "";
            //var userId = _authService.VerifyToken(GetToken());
            if (insertedProductDto.BodyImage != null && insertedProductDto.ProductImages.Any())
            {
                bodyImagePath = await AddProductImageToFile(insertedProductDto.BodyImage);
                //insertedProductDto.BodyImage = imageName;

                foreach (var productImage in insertedProductDto.ProductImages)
                {
                    string imageName = await AddProductImageToFile(productImage);
                    productImagesPath.Add(imageName);
                }
            }
            int userId = _authService.VerifyToken(GetToken());
            await Mediator?.Send(new InsertProductCommand
            {
                InsertedProductDto = insertedProductDto,
                UserId = userId,
                BodyImagePath = bodyImagePath,
                ProductImagesPath = productImagesPath
            })!;
            return Ok(new { message = "Ürün ekleme işlemi başarıyla gerçekleştirildi." });
        }
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetListByCategoryId(int categoryId, [FromQuery] bool isDeleteShow)
        {
            int userId = 0;
            if (GetToken() != null)
            {
                userId = _authService.VerifyToken(GetToken());
            }
            var categoryList = await Mediator.Send(new GetListCategoryQuery());
            var productValues = await Mediator.Send(new GetListProductByCategoryAndIsDeleteQuery
            { CategoryId = categoryId, IsDeleted = isDeleteShow, UserId = userId, GetListCategoryDtos = categoryList });
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

        [HttpGet("{productId:int}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            int userId = 0;
            if (GetToken() != null)
            {
                userId = _authService.VerifyToken(GetToken());
            }

            var productValues = await Mediator?.Send(new GetProductDetailByIdQuery()
            { ProductId = productId, UserId = userId })!;

            productValues.BodyImage = GetImageByHelper(productValues.BodyImage);
            if (productValues.ProductImages != null)
            {
                productValues.ProductImages = productValues.ProductImages.Select(x =>
                    GetImageByHelper(x)).ToList();
            }

            return Ok(new { data = productValues, message = "Ürün verileri başarıyla getirildi." });
        }
        [HttpGet("GetListProductByUser")]
        public async Task<IActionResult> GetListProductByUser()
        {
            int userId = _authService.VerifyToken(GetToken());
            var products = await Mediator.Send(new GetListProductByUserIdQuery { UserId = userId });
            foreach (var product in products)
            {
                product.BodyImage = GetImageByHelper(product.BodyImage);
                if (product.ProductImages != null)
                {
                    product.ProductImages = product.ProductImages.Select(x =>
                        GetImageByHelper(x)).ToList();
                }
            }
            return Ok(new { message = "ürün verileri getirildi.", data = products });
        }

        [HttpDelete("DeleteProduct/{productId:int}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var deletedProduct = await Mediator?.Send(new DeleteProductCommand() { ProductId = productId })!;
            return Ok(new { message = $"{deletedProduct.Name} adlı ürün başarıyla silindi." });
        }
        [HttpPut("{productId:int}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromForm] UpdatedProductDto updatedProductDto)
        {
            // Request.Form["images"] senden bana gelip benim sana tekrar gönderdiğim images ler
            // Request.Form.Files ise Benim sana gönderdiğim image/imagesler ( Name' e göre ayırıyor)
            // Request.Form.Files ın içinde BodyImage varsa onu alıp güncelleme işlemi yap
            // yoksa eskisiyle devam et.

            List<string> productImagesPath = new List<string>();
            string bodyImagePath = "";

            if (updatedProductDto.ProductImagesFile != null)
            {
                foreach (var productImage in updatedProductDto.ProductImagesFile)
                {
                    string imageName = await AddProductImageToFile(productImage);
                    productImagesPath.Add(imageName);
                }
            }
            if (updatedProductDto.ProductImages != null)
            {
                foreach (var image in updatedProductDto.ProductImages)
                {
                    string restoredImage = image.Split("/")[^1];
                    productImagesPath.Add(restoredImage);
                }
            }
            if (updatedProductDto.BodyImage != null)
            {
                bodyImagePath = await AddProductImageToFile(updatedProductDto.BodyImage);
            }
            else
            {
                bodyImagePath = Request.Form["BodyImage"];
                bodyImagePath = bodyImagePath.Split("/")[^1];
            }
            await Mediator?.Send(new UpdateProductCommand
            {
                UpdatedProductDto = updatedProductDto,
                ProductImagesPath = productImagesPath,
                BodyImagePath = bodyImagePath,
                ProductId = productId
            })!;
            return Ok(new { Message = "Ürününüz başarıyla güncellendi." });
        }
        private async Task<string> AddProductImageToFile(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/ProductImages", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }
        [NonAction]
        public string GetImageByHelper(string imageName)
        {
            string getImage = _imageHelper.GetImage(Request.Scheme, Request.Host, Request.PathBase, imageName);
            return getImage;
        }
    }
}
