using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Categories.Dtos
{
    public class GetListCategoryDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        //public ICollection<Product>? Products { get; set; }
        //public ICollection<ProductColor>? ProductColors { get; set; }
        //public ICollection<ProductSize>? ProductSizes { get; set; }
        //public ICollection<ProductImage>? ProductImages { get; set; }
    }
}
