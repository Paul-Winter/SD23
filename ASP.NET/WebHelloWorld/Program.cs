namespace WebHelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            //app.MapGet("/", () => "Привет от студентов четвёртого курса!");

            app.UseWelcomePage();
            app.Run();
        }
    }
}
