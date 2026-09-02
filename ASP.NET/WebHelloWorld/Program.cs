namespace WebHelloWorld
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => "Привет от студентов разработчиков четвёртого курса!");

            await app.StartAsync();
            await Task.Delay(6000);
            await app.StopAsync();
        }
    }
}
