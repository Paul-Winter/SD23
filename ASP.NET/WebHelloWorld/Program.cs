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

                response.Headers.ContentLanguage = "ru";
                response.Headers.ContentType = "text/html";
                response.Headers.Accept = "4 course";
                response.Headers.Append("Student", "John Doe");
                
                await context.Response.WriteAsync($"HELLO, WORLD!");                
            });
            app.Run();
        }
    }
}
