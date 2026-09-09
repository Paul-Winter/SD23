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
            builder.Configuration.AddJsonFile("config.json");
            builder.Configuration.AddXmlFile("config.xml");
            //builder.Configuration.AddIniFile("config.ini");
            var app = builder.Build();

            app.Map("/", (IConfiguration appConfig)
                => $"User {appConfig["user"]}; Id {appConfig["id"]}\n" +
                $"Login {appConfig["login"]}; Password {appConfig["password"]}");
            app.Run();
        }
    }
}
