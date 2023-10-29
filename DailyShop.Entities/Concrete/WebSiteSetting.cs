using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Entities.Concrete
{
    public class WebSiteSetting:Entity
    {
        public WebSiteSetting()
        {
            
        }
        public WebSiteSetting(int id)
        {
            Id = id;
        }
        public string? HtmlContent { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Adres { get; set; }
        public string? Icon { get; set; }
    }
}
