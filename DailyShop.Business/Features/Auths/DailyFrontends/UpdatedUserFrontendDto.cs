﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Auths.DailyFrontends
{
	public class UpdatedUserFrontendDto
	{
        public ICollection<AddressFrontendDto> addresses { get; set; }
    }
}
