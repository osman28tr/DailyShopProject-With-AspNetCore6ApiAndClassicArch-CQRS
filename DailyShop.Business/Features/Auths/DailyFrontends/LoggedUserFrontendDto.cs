using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Auths.DailyFrontends
{
    public class LoggedUserFrontendDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string profileImage { get; set; }
        public string phone { get; set; }
        public ICollection<AddressFrontendDto> addresses { get; set; }
    }
}
