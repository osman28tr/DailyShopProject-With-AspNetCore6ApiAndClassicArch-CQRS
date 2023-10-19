using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Auths.DailyFrontends
{
    public class AddressFrontendDto
    {
        public string title { get; set; }
        public string description { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string zipcode { get; set; }
    }
}
