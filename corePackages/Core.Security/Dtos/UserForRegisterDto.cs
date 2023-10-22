using System.Text.Json.Serialization;

namespace Core.Security.Dtos;

public class UserForRegisterDto
{
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("password")]
    public string Password { get; set; }
    //public string ConfirmPassword { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    //public string Role { get; set; }
    [JsonPropertyName("surname")]
    public string Surname { get; set; }
    [JsonPropertyName("phonenumber")]
    public string PhoneNumber { get; set; }
}