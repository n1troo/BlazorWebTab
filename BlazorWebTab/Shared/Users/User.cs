namespace BlazorWebTab.Shared.Users;

public class User : Base
{
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
}