using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Reviews.Dtos
{
    public class InsertedReviewDto
    {
        [JsonPropertyName("rating")] public byte Rating { get; set; }
        [JsonPropertyName("comment")] public string Description { get; set; }
    }
}
