using Microsoft.AspNetCore.Mvc;
using StavZoo.Data;
using StavZoo.Services;

namespace StavZoo.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ZooDbContext db;
    private readonly JwtService jwt;

    public AuthController(ZooDbContext db, JwtService jwt)
    {
        this.db = db;
        this.jwt = jwt;
    }

    [HttpPost("login")]
    public IActionResult Login(string username, string password)
    {
        var user = db.Users.FirstOrDefault(u => u.Username == username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return Unauthorized();
        }

        var token = jwt.GenerateToken(user.Username, user.Role);
        return Ok(new { token });
    }
}