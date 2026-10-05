using Microsoft.EntityFrameworkCore;
using ZooRabbit.Web.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<ZooDbContext>(o =>
    o.UseSqlite(builder.Configuration.GetConnectionString("Default") ?? "Data Source=zoo.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ZooDbContext>();
    db.Database.EnsureCreated();
    SeedData.Init(db);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();
var api = app.MapGroup("/api/animals");

api.MapGet("/{slug}", async (string slug, ZooDbContext db) =>
    await db.Animals.AsNoTracking().FirstOrDefaultAsync(a => a.Slug == slug) is { } a
        ? Results.Ok(a) : Results.NotFound());

api.MapGet("/{slug}/diary", async (string slug, ZooDbContext db) =>
{
    var entries = await db.DiaryEntries.AsNoTracking()
        .Where(e => e.Animal.Slug == slug)
        .OrderByDescending(e => e.Date)
        .ToListAsync();
    return Results.Ok(entries);
});

app.Run();
