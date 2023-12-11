using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.WebSiteSettings.Dtos
{
	public class WebSiteSettingDto
	{
		public string? About { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
		public string? Address { get; set; }
		public string? SiteIcon { get; set;}
	}
}
