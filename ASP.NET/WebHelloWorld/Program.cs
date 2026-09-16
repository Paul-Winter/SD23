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

            app.UseStatusCodePages("text/plain", "Error! Page not found!");

            app.Map("/hello", () => "Hello, World!");

            app.Run();
        }
    }
}
