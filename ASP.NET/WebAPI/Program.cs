using Microsoft.EntityFrameworkCore;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppContext>(options => options.UseSqlite(connection));

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            List<User> users = new List<User>
            {
                new User() { Id = Guid.NewGuid().ToString(), Login = "111FirstUser111", Password = "11111"},
                new User() { Id = Guid.NewGuid().ToString(), Login = "XyJIu*Gun", Password = "123456"},
                new User() { Id = Guid.NewGuid().ToString(), Login = "DaRK-RaiN", Password = "qwerty"},
                new User() { Id = Guid.NewGuid().ToString(), Login = "Black_Dragon", Password = "password"},
                new User() { Id = Guid.NewGuid().ToString(), Login = "___Max_Frei___", Password = "sirmax"}
            };

            // GET
            app.MapGet("/users", (AppContext db) => db.Users.ToList());
            app.MapGet("/api/users", () => users);
            app.MapGet("/api/users/{id}", (string id) =>
            {
                User? user = users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return Results.NotFound(new { message = "User not found!" });
                }
                return Results.Json(user);
            });

            // POST
            app.MapPost("/api/users", (User user) =>
            {
                user.Id = Guid.NewGuid().ToString();
                users.Add(user);
                return user;
            });

            // PUT
            app.MapPut("/api/users", (User userData) =>
            {
                User? user = users.FirstOrDefault(u => u.Id == userData.Id);
                if (user == null)
                {
                    return Results.NotFound(new { message = "User not found!" });
                }
                user.Login = userData.Login;
                user.Password = userData.Password;
                return Results.Json(user);
            });

            // DELETE
            app.MapDelete("/api/users/{id}", (string id) =>
            {
                User? user = users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return Results.NotFound(new { message = "User not found!" });
                }
                users.Remove(user);
                return Results.Json(user);
            });

            app.Run();
        }
    }
}
