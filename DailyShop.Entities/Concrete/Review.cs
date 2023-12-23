using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Entities.Concrete
{
    public class Review:Entity
    {
        public int? ProductId { get; set; }
        public int? AppUserId { get; set; }
        public int? ParentReviewId { get; set; }
        public string? Name { get; set; }
        public byte? Rating { get; set; }
        public string? Description { get; set; }
        public string? Avatar { get; set; }
        public string? Status { get; set; }
        public Product? Product { get; set; }
        public AppUser? AppUser { get; set; }
        [ForeignKey("ParentReviewId")]
        public ICollection<Review> SubReviews { get; set; }
        public Review()
        {
            
        }
        public Review(int id)
        {
            Id = id;
        }
    }
}
