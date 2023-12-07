using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Addresses.Dtos
{
	public class AddressListByUserIdDto
	{
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Email { get; set; }
        //public string PhoneNumber { get; set; }
        //public string ProfileImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [JsonPropertyName("address")]
        public string Adres { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
