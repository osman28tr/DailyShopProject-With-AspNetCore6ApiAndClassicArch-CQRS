using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Orders.Models
{
    public class GetListOrderAddressByOrderViewModel
    {
        public string? Title { get; set; }
        public string? Adres { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
    }
}
