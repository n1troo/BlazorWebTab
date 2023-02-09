using BlazorWebTab.Server.Data;
using BlazorWebTab.Shared;
using BlazorWebTab.Shared.Users;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebTab.Server.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly DataContext _context;

    public AuthService(DataContext context)
    {
        _context = context;
    }
    public async Task<ServiceResponse<int>> Register(User user, string password)
    {
        
        if (await UserExists(user.Email))
        {
            return new ServiceResponse<int>() { Success = false, Message = "User with this e-mail already exists" };
        }
        else
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            return new ServiceResponse<int>() { Success = true, Data = user.Id};
        }
    }

    public async Task<bool> UserExists(string email)
    {
        var result = _context.Users
            .AnyAsync(s => s.Email.ToLower()
                .Equals(email.ToLower())).Result;
        
        return result;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}