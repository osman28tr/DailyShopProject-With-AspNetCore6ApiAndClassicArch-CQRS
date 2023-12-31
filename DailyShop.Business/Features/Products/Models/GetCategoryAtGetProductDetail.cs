﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Products.Models
{
    public class GetCategoryAtGetProductDetail
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("parentCategoryId")]
        public int? ParentCategoryId { get; set; }
    }
}
