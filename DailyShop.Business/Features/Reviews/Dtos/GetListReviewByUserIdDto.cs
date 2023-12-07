using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DailyShop.Business.Features.Reviews.Models;

namespace DailyShop.Business.Features.Reviews.Dtos
{
    public class GetListReviewByUserIdDto
    {
        [JsonPropertyName("rating")]
        public byte? Rating { get; set; }
        [JsonPropertyName("comment")]
        public string? Description { get; set; }
        [JsonPropertyName("avatar")]
        public string? Status { get; set; }
        [JsonPropertyName("date")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("product")]
        public GetListReviewByProduct? Product { get; set; }
    }
}
