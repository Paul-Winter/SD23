using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StavZoo.Data;
using StavZoo.Models;

namespace StavZoo.Pages;

public class RegisterModel : PageModel
{
    private readonly ZooDbContext db;

    public RegisterModel(ZooDbContext db)
    {
        this.db = db;
    }

    public string? Error { get; set; }

    public void OnGet() { }

    public IActionResult OnPost(string username, string password, string role)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            Error = "Заполните все поля";
            return Page();
        }

        if (db.Users.Any(u => u.Username == username))
        {
            Error = "Пользователь с таким именем уже существует";
            return Page();
        }

        if (role != "Visitor" && role != "Zookeeper")
        {
            Error = "Неверная роль";
            return Page();
        }

        var user = new User
        {
            Username = username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = role
        };

        db.Users.Add(user);
        db.SaveChanges();

        return RedirectToPage("/Login");
    }
}