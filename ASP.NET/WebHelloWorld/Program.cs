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
                var paramString = new StringBuilder();

                foreach (var param in context.Request.Query)
                {
                    paramString.Append($"<h2>{param.Key} - {param.Value}</h2>");
                }
                
                await context.Response.WriteAsync($"{paramString.ToString()}");                
            });
            app.Run();
        }
    }
}
