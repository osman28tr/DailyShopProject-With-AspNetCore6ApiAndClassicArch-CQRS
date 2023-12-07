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
        public GetListCartByUserViewModel()
        {
            Product = new GetListCartByProduct();
        }
        public int? Id { get; set; }
        public int? Quantity { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public GetListCartByProduct Product { get; set; }
    }
}
