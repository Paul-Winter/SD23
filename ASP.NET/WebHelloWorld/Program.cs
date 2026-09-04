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

                if (context.Request.Path == "/postdata")
                {
                    var form = context.Request.Form;
                    string login = form["login"];
                    string password = form["pass"];
                    await context.Response.WriteAsync($"<h3>Login: {login}</h3>" +
                        $"<h3>Password: {password}</h3>");                
                }
                else
                {
                    await context.Response.SendFileAsync("html/index.html");
                }
            });
            app.Run();
        }
    }
}
