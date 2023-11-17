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
        public string? Status { get; set; }
        public string? ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
