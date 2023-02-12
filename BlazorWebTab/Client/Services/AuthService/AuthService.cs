using System.Net.Http.Json;
using BlazorWebTab.Shared;
using BlazorWebTab.Shared.Users;

namespace BlazorWebTab.Client.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<ServiceResponse<int>> Register(UserRegister request)
    {
        var result = await _httpClient.PostAsJsonAsync("api/auth/register", request);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
    }

    public async Task<ServiceResponse<string>> UserLogin(UserLogin userLogin)
    {
        var result = await _httpClient.PostAsJsonAsync("api/auth/login", userLogin);
        var data = await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        return data;
    }
}