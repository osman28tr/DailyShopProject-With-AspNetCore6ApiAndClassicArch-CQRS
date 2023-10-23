using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Auths.Dtos
{
    public class UpdatedUserDto : BaseUserDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("addresses")]
        public AddressDto Addresses { get; set; }
    }
}
