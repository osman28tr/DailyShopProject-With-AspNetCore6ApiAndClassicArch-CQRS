using DailyShop.API.Helpers;
using DailyShop.Business.Features.Products.Commands.InsertProduct;
using DailyShop.Business.Features.Products.Dtos;
using DailyShop.Business.Features.Products.Models;
using DailyShop.Business.Features.Products.Queries.GetListProduct;
using DailyShop.Business.Services.AuthService;
using MediatR;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection.Metadata;

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
            string bodyImagePath = Directory.GetCurrentDirectory() + "/wwwroot/ProductImages/" + productValues.Last().BodyImage;
            IFormFile file = null;
            if (System.IO.File.Exists(bodyImagePath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(bodyImagePath);
                var fileStream = new MemoryStream(fileBytes);

                file = new FormFile(fileStream, 0, fileBytes.Length, "File", Path.GetFileName(bodyImagePath));
                //productValues.Last().BodyImageFile = file;
                var asd = File(fileStream, "application/octet-stream", file.FileName) ;
                var response = new
                {
                    message = productValues,
                    fileresponse = new
                    {
                        filename = file.FileName,
                        contenttype = file.ContentType,
                        content = Convert.ToBase64String(fileBytes)
                    }
                };
                return Ok(response);
            }
            //return Ok(new { data = productValues, message = "Ürün verileri başarıyla getirildi." });
            return NotFound();   
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] InsertedProductDto insertedProductDto)
        {
            string imageName = "";
            List<string> productImagesPath = new List<string>();
            //var userId = _authService.VerifyToken(GetToken());
            if (insertedProductDto.BodyImage != null && insertedProductDto.ProductImages.Any())
            {
                var resourcePath = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(insertedProductDto.BodyImage.FileName);
                imageName = Guid.NewGuid() + extension;
                var saveLocation = resourcePath + "/wwwroot/ProductImages/" + imageName;
                var stream = new FileStream(saveLocation, FileMode.Create);
                await insertedProductDto.BodyImage.CopyToAsync(stream);
                //insertedProductDto.BodyImage = imageName;
                for (int i = 0; i < insertedProductDto.ProductImages.Count; i++)
                {
                    var productImageExtension = Path.GetExtension(insertedProductDto.ProductImages[i].FileName);
                    var imageNameImage = Guid.NewGuid() + productImageExtension;
                    var saveLocationImage = resourcePath + "/wwwroot/ProductImages/" + imageNameImage;
                    var streamImage = new FileStream(saveLocationImage, FileMode.Create);
                    await insertedProductDto.ProductImages[i].CopyToAsync(streamImage);
                    productImagesPath.Add(imageNameImage);
                }
            }
            await Mediator?.Send(new InsertProductCommand { InsertedProductDto = insertedProductDto, UserId = 1, BodyImagePath = imageName, ProductImagesPath = productImagesPath })!;
            return Ok(new { message = "Ürün ekleme işlemi başarıyla gerçekleştirildi." });
        }

        [HttpGet("{categoryId}/{isDeleteShow}")]
        public async Task<IActionResult> GetListByCategoryId(int categoryId, bool isDeleteShow)
        {
            //var productValues = await Mediator.Send(new GetListProductQuery { CategoryId = categoryId, IsDeleteShow = isDeleteShow });
            return Ok(new { data = "productValues", message = "Ürün verileri başarıyla getirildi." });
        }
    }
}
