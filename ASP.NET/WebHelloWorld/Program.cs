using System.ComponentModel.Design;
using System.Text;

namespace WebHelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddTransient<IUserData, UserTime>();
            builder.Services.AddTransient<UserData>();

            var app = builder.Build();

            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";

                var userData = app.Services.GetService<UserData>();

                await context.Response.WriteAsync($"UserData from UserData: {userData?.GetUserData()}");
            });
            app.Run();
        }
    }
}
