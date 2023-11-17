using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Carts.Models
{
    public class GetListCartByUserViewModel
    {
        public int CartItemId { get; set; }
        public string? Status { get; set; }
        public string? ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string? BodyImage { get; set; }
        public string? Description { get; set; }
        public string? ProductStatus { get; set; }
        public int? Stock { get; set; }
        public byte? Rating { get; set; }
        public ICollection<string>? ProductImages { get; set; }
        public ICollection<string>? Colors { get; set; }
        public ICollection<string>? Sizes { get; set; }
    }
}
