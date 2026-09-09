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

            builder.Services.AddScoped<ICounter, UserCounter>();
            builder.Services.AddScoped<CounterService>();
            
            var app = builder.Build();

            app.UseMiddleware<CounterMiddleware>();

            app.Run();
        }
    }
}
