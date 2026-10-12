using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ZooStav.Wolf.Web.Models;

namespace ZooStav.Wolf.Web.Infrastructure;

public class JwtOptions
{
    public string Issuer { get; set; } = "zoostav.ru";
    public string Audience { get; set; } = "zoostav.ru-wolf-api";
    public string Key { get; set; } = string.Empty;
    public int LifetimeMinutes { get; set; } = 480;
}

public record TokenResult(string Token, DateTime ExpiresAtUtc, string Role, string UserId, string DisplayName);


public class JwtTokenService
{
    private readonly JwtOptions _options;

    public JwtTokenService(IConfiguration config)
    {
        _options = new JwtOptions();
        config.GetSection("Jwt").Bind(_options);
    }

    public JwtOptions Options => _options;

    public TokenResult CreateToken(AppUser user)
    {
        var expires = DateTime.UtcNow.AddMinutes(_options.LifetimeMinutes);

        var claims = TokenPrincipalFactory.Build(user).ToList();
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat,
            DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expires,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        return new TokenResult(new JwtSecurityTokenHandler().WriteToken(token), expires, user.Role, user.Id, user.DisplayName);
    }
}
