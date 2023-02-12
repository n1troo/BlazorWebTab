using BlazorWebTab.Shared;
using BlazorWebTab.Shared.Users;

namespace BlazorWebTab.Server.Services.AuthService;

public interface IAuthService
{
    Task<ServiceResponse<int>> Register(User user, string password);
    Task<bool> UserExists(string email);
    Task<ServiceResponse<string>> LoginUser(string email, string password);
}

