using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Orders.Dtos
{
    public class InsertedPaymentDto
    {
        [JsonPropertyName("cardNumber")]
        public string? CardNumber { get; set; }
        [JsonPropertyName("cardOwner")]
        public string? CardOwner { get; set; }
        [JsonPropertyName("LastDate")]
        public string? LastDate { get; set; }
        [JsonPropertyName("cvv")]
        public string? Cvv { get; set; }
    }
}
