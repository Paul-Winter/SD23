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
                await context.Response.SendFileAsync("C:\\Users\\Student\\Desktop\\TOP.jpg");
            });
            app.Run();
        }
    }
}
