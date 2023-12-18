using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Orders.Models
{
    public class GetListProductByOrderViewModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public byte? Rating { get; set; }
        public decimal? Price { get; set; }
        [JsonPropertyName("image")]
        public string? BodyImage { get; set; }
    }
}
