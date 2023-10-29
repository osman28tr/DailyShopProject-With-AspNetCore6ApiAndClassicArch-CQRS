using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Categories.Dtos
{
    public class DeletedCategoryDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
