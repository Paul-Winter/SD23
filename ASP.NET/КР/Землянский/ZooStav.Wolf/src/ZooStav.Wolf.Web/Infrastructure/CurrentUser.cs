using System.Security.Claims;
using ZooStav.Wolf.Web.Models;

namespace ZooStav.Wolf.Web.Infrastructure;

public class CurrentUser
{
    private readonly IHttpContextAccessor _accessor;

    public CurrentUser(IHttpContextAccessor accessor) => _accessor = accessor;

    private ClaimsPrincipal? Principal => _accessor.HttpContext?.User;

    public bool IsAuthenticated => Principal?.Identity?.IsAuthenticated == true;

    public string? Id => Principal?.FindFirstValue(ClaimTypes.NameIdentifier);

    public string? UserName => Principal?.FindFirstValue(ClaimTypes.Name);

    public string? DisplayName => Principal?.FindFirstValue("display_name") ?? UserName;

    public string? Position => Principal?.FindFirstValue("position");

    public string? Role => Principal?.FindFirstValue(ClaimTypes.Role);

    public bool IsEmployee => Permissions.CanViewNonPublic(Role);

    public string RoleRussian => Role == null ? "Гость" : Roles.ToRussian(Role);

    public string? IpAddress => _accessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
}
