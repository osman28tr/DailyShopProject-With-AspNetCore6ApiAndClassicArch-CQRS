using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Categories.Dtos
{
    public class UpdatedCategoryDto
    {
        [JsonPropertyName("parentCategoryId")]
        public int ParentCategoryId { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }
    }
}
