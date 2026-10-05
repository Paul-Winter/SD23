using Microsoft.EntityFrameworkCore;
using Свинья_Визенау.Data;
using Свинья_Визенау.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ZooContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ZooContext>();

    db.Database.EnsureCreated();

    if (!db.Animals.Any())
    {
        db.Animals.Add(new Animal
        {
            Slug = "wizenau",
            Name = "Поросёнок Визенау",
            Description = "Свинья породы Визенау, выведенной в Вьетнаме " +
                          "Вес взрослой особи 20–25 кг. Окрас чёрно-серый, " +
                          "морда со складками вокруг пятачка. " +
                          "Отличается высоким интеллектом и дружелюбным характером.",
            PhotoUrl = "https://placehold.co/600x400?text=Wizenau",
            ArrivedAt = new DateTime(2024, 6, 1)
        });

        db.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
