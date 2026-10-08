using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Coati_Space_Project.Models;

namespace Coati_Space_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connection));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/account/login";
                    options.LogoutPath = "/account/logout";
                    options.AccessDeniedPath = "/account/denied";
                    options.Cookie.SameSite = SameSiteMode.Lax;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                    options.ExpireTimeSpan = TimeSpan.FromHours(8);
                });
            builder.Services.AddAuthorization();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "zoo_coati",
                pattern: "stav-zoo/coati",
                defaults: new { controller = "Zoo", action = "Coati" })
                .WithStaticAssets();

            app.MapControllerRoute(
                name: "zoo_main",
                pattern: "stav-zoo",
                defaults: new { controller = "Zoo", action = "Index" })
                .WithStaticAssets();

            app.MapControllerRoute(
                name: "diary",
                pattern: "diary/{action=Index}/{id?}",
                defaults: new { controller = "Diary" })
                .WithStaticAssets();

            app.MapControllerRoute(
                name: "account",
                pattern: "account/{action=Login}/{id?}",
                defaults: new { controller = "Account" })
                .WithStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Zoo}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
