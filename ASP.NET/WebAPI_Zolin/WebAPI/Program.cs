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

            // GET all
            app.MapGet("/api/users", async (AppContext db) => await db.Users.ToListAsync());

            // GET by id
            app.MapGet("/api/users/{id:guid}", async (string id, AppContext db) =>
            {
                User? user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (user == null)
                    return Results.NotFound(new { message = "User not found!" });
                return Results.Json(user);
            });

            // POST create
            app.MapPost("/api/users", async (User user, AppContext db) =>
            {
                user.Id = Guid.NewGuid().ToString();
                await db.Users.AddAsync(user);
                await db.SaveChangesAsync();
                return user;
            });

            // PUT update
            app.MapPut("/api/users", async (User userData, AppContext db) =>
            {
                User? user = await db.Users.FirstOrDefaultAsync(u => u.Id == userData.Id);
                if (user == null)
                    return Results.NotFound(new { message = "User not found!" });
                user.Login = userData.Login;
                user.Password = userData.Password;
                await db.SaveChangesAsync();
                return Results.Json(user);
            });

            // DELETE
            app.MapDelete("/api/users/{id}", async (string id, AppContext db) =>
            {
                User? user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (user == null)
                    return Results.NotFound(new { message = "User not found!" });
                db.Users.Remove(user);
                await db.SaveChangesAsync();
                return Results.Json(user);
            });

            app.Run();
        }
    }
}
