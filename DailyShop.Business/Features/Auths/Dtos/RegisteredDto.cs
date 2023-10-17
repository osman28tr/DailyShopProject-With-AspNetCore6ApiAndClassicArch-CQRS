using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Auths.Dtos
{
	public class RegisteredDto:RefreshedTokenDto
	{
        public string Message { get; set; }
    }
}
