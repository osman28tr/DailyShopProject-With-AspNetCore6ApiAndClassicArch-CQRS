using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.WebSiteSettings.Dtos
{
	public class WebSiteSettingDto
	{
        public int Id { get; set; }
		public string? About { get; set; }
		public string? Email { get; set; }
		[JsonPropertyName("phone")]
		public string? Phone { get; set; }
		public string? Address { get; set; }
		[JsonPropertyName("siteIcon")]
		public string? SiteIcon { get; set;}
	}
}
