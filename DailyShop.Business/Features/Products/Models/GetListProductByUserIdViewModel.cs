using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Products.Models
{
    public class GetListProductByUserIdViewModel
    {
        public GetListProductByUserIdViewModel()
        {
            ReviewsModel = new List<GetListReviewByProductViewModel>();
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        [JsonPropertyName("image")]
        public string? BodyImage { get; set; }
        [JsonIgnore]
        public string? Description { get; set; }
        public string? Status { get; set; }
        public int? Stock { get; set; }
        public byte? Rating { get; set; }
        [JsonPropertyName("isApproved")]
        public bool? IsApproved { get; set; }
        [JsonPropertyName("images")]
        public ICollection<string>? ProductImages { get; set; }
        public ICollection<string>? Colors { get; set; }
        public ICollection<string>? Sizes { get; set; }
        public ICollection<GetListReviewByProductViewModel>? ReviewsModel { get; set; }
    }
}
