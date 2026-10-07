using ZooRabbit.Web.Models;
using ZooRabbit.Web.Services;

namespace ZooRabbit.Web.Data;

public static class SeedData
{
    public static void Init(ZooDbContext db)
    {
        if (!db.Animals.Any())
        {
            var rabbit = new Animal
            {
                Slug = "rabbit",
                Name = "Буся",
                Species = "Карликовый кролик",
                Description = "Буся появилась в зоопарке два года назад.  Любимые лакомства: морковь и груша.",
                PhotoUrl = "/images/busya.jpg"
            };
            rabbit.Diary.Add(new DiaryEntry { Type = DiaryEntryType.Feeding, Note = "Сено, свежая зелень, гранулы.", Author = "system" });
            db.Animals.Add(rabbit);
        }
        if (!db.Users.Any())
        {
            db.Users.Add(new User { Username = "worker", PasswordHash = Passwords.Hash("Worker123!"), Role = "Employee" });
        }
        db.SaveChanges();
    }
}
