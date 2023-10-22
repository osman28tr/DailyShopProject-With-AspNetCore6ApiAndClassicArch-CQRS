using Core.Security.Entities;
using Core.Security.JWT;
using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Auths.Dtos
{
    public class LoggedInDto
	{
		public AccessToken AccessToken { get; set; }
		public RefreshToken RefreshToken { get; set; }
        public LoggedUserDto LoggedUserDto { get; set; }
    }
}
