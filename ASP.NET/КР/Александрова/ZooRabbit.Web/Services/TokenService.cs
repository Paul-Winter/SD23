using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ZooRabbit.Web.Models;

namespace ZooRabbit.Web.Services;

public static class TokenService
{
    public static string Create(User u, IConfiguration cfg)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cfg["Jwt:Key"]!));
        var token = new JwtSecurityToken(
            issuer: cfg["Jwt:Issuer"], audience: cfg["Jwt:Issuer"],
            claims: new[] { new Claim(ClaimTypes.Name, u.Username), new Claim(ClaimTypes.Role, u.Role) },
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public static class Passwords
{
    private static readonly PasswordHasher<User> H = new();
    public static string Hash(string p) => H.HashPassword(new User(), p);
    public static bool Verify(User u, string p) =>
        H.VerifyHashedPassword(u, u.PasswordHash, p) != PasswordVerificationResult.Failed;
}
