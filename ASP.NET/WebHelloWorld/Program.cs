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

            //app.UseDeveloperExceptionPage();
            app.Environment.EnvironmentName = "Production";

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler(app => app.Run(async (context) =>
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("Error 500. DivideByZeroException!");
                }));
            }

            app.Run(async (context) =>
            {
                int x = 12;
                int y = 0;
                int z = x / y;
                await context.Response.WriteAsync($"x = {x}; y = {y}; z = {z}");
            });

            app.Run();
        }
    }
}
