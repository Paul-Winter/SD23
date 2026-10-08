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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ZooDbContext>();
    db.Database.EnsureCreated();

    var imageFolder = Path.Combine("wwwroot", "images", "lion");
    if (!Directory.Exists(imageFolder))
        Directory.CreateDirectory(imageFolder);

    if (!db.Lions.Any())
    {
        var kiara = new Lion
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
            PhotoUrl = "/images/lion/049e90d1-8e6c-4b24-9114-14b5d9426fa8.jpg",
            PhotoUrl2 = "/images/lion/kiara-photo2.jpg",
            PhotoUrl3 = "/images/lion/kiara-photo3.jpg",
            VideoUrl = "https://rutube.ru/play/embed/3ed11db18df99ffeac65768194a5fb0f/",
            WebcamUrl = "",
            BirthDate = new DateTime(2023, 3, 15),
            MotherName = "Неизвестна"
        };

        db.Lions.Add(kiara);
        db.SaveChanges();

        db.LionPhotos.AddRange(
            new LionPhoto
            {
                LionId = kiara.Id,
                PhotoUrl = "/images/lion/7e3ca5bd-81f9-4d5c-b1d0-ecf89a657e38.jpg",
                UploadedAt = DateTime.UtcNow.AddMinutes(-50)
            },
            new LionPhoto
            {
                LionId = kiara.Id,
                PhotoUrl = "/images/lion/ad513d4c-79d2-4215-9eea-b140f4b8a6ed.jpg",
                UploadedAt = DateTime.UtcNow.AddMinutes(-40)
            },
            new LionPhoto
            {
                LionId = kiara.Id,
                PhotoUrl = "/images/lion/c12b5d49-00f2-4401-b75c-095ab62d12fb.jpg",
                UploadedAt = DateTime.UtcNow.AddMinutes(-30)
            },
            new LionPhoto
            {
                LionId = kiara.Id,
                PhotoUrl = "/images/lion/c54c957a-90b9-4d48-be5f-84dcb873186b.jpg",
                UploadedAt = DateTime.UtcNow.AddMinutes(-20)
            },
            new LionPhoto
            {
                LionId = kiara.Id,
                PhotoUrl = "/images/lion/daa4f10f-b578-4dbc-8503-474c1324aae5.jpg",
                UploadedAt = DateTime.UtcNow.AddMinutes(-10)
            }
        );
        db.SaveChanges();
    }

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