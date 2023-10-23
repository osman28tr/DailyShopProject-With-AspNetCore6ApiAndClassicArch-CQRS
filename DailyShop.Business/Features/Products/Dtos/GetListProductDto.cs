using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Products.Dtos
{
    public class GetListProductDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("categoryId")]
        public int? CategoryId { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        [JsonPropertyName("image")]
        public string? BodyImage { get; set; }
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
        [JsonPropertyName("images")]
        public ICollection<ProductImage>? ProductImages { get; set; }
        [JsonPropertyName("colors")]
        public ICollection<Color>? Colors { get; set; }
        [JsonPropertyName("sizes")]
        public ICollection<Size>? Sizes { get; set; }
    }
}
