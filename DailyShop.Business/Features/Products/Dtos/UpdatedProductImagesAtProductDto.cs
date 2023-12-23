using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Products.Dtos
{
    public class UpdatedProductImagesAtProductDto
    {
        public int Id { get; set; } = 0;
        public IFormFile? Image { get; set; }
    }
}
