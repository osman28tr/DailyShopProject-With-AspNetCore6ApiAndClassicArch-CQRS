using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Products.Models
{
	public class GetListReviewByProductViewModel
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }
		[JsonPropertyName("rating")]
		public byte? ReviewRating { get; set; }
		[JsonPropertyName("comment")]
		public string? ReviewDescription { get; set; }
		[JsonPropertyName("status")]
		public string? ReviewStatus { get; set; }
		[JsonPropertyName("date")]
		public DateTime? ReviewCreatedDate { get; set; }
		public DateTime? ReviewUpdatedDate { get; set; }
		[JsonPropertyName("user")]
		public ReviewUser? User { get; set; }
		[JsonPropertyName("userPurchasedThisProduct")]
		public bool? UserPurchasedThisProduct { get; set; }
	}
}

public class ReviewUser
{
	[JsonPropertyName("id")]
	public int Id { get; set; }
	[JsonPropertyName("name")]
	public string? Name { get; set; }
	[JsonPropertyName("profileImage")]
	public string? Image { get; set; }
	[JsonPropertyName("email")]
	public string? Email { get; set; }
}