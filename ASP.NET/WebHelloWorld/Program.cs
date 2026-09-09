using System.ComponentModel.Design;
using System.Security.Cryptography;
using System.Text;

namespace WebHelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddTransient<IUserData, UserDate>();
            var app = builder.Build();

            app.Map("/", () => "MAIN PAGE");
            app.Map("/hello", () => Console.WriteLine("Hello, World!"));
            app.Map("/users", () => "USERS");
            app.Map("/users/{userId:int}", Handler);
            app.Map("/date", (IUserData userDate) => $"Сегодня: {userDate.GetUserData()}");

            app.Run();
        }

        static string Handler(int userId)
        {
            return $"User Id: {userId}";
        }
    }
}
