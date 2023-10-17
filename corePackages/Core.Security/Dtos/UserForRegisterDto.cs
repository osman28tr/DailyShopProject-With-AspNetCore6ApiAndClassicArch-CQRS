namespace Core.Security.Dtos;

public class UserForRegisterDto
{
    public string email { get; set; }
    public string password { get; set; }
    public string confirmpassword { get; set; }
    public string name { get; set; }
    public string surname { get; set; }
	public string phonenumber { get; set; }
}