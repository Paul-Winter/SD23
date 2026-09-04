using System.ComponentModel.Design;
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
                if (context.Request.Path == "/pagefrom")
                {
                    await context.Response.WriteAsync("Page From");
                }
                else if (context.Request.Path == "/pageto")
                {
                    context.Response.Redirect("https://www.yandex.ru");
                }
                else
                {
                    await context.Response.WriteAsync("MAIN PAGE");
                }
            });
            app.Run();
        }
    }
}
