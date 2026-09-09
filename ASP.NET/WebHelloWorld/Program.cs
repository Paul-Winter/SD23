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

            builder.Services.AddSingleton<ICounter, UserCounter>();
            builder.Services.AddSingleton<CounterService>();
            
            var app = builder.Build();

            app.UseMiddleware<CounterMiddleware>();

            app.Run();
        }
    }
}
