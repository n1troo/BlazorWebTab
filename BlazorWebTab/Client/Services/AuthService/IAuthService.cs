using BlazorWebTab.Shared;
using BlazorWebTab.Shared.Users;

namespace BlazorWebTab.Client.Services.AuthService;

public interface IAuthService
{
    public Task<ServiceResponse<int>> Register(UserRegister request);
}