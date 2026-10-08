using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Свинья_Визенау.Data;
using Свинья_Визенау.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ZooContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ZooContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Diary", "ZooWorker");
    options.Conventions.AuthorizePage("/Donate", "Authenticated");
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ZooWorker", policy =>
        policy.RequireRole("Worker", "Admin"));
    options.AddPolicy("Authenticated", policy =>
        policy.RequireAuthenticatedUser());
});

builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ZooContext>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    db.Database.EnsureCreated();

    string[] roles = { "Admin", "Worker", "Visitor" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }

    if (!db.Animals.Any())
    {
        db.Animals.Add(new Animal
        {
            Slug = "wizenau",
            Name = "Поросёнок Визенау",
            Description = "Свинья породы Визенау вьетнамская. Вес взрослой особи 120–140 кг. " +
                          "Внешний вид: Черный или черно-пестрый окрас, короткие ноги и большой висячий живот.",
            PhotoUrl = "/images/wizenau.jpg",
            VideoUrl = null,
            WebcamUrl = null,
            ArrivedAt = new DateTime(2024, 6, 1)
        });
        await db.SaveChangesAsync();
    }

    var workerEmail = "worker@zoo.local";
    if (await userManager.FindByEmailAsync(workerEmail) == null)
    {
        var worker = new ApplicationUser
        {
            UserName = workerEmail,
            Email = workerEmail,
            EmailConfirmed = true,
            DisplayName = "Смотритель Иван"
        };
        var result = await userManager.CreateAsync(worker, "Worker123");
        if (result.Succeeded)
            await userManager.AddToRoleAsync(worker, "Worker");
    }

    var adminEmail = "admin@zoo.local";
    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var admin = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true,
            DisplayName = "Администратор"
        };
        var result = await userManager.CreateAsync(admin, "Admin123");
        if (result.Succeeded)
            await userManager.AddToRoleAsync(admin, "Admin");
    }

    var wizenau = db.Animals.FirstOrDefault(a => a.Slug == "wizenau");
    if (wizenau != null)
    {
        if (wizenau.VideoUrl != "/Videos/Wizenau.mp4")
            wizenau.VideoUrl = "/Videos/Wizenau.mp4";

        if (wizenau.WebcamUrl != "/Videos/WebCamWizenau.mp4")
            wizenau.WebcamUrl = "/Videos/WebCamWizenau.mp4";


        db.SaveChanges();
    }

    if (wizenau != null && !db.AnimalPhotos.Any(p => p.AnimalId == wizenau.Id))
    {
        db.AnimalPhotos.AddRange(
            new AnimalPhoto { AnimalId = wizenau.Id, Url = "/Images/Wizenau.jpg", Caption = "Визенау в вольере", SortOrder = 1 },
            new AnimalPhoto { AnimalId = wizenau.Id, Url = "/Images/svinia2.jpg", Caption = "Старый поросенок", SortOrder = 2 },
            new AnimalPhoto { AnimalId = wizenau.Id, Url = "/Images/svinia3.jpg", Caption = "Поросенок со своей семьей", SortOrder = 3 },
            new AnimalPhoto { AnimalId = wizenau.Id, Url = "/Images/svinia4.jpg", Caption = "кормление молоком маленького поросенка", SortOrder = 4 },
            new AnimalPhoto { AnimalId = wizenau.Id, Url = "/Images/svinia5.jpg", Caption = "Семейное фото", SortOrder = 5 }
        );
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



app.Use(async (context, next) =>
{
    var host = context.Request.Host.Host;
    var parts = host.Split('.');

    if (parts.Length > 1 && parts[0] != "www" && parts[0] != "localhost")
    {
        var subdomain = parts[0];
        var currentPath = context.Request.Path.Value ?? "/";

        if (!currentPath.StartsWith("/api") &&
            !currentPath.StartsWith("/Account") &&
            !currentPath.StartsWith("/Donate") &&
            !currentPath.StartsWith("/Diary") &&
            !currentPath.StartsWith("/images") &&
            !currentPath.StartsWith("/css") &&
            !currentPath.StartsWith("/js") &&
            !currentPath.StartsWith("/lib") &&
            !currentPath.StartsWith("/favicon"))
        {
            context.Request.Path = "/" + subdomain + currentPath;
        }
    }

    await next();
});

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();

app.Run();