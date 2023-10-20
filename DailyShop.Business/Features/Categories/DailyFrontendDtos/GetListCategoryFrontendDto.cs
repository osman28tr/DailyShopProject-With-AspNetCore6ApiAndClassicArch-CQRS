using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Categories.DailyFrontendDtos
{
    public class GetListCategoryFrontendDto
    {
        public string name { get; set; }
        public ICollection<Product>? Products { get; set; }
        //public ICollection<ProductColor>? ProductColors { get; set; }
        //public ICollection<ProductSize>? ProductSizes { get; set; }
        //public ICollection<ProductImage>? ProductImages { get; set; }
    }
}
