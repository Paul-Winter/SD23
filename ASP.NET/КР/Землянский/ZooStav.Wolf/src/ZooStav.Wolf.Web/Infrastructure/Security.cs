using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using ZooStav.Wolf.Web.Models;

namespace ZooStav.Wolf.Web.Infrastructure;

public class PasswordService
{
    private readonly PasswordHasher<AppUser> _hasher = new();

    public string Hash(AppUser user, string password) => _hasher.HashPassword(user, password);

    public bool Verify(AppUser user, string password)
        => _hasher.VerifyHashedPassword(user, user.PasswordHash, password) != PasswordVerificationResult.Failed;
}

public class CookieAuthService
{
    public const string Scheme = CookieAuthenticationDefaults.AuthenticationScheme;
    public const string CookieName = "ZooStav.Wolf.Auth";

    public async Task SignInAsync(HttpContext ctx, AppUser user)
    {
        var principal = TokenPrincipalFactory.Create(user);
        await ctx.SignInAsync(Scheme, principal, new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
        });
    }

    public Task SignOutAsync(HttpContext ctx) => ctx.SignOutAsync(Scheme);
}

public static class TokenPrincipalFactory
{
    public static System.Security.Claims.ClaimsPrincipal Create(AppUser user)
        => new(new System.Security.Claims.ClaimsIdentity(Build(user), CookieAuthService.Scheme));

    public static IEnumerable<System.Security.Claims.Claim> Build(AppUser user)
    {
        yield return new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id);
        yield return new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.UserName);
        yield return new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, user.Role);
        yield return new System.Security.Claims.Claim("display_name", user.DisplayName);
        yield return new System.Security.Claims.Claim("position", user.Position);
    }
}

public static class Permissions
{
    public static bool CanEditDiary(string? role) => role != null && Roles.DiaryEditors.Contains(role);
    public static bool CanDeleteAnyEntry(string? role) => role == Roles.Admin;
    public static bool CanManageUsers(string? role) => role == Roles.Admin;
    public static bool CanViewAudit(string? role) => role is Roles.Admin or Roles.Veterinarian;
    public static bool CanViewNonPublic(string? role) => role != null && Roles.Employees.Contains(role);
    public static bool CanModerateComments(string? role) => role == Roles.Admin;
}
