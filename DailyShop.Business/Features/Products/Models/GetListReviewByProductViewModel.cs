using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Products.Models
{
    public class GetListReviewByProductViewModel
    {
        [JsonPropertyName("comment")]
        public string? Name { get; set; }
        public string? UserName { get; set; }
        [JsonPropertyName("rating")]
        public byte? ReviewRating { get; set; }
        public string? ReviewDescription { get; set; }
        public string? ReviewAvatar { get; set; }
        [JsonPropertyName("status")]
        public string? ReviewStatus { get; set; }
        [JsonPropertyName("date")]
        public DateTime? ReviewCreatedDate { get; set; }
        public DateTime? ReviewUpdatedDate { get; set; }
    }
}
