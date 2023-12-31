using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.AppUsers.Dtos
{
    public class ReportUserDto
    {
        [JsonPropertyName("message")]
        public string? ReportedMessage { get; set; }
    }
}
