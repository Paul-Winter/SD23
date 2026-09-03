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
                context.Response.ContentType = "text/html; charset=utf-8";

                var headers = new StringBuilder();
                foreach (var header in context.Request.Headers)
                {
                    headers.Append($"<p>{header.Key} - {header.Value}</p>");
                }

                await context.Response.WriteAsync(headers.ToString());
            });
            app.Run();
        }
    }
}
