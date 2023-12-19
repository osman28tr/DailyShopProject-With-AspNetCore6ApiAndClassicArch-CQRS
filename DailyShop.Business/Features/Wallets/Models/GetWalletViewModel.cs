using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Wallets.Models
{
    public class GetWalletViewModel
    {
        public int? Id { get; set; }
        public int? Balance { get; set; }
        [JsonPropertyName("date")]
        public DateTime? CreatedAt { get; set; }
    }
}
