using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace StavZoo;

public class ZooDb(DbContextOptions<ZooDb> options) : DbContext(options)
{
    public DbSet<Animal> Animals => Set<Animal>();
    public DbSet<DiaryEntry> Diary => Set<DiaryEntry>();
    public DbSet<Donation> Donations => Set<Donation>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Animal>().HasIndex(a => a.Slug).IsUnique();
        b.Entity<User>().HasIndex(u => u.UserName).IsUnique();
        b.Entity<DiaryEntry>().HasOne<Animal>().WithMany().HasForeignKey(d => d.AnimalId);
        b.Entity<Donation>().HasOne<Animal>().WithMany().HasForeignKey(d => d.AnimalId);
    }

    public static void Seed(ZooDb db, IPasswordHasher<User> hasher)
    {
        db.Database.EnsureCreated();
        if (db.Animals.Any()) return;

        var llama = new Animal
        {
            Slug = "llama",
            Name = "Лама",
            Species = "Lama glama",
            Biography = "Наше стадо лам живёт в зоопарке с 2019 года. Самец Дали приехал из питомника " +
                        "в Подмосковье, самки Луна и Пепита — из Чехии. Ламы любят сено, овёс и яблоки, " +
                        "могут плеваться, если их разозлить, и очень дружелюбны к детям. " +
                        "В 2024 году у Луны родился первый детёныш — Кузя.",
            PhotoUrl = "/img/llama.svg",
            VideoUrl = "https://rutube.ru/play/embed/16e2b5c725bf6c589f22f6c25dda380f", 
            WebcamUrl = "https://www.youtube.com/embed/a_WjWhIsVVU?si=acRbF4Qyuinr2d5M"   
        };
        db.Animals.Add(llama);

        var admin = new User { UserName = "admin", Role = "Admin" };
        admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");
        var keeper = new User { UserName = "keeper", Role = "Keeper" };
        keeper.PasswordHash = hasher.HashPassword(keeper, "Keeper123!");
        db.Users.AddRange(admin, keeper);
        db.SaveChanges();

        db.Diary.AddRange(
            new DiaryEntry
            {
                AnimalId = llama.Id, Type = DiaryType.Offspring, SubjectName = "Кузя",
                Text = "Родился детёныш у Луны, вес 11 кг, оба в порядке.",
                OccurredAt = new DateTime(2024, 5, 14, 6, 30, 0, DateTimeKind.Utc), Author = "keeper"
            },
            new DiaryEntry
            {
                AnimalId = llama.Id, Type = DiaryType.Feeding, SubjectName = null,
                Text = "Утренняя кормёжка стада: сено 12 кг, овёс 3 кг, яблоки.",
                OccurredAt = DateTime.UtcNow.AddHours(-3), Author = "keeper"
            });
        db.SaveChanges();
    }
}
