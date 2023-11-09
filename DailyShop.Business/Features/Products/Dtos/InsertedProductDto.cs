using DailyShop.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Products.Dtos
{
    public class InsertedProductDto
    {
        [JsonPropertyName("categoryId")]
        public int? CategoryId { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        [JsonPropertyName("coverImage")]
        public IFormFile? BodyImage { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("stock")]
        public int? Stock { get; set; }
        [JsonPropertyName("colors")]
        public ICollection<string>? Colors { get; set; }
        [JsonPropertyName("sizes")]
        public ICollection<string>? Sizes { get; set; }
        [JsonPropertyName("images")]
        public List<IFormFile>? ProductImages { get; set; }
    }
}
