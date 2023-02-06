using System.ComponentModel.DataAnnotations;

namespace BlazorWebTab.Shared.Users;

public class UserRegister
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required, StringLength(10, MinimumLength = 2)]
    public string Password { get; set; } = string.Empty;
    
    [Compare("Password", ErrorMessage = "Password do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
    
}