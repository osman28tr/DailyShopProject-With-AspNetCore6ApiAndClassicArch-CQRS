using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DailyShop.Business.Features.WebSiteSettings.Dtos
{
    public class UpdatedWebSiteSettingDto
    {
        [JsonPropertyName("about")]
        public string? About { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }
        [JsonPropertyName("address")]
        public string? Address { get; set; }
        [JsonPropertyName("siteIcon")]
        public IFormFile? SiteIcon { get; set; }
    }
}
