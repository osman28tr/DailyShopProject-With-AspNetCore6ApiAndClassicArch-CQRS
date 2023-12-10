using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Products.Models
{
    public class DeleteProductViewModel
    {
        public string Name { get; set; }
        public string BodyImage { get; set; }
        public List<string> ProductImages { get; set; }
    }
}
