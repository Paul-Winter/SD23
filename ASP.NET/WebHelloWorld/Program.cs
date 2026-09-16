using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.Design;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace WebHelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<User> users = new List<User>
            {
                new User("John_Doe", "qwer1234", "john_doe@mail.ru"),
                new User("Student1", "11111", "stud_ent@mail.edu"),
                new User("Unnamed", "1234567", "un-name@mail.com")
            };

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => options.LoginPath = "/auth");
            builder.Services.AddAuthorization();

            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGet("/auth", async (HttpContext context) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                string authForm = @"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8' />
                    <title>Authentication</title>
                </head>
                <body>
                    <h2>Authentication</h2>
                    <form method='post'>
                        <p>
                            <label>Login:</label>
                            <input name='login'/>
                        </p>
                        <p>
                            <label>Password:</label>
                            <input name='password'/>
                        </p>
                        <p>
                            <label>Email:</label>
                            <input name='email'/>
                        </p>
                        <input type='submit' value='Sign in'/>
                    </form>
                </body>
                </html>";
                await context.Response.WriteAsync(authForm);
            });

            app.MapPost("/auth", async (string url, HttpContext context) =>
            {
                var form = context.Request.Form;
                if (!form.ContainsKey("login") || !form.ContainsKey("password"))
                {
                    return Results.BadRequest("Логин или пароль не указаны!");
                }
                string login = form["login"];
                string pass = form["password"];

                User? user = users.FirstOrDefault(u => u.Login == login && u.Password == pass);
                if (user is null)
                {
                    return Results.Unauthorized();
                }
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Login)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
                return Results.Redirect(url??"/");
            });

            app.Map("/", [Authorize] () => "Hello, World!");

            app.Run();
        }
    }
}
