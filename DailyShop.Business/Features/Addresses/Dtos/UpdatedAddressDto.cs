using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Addresses.Dtos
{
	public class UpdatedAddressDto
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string Adres { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public string ZipCode { get; set; }
	}
}
