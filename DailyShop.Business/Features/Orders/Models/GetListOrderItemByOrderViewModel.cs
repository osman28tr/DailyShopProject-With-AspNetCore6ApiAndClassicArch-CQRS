using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Orders.Models
{
    public class GetListOrderItemByOrderViewModel
    {
        public int? Id { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        [JsonPropertyName("product")]
        public GetListProductByOrderViewModel? GetListProductByOrderViewModel { get; set; }
    }
}
