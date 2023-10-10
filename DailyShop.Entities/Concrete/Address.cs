using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Entities.Concrete
{
	public class Address:Entity
	{
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Adres { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public AppUser? AppUser { get; set; }
        public Address()
        {
            
        }

		public Address(int id, int appUserId, string title, string description, string adres, string city, string country, string zipCode)
		{
			Id = id;
			AppUserId = appUserId;
			Title = title;
			Description = description;
			Adres = adres;
			City = city;
			Country = country;
			ZipCode = zipCode;
		}
	}
}
