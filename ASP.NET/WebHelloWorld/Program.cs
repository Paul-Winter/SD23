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
                var path = context.Request.Path;
                string user = "/user";
                string pass = "/secure";
                var now = DateTime.Now.ToString();

                context.Response.ContentType = "text/html; charset=utf-8";

                if (path == "/user")
                {
                    await context.Response.WriteAsync($"Welcome to USER page! {now}");
                }
                else if (path == "/secure")
                {
                    await context.Response.WriteAsync($"ВВЕДИТЕ ПАРОЛЬ: _");
                }
                else
                {
                    await context.Response.WriteAsync($"Path: {path} {now}");
                }
            });
            app.Run();
        }
    }
}
