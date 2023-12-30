using DailyShop.Business.Features.Categories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Reviews.Models
{
    public class GetListReviewToReviewViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("comment")]
        public string Description { get; set; }
        [JsonPropertyName("parentReviewId")]
        public int? ParentReviewId { get; set; }
        [JsonPropertyName("answers")]
        public ICollection<GetListReviewToReviewViewModel>? SubReviews { get; set; }
    }
}
