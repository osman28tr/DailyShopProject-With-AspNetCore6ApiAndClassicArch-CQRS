using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Auths.DailyFrontends
{
	public class UpdatedUserFrontendDto:BaseUserFrontendDto
	{
        public int id { get; set; }
        public AddressFrontendDto addresses { get; set; }
    }
}
