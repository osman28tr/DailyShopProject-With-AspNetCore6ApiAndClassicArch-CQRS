using DailyShop.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Products.Dtos
{
    public class GetListProductDto
    {
        public GetListProductDto()
        {
            ProductImages = new List<string>();
            Colors = new List<string>();
            Sizes = new List<string>();
        }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("categoryId")]
        public int? CategoryId { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("sellerName")]
        public string? SellerName { get; set; }
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        [JsonPropertyName("image")]
        public string? BodyImage { get; set; }
        [JsonPropertyName("imageFile")]
        [NotMapped]
        public IFormFile? BodyImageFile { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("stock")]
        public int? Stock { get; set; }
        [JsonPropertyName("rating")]
        public byte? Rating { get; set; }
        [JsonPropertyName("isDeleted")]
        public bool? IsDeleted { get; set; }
        [JsonPropertyName("isApproved")]
        public bool? IsApproved { get; set; }
        [JsonPropertyName("imagesFile")]
        [NotMapped]
        public ICollection<IFormFile>? ProductImagesFile { get; set; }
        [JsonPropertyName("images")]
        public ICollection<string>? ProductImages { get; set; }
        [JsonPropertyName("colors")]
        public ICollection<string>? Colors { get; set; }
        [JsonPropertyName("sizes")]
        public ICollection<string>? Sizes { get; set; }
    }
}
