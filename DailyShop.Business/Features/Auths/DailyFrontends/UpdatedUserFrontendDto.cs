using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyShop.Business.Features.Auths.Dtos;

namespace DailyShop.Business.Features.Auths.DailyFrontends
{
    public class UpdatedUserFrontendDto:BaseUserFrontendDto
	{
        public int id { get; set; }
        public AddressDto addresses { get; set; }
    }
}
