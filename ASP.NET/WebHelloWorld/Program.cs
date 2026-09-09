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
            var app = builder.Build();

            app.Configuration["user"] = "John Doe";
            app.Configuration["id"] = "13748";
            
            app.Run(async context =>
            {
                string? user = app.Configuration["user"];
                string? userId = app.Configuration["id"];

                await context.Response.WriteAsync($"<h2>пользователь {user}, id {userId}</h2>");
            });
        }
    }
}
