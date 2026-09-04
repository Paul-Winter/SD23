using System.Text;

namespace WebHelloWorld
{
    public class Program
    {
        public record User(string login, string password);
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.Run(async (context) =>
            {
                var response = context.Response;
                //response.ContentType = "text/html; charset=utf-8";
                User userdata = new User("John Doe", "qwerty111");

                await response.WriteAsJsonAsync(userdata);
                //context.Request.ReadFromJsonAsync();
            });
            app.Run();
        }
    }
}
