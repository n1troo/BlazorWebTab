using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Text;
using BlazorWebTab.Server.Data;
using BlazorWebTab.Shared;
using BlazorWebTab.Shared.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BlazorWebTab.Server.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly DataContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(DataContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
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
            
            return new ServiceResponse<int>() { Success = true, Data = user.Id, Message = "User created!"};
        }
    }

    public async Task<bool> UserExists(string email)
    {
        var result = _context.Users
            .AnyAsync(s => s.Email.ToLower()
                .Equals(email.ToLower())).Result;
        
        return result;
    }

    public async Task<ServiceResponse<string>> LoginUser(string email, string password)
    {

        var response = new ServiceResponse<string>();

        var user = await _context.Users.Where(s => s.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();

        if (user == null)
        {
            response.Success = false;
            response.Message = "Incorrect username or password";
        }
        else if(!VerificationPassword(password, user.PasswordHash, user.PasswordSalt))
        {
            response.Success = false;
            response.Message = "Incorrect username or password";
        }
        else
        {
            response.Success = true;
            response.Data = CreateToken(user);
        }
        
        return response;
    }

    private string? CreateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Email),
        };

        string secretKey = _configuration.GetSection("AppSettings:Token").Value;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddHours(10);

        var token = new JwtSecurityToken(
            issuer: "your-issuer",
            audience: "your-audience",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials:cred
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    private bool VerificationPassword(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512(userPasswordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(userPasswordHash);
        }
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