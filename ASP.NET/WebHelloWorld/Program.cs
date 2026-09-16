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

            app.Run(async (context) =>
            {
                if (context.Request.Cookies.ContainsKey("name"))
                {
                    string? name = context.Request.Cookies["name"];
                    await context.Response.WriteAsync($"Hello {name}");
                }
                else
                {
                    context.Response.Cookies.Append("name", "John Doe");
                    await context.Response.WriteAsync($"Hello, World!");
                }
            });

            app.Run();
        }
    }
}
