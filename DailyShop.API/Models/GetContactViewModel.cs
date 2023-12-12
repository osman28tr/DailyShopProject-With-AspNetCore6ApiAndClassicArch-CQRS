using System.Text.Json.Serialization;

namespace DailyShop.API.Models
{
    public class GetContactViewModel
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
        [JsonPropertyName("address")]
        public string? Address { get; set; }
    }
}
