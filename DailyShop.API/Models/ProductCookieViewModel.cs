using System.Text.Json.Serialization;

namespace DailyShop.API.Models
{
    public class ProductCookieViewModel
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }
        [JsonPropertyName("durationInSeconds")]
        public int DurationInSeconds { get; set; }
    }
}
