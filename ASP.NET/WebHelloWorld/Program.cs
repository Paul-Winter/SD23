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
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            var app = builder.Build();

            app.Run(async (context) =>
            {
                var path = context.Request.Path;

                app.Logger.LogInformation($"LogInformation: {context.Request.Path}");
                app.Logger.LogWarning($"LogWarning: {context.Request.Path}");
                app.Logger.LogError($"LogError: {context.Request.Path}");
                app.Logger.LogCritical($"LogCritical: {context.Request.Path}");

                await context.Response.WriteAsync("Hello, World!");
            });

            app.Run();
        }
    }
}
