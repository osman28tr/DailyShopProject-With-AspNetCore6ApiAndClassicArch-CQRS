using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Auths.Dtos
{
    public class LoggedUserDto : BaseUserDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("profileImage")]
        public string ProfileImage { get; set; }
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("createdAt")]
        public string CreatedAt { get; set; }
        [JsonPropertyName("updatedAt")]
        public string UpdatedAt { get; set; }
        [JsonPropertyName("addresses")]
        public ICollection<AddressDto> Addresses { get; set; }
    }
}
