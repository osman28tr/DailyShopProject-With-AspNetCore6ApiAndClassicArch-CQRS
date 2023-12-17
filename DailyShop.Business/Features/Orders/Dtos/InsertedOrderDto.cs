using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DailyShop.Business.Features.Payments.Dtos;

namespace DailyShop.Business.Features.Orders.Dtos
{
    public class InsertedOrderDto
    {
        public InsertedOrderDto()
        {
            InsertedOrderItemDtos = new List<InsertedOrderItemDto>();
        }
        [JsonPropertyName("addressId")]
        public int? AdressId { get; set; }
        [JsonPropertyName("creditCard")]
        public InsertedPaymentDto? InsertedCreditCardDto { get; set; }
        [JsonPropertyName("orderItems")]
        public ICollection<InsertedOrderItemDto>? InsertedOrderItemDtos { get; set; }
    }
}
