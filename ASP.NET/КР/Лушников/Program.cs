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

// === SQLite вместо SQL Server ===
builder.Services.AddDbContext<ZooDbContext>(options =>
    options.UseSqlite("Data Source=StavZoo.db"));

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

// === Инициализация БД и начальные данные ===
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ZooDbContext>();
    db.Database.EnsureCreated();

    // Создаём папку для фото
    var imageFolder = Path.Combine("wwwroot", "images", "lion");
    if (!Directory.Exists(imageFolder))
        Directory.CreateDirectory(imageFolder);

    // Начальные данные: Киара
    if (!db.Lions.Any())
    {
        db.Lions.Add(new Lion
        {
            Name = "Киара",
            Slug = "kiara",
            Description = "Киара родилась в 2023 году и стала мамой впервые в мае 2025 года. " +
                          "Она произвела на свет трёх львят — двух мальчиков и одну девочку. " +
                          "Это знаменательное событие стало частью настоящего «беби-бума» в Ставропольском зоопарке, " +
                          "где за три месяца родилось 13 детёнышей.\n\n" +
                          "Первые роды Киары прошли успешно, и молодая львица сразу зарекомендовала себя " +
                          "как заботливая и ответственная мать. Сейчас она неотлучно находится рядом с малышами, " +
                          "которые ещё совсем крошечные — их вес при рождении составлял около 1,5 килограмма. " +
                          "Когда львятам исполнилось два месяца, сотрудники зоопарка начали вводить в их рацион " +
                          "первый прикорм — небольшие кусочки мяса на кости.\n\n" +
                          "Отец семейства, самец Симба, с интересом наблюдает за своим потомством из соседнего вольера, " +
                          "оберегая свою семью. Гости зоопарка могут наблюдать за играми подросших львят, " +
                          "которые уже демонстрируют свой характер и хищный оскал, резвясь на солнышке рядом с матерью.",
            PhotoUrl = "/images/lion/kiara.jpg",
            VideoUrl = "https://rutube.ru/play/embed/3ed11db18df99ffeac65768194a5fb0f/",
            WebcamUrl = "",
            BirthDate = new DateTime(2023, 3, 15),
            MotherName = "Неизвестна"
        });
        db.SaveChanges();
    }

    // Начальные данные: сотрудник по умолчанию
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

    // Начальные заметки зоологов в дневнике
    if (!db.LionDiaryEntries.Any())
    {
        var kiara = db.Lions.FirstOrDefault(l => l.Slug == "kiara");
        if (kiara != null)
        {
            db.LionDiaryEntries.AddRange(
                new LionDiaryEntry
                {
                    LionId = kiara.Id,
                    EntryType = "Потомство",
                    Notes = "Киара родила трёх львят: двух мальчиков и одну девочку. Вес каждого около 1,5 кг.",
                    RecordedAt = new DateTime(2025, 5, 17, 10, 30, 0, DateTimeKind.Utc),
                    RecordedBy = "Д-р Соколова, ветеринар"
                },
                new LionDiaryEntry
                {
                    LionId = kiara.Id,
                    EntryType = "Здоровье",
                    Notes = "Все львята здоровы. Вес достиг 3,5 кг. Мать заботлива, кормит молоком.",
                    RecordedAt = new DateTime(2025, 5, 24, 14, 0, 0, DateTimeKind.Utc),
                    RecordedBy = "Д-р Соколова, ветеринар"
                },
                new LionDiaryEntry
                {
                    LionId = kiara.Id,
                    EntryType = "Наблюдение",
                    Notes = "Киара вывела львят на первую прогулку. Малыши резвятся, кусают мать за лапы.",
                    RecordedAt = new DateTime(2025, 7, 3, 11, 15, 0, DateTimeKind.Utc),
                    RecordedBy = "Кипер Алексеев, старший смотритель"
                },
                new LionDiaryEntry
                {
                    LionId = kiara.Id,
                    EntryType = "Кормление",
                    Notes = "Львятам начали вводить первый прикорм — небольшие кусочки мяса на кости.",
                    RecordedAt = new DateTime(2025, 7, 17, 9, 0, 0, DateTimeKind.Utc),
                    RecordedBy = "Кипер Алексеев, старший смотритель"
                }
            );
            db.SaveChanges();
        }
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