using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DailyShop.Business.Features.Products.Models;

namespace DailyShop.Business.Features.Carts.Models
{
    public class GetListCartByProduct
    {
        public GetListCartByProduct()
        {
            ProductImages = new List<string>();
            Reviews = new List<GetListReviewByProductViewModel>();
        }
        public int? Id { get; set; }
        public int? CategoryId { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        [JsonPropertyName("image")]
        public string? BodyImage { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public int? Stock { get; set; }
        public byte? Rating { get; set; }
        public DateTime? Date { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsApproved { get; set; }
        [JsonPropertyName("images")]
        public ICollection<string>? ProductImages { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public ICollection<GetListReviewByProductViewModel>? Reviews { get; set; }
    }
}
