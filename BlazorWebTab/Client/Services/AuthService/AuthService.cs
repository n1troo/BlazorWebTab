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
        var responseMessage = await _httpClient.PostAsJsonAsync($"api/register", Register(request));
        return await responseMessage.Content.ReadFromJsonAsync<ServiceResponse<int>>();;
    }
}