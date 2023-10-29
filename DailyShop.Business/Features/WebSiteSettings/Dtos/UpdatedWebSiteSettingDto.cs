using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.WebSiteSettings.Dtos
{
    public class UpdatedWebSiteSettingDto
    {
        public int? Id { get; set; } = 1;
        public string? HtmlContent { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Adres { get; set; }
        public string? Icon { get; set; }
    }
}
