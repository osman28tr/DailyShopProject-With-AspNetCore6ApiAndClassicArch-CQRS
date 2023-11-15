using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Products.Models
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            ReviewsModel = new List<GetListReviewByProductViewModel>();
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? BodyImage { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public int? Stock { get; set; }
        public string? UserName { get; set; }
        public byte? Rating { get; set; }
        public ICollection<string>? ProductImages { get; set; }
        public ICollection<string>? Colors { get; set; }
        public ICollection<string>? Sizes { get; set; }
        public ICollection<GetListReviewByProductViewModel>? ReviewsModel { get; set; }
    }
}
