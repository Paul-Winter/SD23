using Microsoft.EntityFrameworkCore;

namespace Coati_Space_Project.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Coati> Coatis { get; set; } = null!;
        public DbSet<DiaryEntry> DiaryEntries { get; set; } = null!;
        public DbSet<Donation> Donations { get; set; } = null!;
        public DbSet<StaffUser> StaffUsers { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Начальные данные для особи носухи
            modelBuilder.Entity<Coati>().HasData(
                new Coati
                {
                    Id = 1,
                    Name = "Хахатуха",
                    Species = "Южноамериканская носуха (Nasua nasua)",
                    Gender = "Самка",
                    BirthDate = new DateOnly(2021, 5, 14),
                    Biography = "Хахатуха родилась в питомнике и поселилась в Ставропольском зоопарке весной 2022 года. Она невероятно любознательна, обожает исследовать верхние ярусы вольера своим гибким носом и первой встречает сотрудников зоопарка во время утреннего обхода.",
                    Diet = "Сезонные фрукты (виноград, бананы, груши), перепелиные яйца, зофобас, отварная индейка и цветочный мёд по праздникам.",
                    Habitat = "Просторный вольер сектора млекопитающих «Южная Америка», оборудованный ветками, подвесными гамаками и полосой препятствий для обогащения среды.",
                    HealthStatus = "Клинически здоров",
                    PhotoUrl = "/images/coati-main.jpg",
                    VideoUrl = "/videos/coati-stream.mp4"
                }
            );

            // Начальные записи дневника наблюдений
            modelBuilder.Entity<DiaryEntry>().HasData(
                new DiaryEntry
                {
                    Id = 1,
                    CoatiId = 1,
                    EventDate = new DateTime(2026, 9, 20, 10, 30, 0),
                    EventType = "Кормление",
                    Title = "Обогащенное утреннее кормление",
                    Description = "Хахатухе предложены интерактивные деревянные кормушки с кусочками банана и винограда. Проявила высокую смекалку и интерес.",
                    Author = "Кипер Алексей"
                },
                new DiaryEntry
                {
                    Id = 2,
                    CoatiId = 1,
                    EventDate = new DateTime(2026, 9, 25, 14, 0, 0),
                    EventType = "Медосмотр",
                    Title = "Плановый осмотр ветеринара",
                    Description = "Проверено состояние зубов, подвижного носа и шерсти. Вес: 4.8 кг. Все показатели в норме.",
                    Author = "Ветврач Елена"
                },
                new DiaryEntry
                {
                    Id = 3,
                    CoatiId = 1,
                    EventDate = new DateTime(2026, 10, 1, 16, 15, 0),
                    EventType = "Активность",
                    Title = "Обновление вольера и канатных дорожек",
                    Description = "Установлены новые бамбуковые стволы и подвесные гамаки. Хахатуха активно исследовала верхний ярус вольера.",
                    Author = "Кипер Алексей"
                },
                new DiaryEntry
                {
                    Id = 4,
                    CoatiId = 1,
                    EventDate = new DateTime(2026, 10, 5, 11, 0, 0),
                    EventType = "Спаривание",
                    Title = "Период адаптации к самке Норе",
                    Description = "Проведено знакомство через разделительный вольер с самкой Норой. Проявили дружелюбный интерес без агрессии.",
                    Author = "Зоолог Мария"
                }
            );

            // Начальные данные пожертвований
            modelBuilder.Entity<Donation>().HasData(
                new Donation
                {
                    Id = 1,
                    DonorName = "Семья Кузнецовых",
                    Email = "kuznetsov@mail.ru",
                    Amount = 500,
                    Target = "На лакомства и фрукты",
                    Message = "Привет шустрой Хахатухе от детей Алисы и Марка!",
                    CreatedAt = new DateTime(2026, 10, 2, 12, 10, 0)
                },
                new Donation
                {
                    Id = 2,
                    DonorName = "Студенты СКФУ",
                    Email = "students@edu.ru",
                    Amount = 1000,
                    Target = "На новые канаты и игрушки",
                    Message = "На радость пушистому любопытному носу!",
                    CreatedAt = new DateTime(2026, 10, 4, 15, 45, 0)
                }
            );

            // Единственная учетная запись администратора системы
            modelBuilder.Entity<StaffUser>().HasData(
                new StaffUser
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin2026",
                    FullName = "Иван Романов",
                    Role = "Admin",
                    Position = "Администратор зоопарка"
                }
            );
        }
    }
}
