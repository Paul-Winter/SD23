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

            app.Use(async (context, data) =>
            {
                context.Items["text"] = "Hello, World!";
                await data.Invoke();
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Text: {context.Items["text"]}");
            });

            app.Run();
        }
    }
}
