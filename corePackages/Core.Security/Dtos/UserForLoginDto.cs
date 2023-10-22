using System.Text.Json.Serialization;

namespace Core.Security.Dtos;

public class UserForLoginDto
{
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName ("password")]
    public string Password { get; set; }
    //public string? AuthenticatorCode { get; set; }
}