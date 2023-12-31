using DailyShop.Business.Features.Addresses.Dtos;
using DailyShop.Business.Features.Reviews.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.AppUsers.Dtos
{
    public class GetListUserDto
    {
        public GetListUserDto()
        {
            Addresses = new List<AddressListByUserIdDto>();
            Reviews = new List<GetListReviewByUserDto>();
        }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string FirstName { get; set; }
        [JsonPropertyName("surname")]
        public string LastName { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("profileImage")]
        public string ProfileImage { get; set; }
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("status")]
        public bool Status { get; set; }
        [JsonPropertyName("phone")] 
        public string PhoneNumber { get; set; }
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set;}
        [JsonPropertyName("addresses")]
        public ICollection<AddressListByUserIdDto> Addresses { get; set; }
        [JsonPropertyName("reviews")]
        public ICollection<GetListReviewByUserDto> Reviews { get; set; }
    }
}
