using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Products.Models
{
    public class GetListReviewByProductViewModel
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public byte? ReviewRating { get; set; }
        public string? ReviewDescription { get; set; }
        public string? ReviewAvatar { get; set; }
        public string? ReviewStatus { get; set; }
        public DateTime? ReviewCreatedDate { get; set; }
        public DateTime? ReviewUpdatedDate { get; set; }
    }
}
