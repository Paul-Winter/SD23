using System.ComponentModel.Design;
using System.Text;

namespace WebHelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();
            var app = builder.Build();
            var services = builder.Services;
            //builder.Services.AddRazorPages();

            app.Run(async (context) =>
            {
                var servs = new StringBuilder();
                servs.Append("<table>");
                servs.Append("<tr><th>Тип сервиса</th><th>Lifetime</th><th>Реализация</th>");

                foreach (var service in services)
                {
                    servs.Append($"<tr><td>{service.ServiceType.FullName}</td>");
                    servs.Append($"<td>{service.Lifetime}</td>");
                    servs.Append($"<td>{service.ImplementationType?.FullName}</td></tr>");
                }
                
                servs.Append("</table>");
                context.Response.ContentType = "text/html; charset=utf-8";

                await context.Response.WriteAsync(servs.ToString());
            });
            app.Run();
        }
    }
}
