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
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("about")]
        public string? HtmlContent { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }
        [JsonPropertyName("address")]
        public string? Adres { get; set; }
        //[JsonPropertyName("siteIcon")]
        //[NotMapped]
        //public IFormFile? Icon { get; set; }
    }
}
