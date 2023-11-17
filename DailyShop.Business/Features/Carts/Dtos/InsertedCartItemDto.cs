using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Carts.Dtos
{
    public class InsertedCartItemDto
    {
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
    }
}
