using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Reviews.Models
{
    public class GetListReviewByProduct
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? CategoryId { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        [JsonPropertyName("image")]
        public string? BodyImage { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public int? Stock { get; set; }
        public byte? Rating { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsApproved { get; set; }
        public Category? Category { get; set; }
        public AppUser? User { get; set; }
        public ICollection<ProductImage>? ProductImages { get; set; }
        public ICollection<ProductColor>? Colors { get; set; }
        public ICollection<ProductSize>? Sizes { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
        public GetListReviewByProduct()
        {
            Colors = new HashSet<ProductColor>();
            Sizes = new HashSet<ProductSize>();
            ProductImages = new HashSet<ProductImage>();
        }
    }
}
