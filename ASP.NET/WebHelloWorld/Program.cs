using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.Design;
using System.Security.Cryptography;
using System.Text;

namespace WebHelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
            //builder.Services.AddAuthentication("Bearer").AddBearerToken();
            builder.Services.AddAuthentication("Cookies").AddCookie();
            builder.Services.AddAuthorization();

            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Map("/", () => "Hello, World!");
            app.Map("/secret", [Authorize] () => "Hello, Admin!");

            app.Run();
        }
    }
}
