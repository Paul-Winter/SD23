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

            builder.Services.AddTransient<IUserData, UserTime>();
            builder.Services.AddTransient<UserData>();

            var app = builder.Build();

            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";

                //var userData = app.Services.GetService<UserData>();
                var userData = context.RequestServices.GetService<UserData>();

                await context.Response.WriteAsync($"<h3>UserData from: {userData?.GetUserData()}</h3>");
            });
            app.Run();
        }
    }
}
