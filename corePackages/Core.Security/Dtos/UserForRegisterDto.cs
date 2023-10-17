namespace Core.Security.Dtos;

public class UserForRegisterDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public string Surname { get; set; }
	public string PhoneNumber { get; set; }
}