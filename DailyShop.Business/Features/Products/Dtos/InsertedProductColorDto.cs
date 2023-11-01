using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Products.Dtos
{
    public class InsertedProductColorDto
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}
