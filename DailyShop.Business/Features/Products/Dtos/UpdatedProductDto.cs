using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Products.Dtos
{
    public class UpdatedProductDto
    {
        public int? CategoryId { get; set; }
        public IFormFile? BodyImage { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Status { get; set; }
        public int? Stock { get; set; }
        public string? Description { get; set; }
        public ICollection<string>? ProductImages { get; set; }
        public ICollection<IFormFile>? ProductImagesFile { get; set; }
        public ICollection<string>? Colors { get; set; }
        public ICollection<string>? Sizes { get; set; }
    }   
}
