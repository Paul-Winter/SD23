using Microsoft.AspNetCore.Authentication.Cookies;
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
            builder.Services.AddAuthentication("Cookies").AddCookie();
            //builder.Services.AddAuthentication("Bearer").AddBearerToken();

            var app = builder.Build();

            app.UseAuthentication();

            app.Run(async (context) =>
            {

            });

            app.Run();
        }
    }
}
