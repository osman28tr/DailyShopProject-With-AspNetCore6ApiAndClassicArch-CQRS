using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Orders.Dtos
{
    public class InsertedOrderItemDto
    {
        [JsonPropertyName("productId")]
        public int? ProductId { get; set; }
        [JsonPropertyName("quantity")]
        public int? Quantity { get; set; }
        [JsonPropertyName("size")]
        public string? Size { get; set; }
        [JsonPropertyName("color")]
        public string? Color { get; set; }
    }
}
