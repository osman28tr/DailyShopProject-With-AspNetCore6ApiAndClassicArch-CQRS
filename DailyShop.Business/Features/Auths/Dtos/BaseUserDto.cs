using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Auths.Dtos
{
    public class BaseUserDto
    {
        public IFormFile ProfileImageFile { get; set; }
        [JsonPropertyName("profileImage")]
        public string? ProfileImage { get; set; }
        [JsonPropertyName("name")]
        public string? FirstName { get; set; }
        [JsonPropertyName("surname")]
        public string? LastName { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("phone")]
        public string? PhoneNumber { get; set; }
    }
}
