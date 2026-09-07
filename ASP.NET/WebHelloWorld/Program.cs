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
            builder.Services.AddTransient<IUserData, UserDate>();

            var app = builder.Build();

            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";

                var userData = app.Services.GetService<IUserData>();

                await context.Response.WriteAsync($"UserData from UserDate: {userData?.GetUserData()}");
            });
            app.Run();
        }
    }
}
