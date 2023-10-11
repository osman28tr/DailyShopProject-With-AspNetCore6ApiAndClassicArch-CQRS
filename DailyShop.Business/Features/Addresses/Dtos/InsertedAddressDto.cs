using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Addresses.Dtos
{
	public class InsertedAddressDto
	{
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public string Title { get; set; }
    }
}
