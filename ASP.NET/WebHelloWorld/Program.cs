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

            builder.Services.AddTransient<IUserData, UserHello>();
            builder.Services.AddTransient<IUserData, UserDate>();
            builder.Services.AddTransient<IUserData, UserTime>();
            
            var app = builder.Build();

            app.UseMiddleware<UserDataMiddleware>();

            app.Run();
        }
    }
}
