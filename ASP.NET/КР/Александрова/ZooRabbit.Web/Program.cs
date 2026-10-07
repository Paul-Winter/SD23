using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ZooRabbit.Web.Data;
using ZooRabbit.Web.Models;
using ZooRabbit.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<ZooDbContext>(o =>
    o.UseSqlite(builder.Configuration.GetConnectionString("Default") ?? "Data Source=zoo.db"));
builder.Services.Configure<JsonOptions>(o => o.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true, ValidAudience = builder.Configuration["Jwt:Issuer"],
            ValidateLifetime = true, ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            NameClaimType = ClaimTypes.Name, RoleClaimType = ClaimTypes.Role
        };
        o.Events = new JwtBearerEvents
        {
            OnMessageReceived = ctx =>
            {
                if (string.IsNullOrEmpty(ctx.Token)) ctx.Token = ctx.Request.Cookies["jwt"];
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ZooDbContext>();
    db.Database.EnsureCreated();
    SeedData.Init(db);
}

if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Error");

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapPost("/api/auth/login", async (LoginDto dto, ZooDbContext db, IConfiguration cfg) =>
{
    var u = await db.Users.FirstOrDefaultAsync(x => x.Username == dto.Username);
    if (u is null || !Passwords.Verify(u, dto.Password)) return Results.Unauthorized();
    return Results.Ok(new { token = TokenService.Create(u, cfg) });
});

var api = app.MapGroup("/api/animals");

api.MapGet("/{slug}", async (string slug, ZooDbContext db) =>
    await db.Animals.AsNoTracking().Where(a => a.Slug == slug)
        .Select(a => new { a.Slug, a.Name, a.Species, a.Description, a.PhotoUrl, a.VideoUrl, a.WebcamUrl })
        .FirstOrDefaultAsync() is { } a ? Results.Ok(a) : Results.NotFound());

api.MapPost("/{slug}/donate", async (string slug, DonateDto dto, ZooDbContext db) =>
{
    var animal = await db.Animals.FirstOrDefaultAsync(a => a.Slug == slug);
    if (animal is null) return Results.NotFound();
    if (dto.Amount <= 0) return Results.BadRequest("Сумма должна быть больше 0");
    db.Donations.Add(new Donation { AnimalId = animal.Id, Amount = dto.Amount, Purpose = dto.Purpose ?? "Корм",
        DonorName = string.IsNullOrWhiteSpace(dto.DonorName) ? "Аноним" : dto.DonorName });
    await db.SaveChangesAsync();
    return Results.Ok();
});

var diary = api.MapGroup("/{slug}/diary").RequireAuthorization(p => p.RequireRole("Employee"));

diary.MapGet("", async (string slug, ZooDbContext db) =>
    Results.Ok(await db.DiaryEntries.AsNoTracking().Where(e => e.Animal.Slug == slug)
        .OrderByDescending(e => e.Date)
        .Select(e => new { e.Id, e.Date, e.Type, e.Note, e.Author }).ToListAsync()));

diary.MapPost("", async (string slug, DiaryDto dto, ZooDbContext db, ClaimsPrincipal user) =>
{
    var animal = await db.Animals.FirstOrDefaultAsync(a => a.Slug == slug);
    if (animal is null) return Results.NotFound();
    if (string.IsNullOrWhiteSpace(dto.Note)) return Results.BadRequest("Пустая запись");
    var e = new DiaryEntry { AnimalId = animal.Id, Type = dto.Type, Note = dto.Note.Trim(), Author = user.Identity!.Name! };
    db.DiaryEntries.Add(e);
    await db.SaveChangesAsync();
    return Results.Created($"/api/animals/{slug}/diary/{e.Id}", new { e.Id });
});

diary.MapDelete("/{id:int}", async (string slug, int id, ZooDbContext db) =>
{
    var e = await db.DiaryEntries.FirstOrDefaultAsync(x => x.Id == id && x.Animal.Slug == slug);
    if (e is null) return Results.NotFound();
    db.DiaryEntries.Remove(e);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();

record LoginDto(string Username, string Password);
record DonateDto(int Amount, string? Purpose, string? DonorName);
record DiaryDto(DiaryEntryType Type, string Note);
