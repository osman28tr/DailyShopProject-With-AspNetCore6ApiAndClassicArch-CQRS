using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.AppUsers.Dtos
{
    public class BlockedUserDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
