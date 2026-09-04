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
                var response = context.Response;

                response.ContentType = "text/html; charset=utf-8";

                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Страница не найдена!");                
            });
            app.Run();
        }
    }
}
