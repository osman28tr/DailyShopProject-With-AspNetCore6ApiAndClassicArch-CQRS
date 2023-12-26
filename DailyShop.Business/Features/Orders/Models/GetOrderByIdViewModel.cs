using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Orders.Models
{
    public class GetOrderByIdViewModel
    {
        public int? Id { get; set; }
        public int? TotalPrice { get; set; }
        public string? Status { get; set; }
        [JsonPropertyName("date")]
        public DateTime? CreatedAt { get; set; }
        public string? OrderNumber { get; set; }
        [JsonPropertyName("orderItems")]
        public ICollection<GetListOrderItemByOrderViewModel> GetListOrderItemByOrderViewModels { get; set; }
    }
}
