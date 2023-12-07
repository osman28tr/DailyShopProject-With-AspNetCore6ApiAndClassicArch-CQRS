using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DailyShop.Business.Features.Products.Models;
using DailyShop.Entities.Concrete;

namespace DailyShop.Business.Features.Carts.Models
{
    public class GetListCartByProduct
    {
        public GetListCartByProduct()
        {
            ProductImages = new List<string>();
            Reviews = new List<GetListReviewByProductViewModel>();
        }
        [JsonPropertyName("image")]
        public string? BodyImage { get; set; }
        public ICollection<string>? ProductImages { get; set; }
        public ICollection<GetListReviewByProductViewModel>? Reviews { get; set; }
    }
}
