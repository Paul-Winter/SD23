using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace StavZoo;

public class AuthService(ZooDb db, IPasswordHasher<User> hasher, IConfiguration cfg)
{
    public async Task<string?> LoginAsync(string userName, string password)
    {
        var u = await db.Users.SingleOrDefaultAsync(x => x.UserName == userName);
        if (u is null) return null;
        if (hasher.VerifyHashedPassword(u, u.PasswordHash, password) == PasswordVerificationResult.Failed)
            return null;

        var claims = new[]
        {
            new Claim("sub", u.Id.ToString()),
            new Claim("name", u.UserName),
            new Claim("role", u.Role)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cfg["Jwt:Key"]!));
        var token = new JwtSecurityToken(
            cfg["Jwt:Issuer"], cfg["Jwt:Audience"], claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(cfg["Jwt:Minutes"]!)),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
