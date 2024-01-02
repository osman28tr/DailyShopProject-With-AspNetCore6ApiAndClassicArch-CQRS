using DailyShop.Business.Features.AppUsers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Reviews.Models
{
    public class GetReviewByReportReviewViewModel
    {
        public GetReviewByReportReviewViewModel()
        {
            User = new GetListUserDto();
        }
        public GetListUserDto User { get; set; }
        [JsonPropertyName("comment")]
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}
