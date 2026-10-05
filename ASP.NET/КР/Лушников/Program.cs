using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StavZoo.Data;
using StavZoo.Models;
using StavZoo.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddDbContext<ZooDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ZooDb")));

var jwtKey = builder.Configuration["Jwt:Key"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!))
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = ctx =>
            {
                ctx.Token = ctx.Request.Cookies["access_token"];
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddScoped<JwtService>();

var app = builder.Build();

// Создание БД и первичные данные
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ZooDbContext>();
    db.Database.EnsureCreated();

    // Создаём льва Киару, если его нет
    if (!db.Lions.Any())
    {
        db.Lions.Add(new Lion
        {
            Name = "Киара",
            Slug = "kiara",
            Description = "Львица Киара — обитательница Ставропольского зоопарка в парке Победы. В мае 2025 года впервые стала матерью, родив троих львят. Львята содержатся вместе с матерью, отец — в соседнем вольере.",
            PhotoUrl = "/images/lion/kiara.jpg",
            VideoUrl = "",
            WebcamUrl = "",
            BirthDate = new DateTime(2018, 3, 15),
            MotherName = "Неизвестна"
        });
        db.SaveChanges();
    }

    // Создаём сотрудника по умолчанию, если пользователей нет
    if (!db.Users.Any())
    {
        db.Users.Add(new User
        {
            Username = "keeper",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("123"),
            Role = "Zookeeper"
        });
        db.SaveChanges();
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();

app.Run();