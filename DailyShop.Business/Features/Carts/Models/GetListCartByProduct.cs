using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DailyShop.Business.Features.Products.Models;
using DailyShop.Entities.Concrete;

namespace DailyShop.Business.Features.Carts.Models
{
    public class GetListCartByProduct
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("image")]
        public string? BodyImage { get; set; }
    }
}
