using ZooRabbit.Web.Models;

namespace ZooRabbit.Web.Data;

public static class SeedData
{
    public static void Init(ZooDbContext db)
    {
        if (db.Animals.Any()) return;

        var rabbit = new Animal
        {
            Slug = "rabbit",
            Name = "Буся",
            Species = "Карликовый кролик",
            Description = "(история)",
            PhotoUrl = "/images/busya.jpg",
        };
        rabbit.Diary.Add(new DiaryEntry
        {
            Type = DiaryEntryType.Feeding,
            Note = "Качественное сено, свежая трава, травяные гранулы.",
            Author = "system"
        });

        db.Animals.Add(rabbit);
        db.SaveChanges();
    }
}
