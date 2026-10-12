using Microsoft.EntityFrameworkCore;
using ZooStav.Wolf.Web.Infrastructure;
using ZooStav.Wolf.Web.Models;

namespace ZooStav.Wolf.Web.Data;

/// <summary>
/// Наполнение демонстрационной базы: работники, особи стаи, медиа, дневник,
/// комментарии, донаты и журнал изменений (кто, когда и что менял).
/// </summary>
public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db, PasswordService passwords)
    {
        if (await db.Animals.AnyAsync()) return;   // данные уже есть

        var now = DateTime.Now;

        // ---------------------------------------------------- Работники зоопарка
        AppUser NewUser(string login, string password, string name, string position, string role, string email)
        {
            var u = new AppUser
            {
                UserName = login,
                DisplayName = name,
                Position = position,
                Role = role,
                Email = email,
                IsActive = true,
                CreatedAt = now.AddMonths(-14)
            };
            u.PasswordHash = passwords.Hash(u, password);
            return u;
        }

        var admin = NewUser("admin", "Admin#2026", "Смирнова Ольга Петровна", "Директор зоопарка", Roles.Admin, "smirnova@zoostav.ru");
        var keeper = NewUser("keeper", "Keeper#2026", "Егоров Дмитрий Сергеевич", "Старший кипер вольера «Северная тундра»", Roles.Keeper, "egorov@zoostav.ru");
        var keeper2 = NewUser("keeper2", "Keeper2#2026", "Морозова Анна Викторовна", "Кипер", Roles.Keeper, "morozova@zoostav.ru");
        var vet = NewUser("vet", "Vet#2026", "Лапшин Игорь Николаевич", "Ветеринарный врач", Roles.Veterinarian, "lapshin@zoostav.ru");
        var intern = NewUser("intern", "Intern#2026", "Абрамов Пётр Иванович", "Стажёр-зоолог", Roles.Observer, "abramov@zoostav.ru");

        db.Users.AddRange(admin, keeper, keeper2, vet, intern);
        await db.SaveChangesAsync();

        // ---------------------------------------------------- Животные (стая)
        var umka = new Animal
        {
            Name = "Умка", Species = "Северный (арктический) волк", LatinName = "Canis lupus arctos",
            SubdomainSlug = "wolf", Gender = "Самка", BirthDate = new DateTime(2019, 4, 12),
            Enclosure = "Вольер «Северная тундра» №1", Status = "Живёт в зоопарке",
            ConservationStatus = "Вызывающие наименьшие опасения (LC)",
            WeightKg = 42.5m, HeightCm = 76m, IsMainPage = true,
            ShortDescription = "Умка — альфа-самка стаи северных волков зоопарка «ZooStav»: спокойная, " +
                               "любопытная к посетителям, легко узнаётся по тёмной отметине над правым глазом.",
            History =
                "Умка родилась 12 апреля 2019 года в питомнике северных волков под Архангельском. " +
                "В волчьем выводке она была самой крупной самкой и с первых недель проявляла лидерские качества: " +
                "первой подходила к кормовому столу и первой начинала осваивать новые предметы в вольере.\n\n" +
                "В ставропольский зоопарк ZooStav Умка переехала в августе 2020 года в возрасте 16 месяцев — " +
                "для формирования новой экспозиционной стаи «Северная тундра». Дорогу перенесла спокойно, " +
                "адаптационный период занял всего девять дней: уже на десятый день она принимала корм из рук кипера.\n\n" +
                "В феврале 2022 года Умка стала альфа-самкой стаи, а в марте 2023-го вместе с самцом Таймыром " +
                "принесла первое потомство — трёх волчат, двое из которых впоследствии были переведены в " +
                "зоопарки Казани и Ростова-на-Дону в рамках программы обмена кровью.\n\n" +
                "Весной 2025 года у пары родилось ещё два волчонка. Умка — заботливая мать: она по очереди " +
                "допускает к логову киперов и ветеринаров, что делает её идеальной «нянькой» для " +
                "воспитания молодняка. Любимое лакомство — оленина и замороженная рыба; " +
                "в жару предпочитает лёд с рыбой в качестве обогащения среды.",
            HeroImageUrl = "/images/wolf-hero.jpg"
        };

        var taimyr = new Animal
        {
            Name = "Таймыр", Species = "Северный (арктический) волк", LatinName = "Canis lupus arctos",
            SubdomainSlug = "taimyr", Gender = "Самец", BirthDate = new DateTime(2018, 5, 3),
            Enclosure = "Вольер «Северная тундра» №1", Status = "Живёт в зоопарке",
            ConservationStatus = "Вызывающие наименьшие опасения (LC)",
            WeightKg = 51.2m, HeightCm = 84m,
            ShortDescription = "Таймыр — альфа-самец стаи, самый крупный волк вольера: вес 51 кг, спокойный «серый кардинал» стаи.",
            History = "Таймыр родился 3 мая 2018 года в зоопарке Новосибирска. В 2020 году переехал в ZooStav " +
                      "и возглавил стаю. Отличный «охранник» вольера: первым проверяет новые объекты и подаёт " +
                      "сигнал остальным. Любит замороженный лёд с рыбой и долгие прогулки по снежному насту.",
            HeroImageUrl = "/images/wolf-pack.jpg",
            PartnerAnimalId = null
        };

        var yana = new Animal
        {
            Name = "Яна", Species = "Северный (арктический) волк", LatinName = "Canis lupus arctos",
            SubdomainSlug = "yana", Gender = "Самка", BirthDate = new DateTime(2021, 6, 20),
            Enclosure = "Вольер «Северная тундра» №1", Status = "Живёт в зоопарке",
            ConservationStatus = "Вызывающие наименьшие опасения (LC)",
            WeightKg = 36.8m, HeightCm = 71m,
            ShortDescription = "Яна — младшая самка стаи, дочь Умки и Таймыра. Самая игривая и голосистая: " +
                               "именно её вой чаще всего слышат посетители.",
            History = "Яна родилась 20 июня 2021 года в стае ZooStav и осталась в родном вольере. " +
                      "С детства дружелюбна к людям, но сохраняет дистанцию с незнакомцами. " +
                      "Участвует в показательных занятиях по обогащению среды.",
            HeroImageUrl = "/images/wolf-puppies.jpg"
        };

        var nord = new Animal
        {
            Name = "Норд", Species = "Северный (арктический) волк", LatinName = "Canis lupus arctos",
            SubdomainSlug = "nord", Gender = "Самец", BirthDate = new DateTime(2023, 3, 28),
            Enclosure = "Вольер «Северная тундра» №2 (молодняк)", Status = "Живёт в зоопарке",
            ConservationStatus = "Вызывающие наименьшие опасения (LC)",
            WeightKg = 34.1m, HeightCm = 68m,
            ShortDescription = "Норд — самый молодой самец стаи, будущий вожак молодняка: " +
                               "любопытен, первым осваивает новые игрушки и укрытия.",
            History = "Норд родился 28 марта 2023 года в ZooStav. Прошёл программу социализации " +
                      "и участвует в ветеринарных тренингах (добровольный подход к весам и осмотр лап).",
            HeroImageUrl = "/images/wolf-webcam.jpg"
        };

        db.Animals.AddRange(umka, taimyr, yana, nord);
        await db.SaveChangesAsync();

        umka.PartnerAnimalId = taimyr.Id;
        yana.PartnerAnimalId = nord.Id;
        await db.SaveChangesAsync();

        // ---------------------------------------------------- Медиа
        db.Media.AddRange(
            new MediaItem { AnimalId = umka.Id, Kind = MediaKinds.Photo, Title = "Умка в зимнем вольере", Url = "/images/wolf-hero.jpg", Description = "Портрет альфа-самки, январь", CreatedAt = now.AddDays(-40) },
            new MediaItem { AnimalId = umka.Id, Kind = MediaKinds.Photo, Title = "Стая на снежном холме", Url = "/images/wolf-pack.jpg", Description = "Умка, Таймыр и Яна на вечерней прогулке", CreatedAt = now.AddDays(-25) },
            new MediaItem { AnimalId = umka.Id, Kind = MediaKinds.Photo, Title = "Кормление у платформы", Url = "/images/wolf-feeding.jpg", Description = "Кипер Дмитрий Егоров раскладывает рацион", CreatedAt = now.AddDays(-12) },
            new MediaItem { AnimalId = umka.Id, Kind = MediaKinds.Photo, Title = "Оба волчонка на первой прогулке", Url = "/images/wolf-puppies.jpg", Description = "Потомство 2025 года", CreatedAt = now.AddDays(-5) },
            new MediaItem { AnimalId = umka.Id, Kind = MediaKinds.Video, Title = "Вечерний выход стаи", Url = "/media/wolf-pack.mp4", PosterUrl = "/images/wolf-pack.jpg", Description = "Короткая видеозапись из вольера", CreatedAt = now.AddDays(-20) },
            new MediaItem { AnimalId = umka.Id, Kind = MediaKinds.Video, Title = "Кормление и обогащение среды", Url = "/media/wolf-feeding.mp4", PosterUrl = "/images/wolf-feeding.jpg", Description = "Занятие по обогащению среды с кипером", CreatedAt = now.AddDays(-10) },
            new MediaItem { AnimalId = umka.Id, Kind = MediaKinds.Webcam, Title = "Вебикамера вольера «Северная тундра»", Url = "", PosterUrl = "/images/wolf-webcam.jpg", Description = "Камера №2, обзор кормовой платформы и укрытия", CreatedAt = now.AddDays(-90) },

            new MediaItem { AnimalId = taimyr.Id, Kind = MediaKinds.Photo, Title = "Таймыр", Url = "/images/wolf-pack.jpg", CreatedAt = now.AddDays(-30) },
            new MediaItem { AnimalId = taimyr.Id, Kind = MediaKinds.Webcam, Title = "Вебикамера вольера", Url = "", PosterUrl = "/images/wolf-webcam.jpg", CreatedAt = now.AddDays(-90) },
            new MediaItem { AnimalId = yana.Id, Kind = MediaKinds.Photo, Title = "Яна", Url = "/images/wolf-puppies.jpg", CreatedAt = now.AddDays(-30) },
            new MediaItem { AnimalId = nord.Id, Kind = MediaKinds.Photo, Title = "Норд", Url = "/images/wolf-webcam.jpg", CreatedAt = now.AddDays(-15) }
        );
        await db.SaveChangesAsync();

        // ---------------------------------------------------- Дневник особи
        var entries = new List<DiaryEntry>();

        DiaryEntry Add(Animal animal, DiaryEntryType type, DateTime occurred, string title, string details,
            AppUser author, bool isPublic = true, string? foodType = null, decimal? foodKg = null,
            string? medication = null, string? dosage = null, string? partner = null, int? offspring = null,
            decimal? weight = null, decimal? temp = null, string? diagnosis = null, int createdMinutesBefore = 10)
        {
            var e = new DiaryEntry
            {
                AnimalId = animal.Id,
                Type = type,
                OccurredAt = occurred,
                Title = title,
                Details = details,
                AuthorId = author.Id,
                IsPublic = isPublic,
                FoodType = foodType,
                FoodAmountKg = foodKg,
                Medication = medication,
                Dosage = dosage,
                PartnerName = partner,
                OffspringCount = offspring,
                WeightKg = weight,
                TemperatureC = temp,
                Diagnosis = diagnosis,
                CreatedAt = occurred.AddMinutes(createdMinutesBefore),
                UpdatedAt = occurred.AddMinutes(createdMinutesBefore)
            };
            entries.Add(e);
            return e;
        }

        // — кормёжки (последняя ~6 часов назад)
        Add(umka, DiaryEntryType.Feeding, now.AddHours(-6), "Утренняя кормёжка",
            "Рацион съеден полностью, вода заменена. Умка кормилась первой, конфликтов в стае нет.",
            keeper, true, "Оленина, говяжьи субпродукты, витаминный премикс", 2.4m);
        Add(umka, DiaryEntryType.Feeding, now.AddHours(-22), "Вечерняя кормёжка",
            "Аппетит хороший. Половина рациона спрятана в снегу как обогащение среды — найдено за 12 минут.",
            keeper2, true, "Говядина, морская рыба (минтай), яйцо", 2.2m);
        Add(umka, DiaryEntryType.Feeding, now.AddHours(-30), "Утренняя кормёжка",
            "Съедено полностью. Наблюдался повышенный интерес к кормовой платформе после вакцинации.",
            keeper2, true, "Кролик, субпродукты, морковь", 2.6m);
        Add(umka, DiaryEntryType.Feeding, now.AddHours(-46), "Кормёжка с лакомством",
            "По случаю холодной погоды выдано замороженное угощение — рыба во льду.",
            keeper, true, "Замороженная рыба во льду", 1.8m);
        Add(umka, DiaryEntryType.Feeding, now.AddHours(-54), "Вечерняя кормёжка",
            "Съедено 90% рациона, остаток убран. Причина — жаркая погода, аппетит снижен.",
            keeper, true, "Говяжье сердце, творог, яйцо", 2.0m);
        Add(umka, DiaryEntryType.Feeding, now.AddHours(-70), "Утренняя кормёжка",
            "Аппетит восстановлен, съедено полностью.", keeper2, true, "Оленина, рыба, премикс", 2.5m);

        // — вакцинация
        Add(umka, DiaryEntryType.Vaccination, now.AddDays(-28).AddHours(10), "Ежегодная вакцинация (комплексная)",
            "Вакцинация проведена в помещении карантина. Перенесена без осложнений, наблюдение 48 часов — норма.",
            vet, true, medication: "Нобивак Rabies + Nobivac DHPPi", dosage: "1,0 мл п/к");
        Add(umka, DiaryEntryType.Vaccination, now.AddDays(-28).AddHours(10).AddMinutes(-20), "Осмотр перед вакцинацией",
            "Температура в норме, слизистые чистые, состояние шерсти и подушечек лап — удовлетворительное.",
            vet, false, weight: 42.5m, temp: 38.4m);

        // — взвешивание
        Add(umka, DiaryEntryType.Weighing, now.AddDays(-14).AddHours(9), "Ежемесячное взвешивание",
            "Вес стабилен, соответствует норме для самки 6 лет. Участие добровольное, без успокоительных средств.",
            vet, true, weight: 42.5m);

        // — спаривание и потомство
        Add(umka, DiaryEntryType.Mating, now.AddDays(-64), "Спаривание с самцом Таймыром",
            "Отмечено спаривание в период эструса. Пара уединилась в логове, стая сохраняла нейтралитет.",
            keeper, false, partner: "Таймыр");
        Add(umka, DiaryEntryType.Observation, now.AddDays(-52), "Признаки беременности",
            "Отмечено гнездовое поведение, отказ от активных игр. Взяты анализы — подтверждена беременность.",
            vet, false);

        // — болезнь и лечение
        Add(umka, DiaryEntryType.Illness, now.AddDays(-19).AddHours(15), "Хромота на переднюю правую лапу",
            "При беге отмечена лёгкая хромота. Побег осмотрен: микротрещина подушечки. Назначено щадящее содержание.",
            vet, false, diagnosis: "Поверхностная травма подушечки лапы");
        Add(umka, DiaryEntryType.Treatment, now.AddDays(-19).AddHours(16), "Обработка лапы, наложение повязки",
            "Проведена обработка антисептиком, наложена защитная повязка. Ограничена нагрузка на 5 дней.",
            vet, false, medication: "Хлоргексидин 0,05%, левомеколь", dosage: "по схеме, 2 раза в день, 5 дней");
        Add(umka, DiaryEntryType.Examination, now.AddDays(-14).AddHours(10), "Контрольный осмотр лапы",
            "Повязка снята, заживление полное. Ограничения по нагрузке сняты, волчица вернулась к активности стаи.",
            vet, true);

        // — наблюдения и комментарии
        Add(umka, DiaryEntryType.Observation, now.AddDays(-7).AddHours(11), "Поведение во время дождя",
            "Стая предпочла укрытие, на кормовую платформу выходили по очереди. Умка контролировала очередь.",
            keeper2, true);
        Add(umka, DiaryEntryType.Comment, now.AddDays(-2).AddHours(13), "Замечание по вольеру",
            "Рекомендую увеличить количество укрытий и добавить второй теневой навес — в жаркие дни стая скапливается в одном месте.",
            keeper, true);
        Add(umka, DiaryEntryType.Relocation, now.AddDays(-33), "Перевод молодняка в вольер №2",
            "Двое волчат переведены в вольер для молодняка. Умка перенесла спокойно, вокализации не наблюдалось.",
            admin, true);

        // — несколько записей по другим особям (для общей ленты дневника стаи)
        Add(taimyr, DiaryEntryType.Feeding, now.AddHours(-4), "Утренняя кормёжка Таймыра",
            "Съедено полностью, аппетит отличный.", keeper, true, "Оленина, рыба, субпродукты", 3.1m);
        Add(taimyr, DiaryEntryType.Examination, now.AddDays(-11).AddHours(12), "Осмотр шерстного покрова",
            "Линька проходит физиологично, паразитов не обнаружено.", vet, true);
        Add(taimyr, DiaryEntryType.Illness, now.AddDays(-40), "Снижение аппетита",
            "Отказ от корма в течение суток, вялость. Начато наблюдение, взяты анализы.",
            vet, false, diagnosis: "Подозрение на гастрит (не подтверждено)");
        Add(yana, DiaryEntryType.Feeding, now.AddHours(-7), "Утренняя кормёжка Яны",
            "Аппетит хороший, кормилась после альфа-пары.", keeper2, true, "Говядина, рыба, творог", 1.9m);
        Add(yana, DiaryEntryType.Observation, now.AddDays(-9).AddHours(20), "Вечерний вой",
            "Яна инициировала групповой вой в 20:40, продолжительность около минуты. Ответа соседних стай нет.",
            keeper2, true);
        Add(yana, DiaryEntryType.Weighing, now.AddDays(-6).AddHours(9), "Взвешивание Яны",
            "Вес соответствует норме.", vet, true, weight: 36.8m);
        Add(nord, DiaryEntryType.Feeding, now.AddHours(-5), "Кормёжка молодняка",
            "Аппетит отличный, съедено полностью. Норд осваивает кормовую платформу первым из молодняка.",
            keeper2, true, "Говядина, индейка, премикс", 2.8m);
        Add(nord, DiaryEntryType.Observation, now.AddDays(-3).AddHours(17), "Знакомство с новым укрытием",
            "Первым обследовал новый деревянный домик, проявил игровое поведение.", keeper, true);
        Add(nord, DiaryEntryType.Vaccination, now.AddDays(-20).AddHours(11), "Ревакцинация молодняка",
            "Ревакцинация по календарю, перенесена без реакций.", vet, false,
            medication: "Nobivac DHPPi", dosage: "1,0 мл п/к");

        db.DiaryEntries.AddRange(entries);
        await db.SaveChangesAsync();

        // ---------------------------------------------------- Комментарии работников
        DiaryEntry Entry(string title) => entries.First(x => x.Title == title);

        var feedingToday = Entry("Утренняя кормёжка");
        var illness = Entry("Хромота на переднюю правую лапу");
        var weighing = Entry("Ежемесячное взвешивание");
        var mating = Entry("Спаривание с самцом Таймыром");

        var comments = new List<DiaryComment>
        {
            new() { DiaryEntryId = feedingToday.Id, AuthorId = vet.Id, Text = "Проверил остатки корма — всё в норме, добавок не требуется.", CreatedAt = feedingToday.OccurredAt.AddMinutes(35) },
            new() { DiaryEntryId = feedingToday.Id, AuthorId = admin.Id, Text = "Спасибо. Завтра ожидается похолодание, увеличьте калорийность рациона на 10%.", CreatedAt = feedingToday.OccurredAt.AddMinutes(90) },
            new() { DiaryEntryId = illness.Id, AuthorId = keeper.Id, Text = "Лапа осмотрена повторно, отёка нет. Волчица спокойно даёт обрабатывать.", CreatedAt = illness.OccurredAt.AddHours(3) },
            new() { DiaryEntryId = illness.Id, AuthorId = vet.Id, Text = "Повязку менять утром и вечером, фиксировать состояние в дневнике.", CreatedAt = illness.OccurredAt.AddHours(4) },
            new() { DiaryEntryId = weighing.Id, AuthorId = admin.Id, Text = "Динамика веса положительная относительно осенних показателей.", CreatedAt = weighing.OccurredAt.AddHours(2) },
            new() { DiaryEntryId = mating.Id, AuthorId = vet.Id, Text = "Запись служебная: при подготовке отчёта по размножению учитывать.", CreatedAt = mating.OccurredAt.AddHours(5) }
        };
        db.DiaryComments.AddRange(comments);
        await db.SaveChangesAsync();

        // ---------------------------------------------------- Журнал изменений (аудит)
        var audit = new List<AuditLog>();
        string Name(AppUser u) => u.DisplayName;
        void AddAudit(string entity, string id, string action, string summary, AppUser user, DateTime when)
            => audit.Add(new AuditLog
            {
                EntityName = entity, EntityId = id, Action = action, Summary = summary,
                UserId = user.Id, UserName = user.DisplayName, UserRole = user.Role,
                IpAddress = "10.20.0." + (Math.Abs(user.UserName.GetHashCode()) % 200 + 10),
                TimestampUtc = when.ToUniversalTime()
            });

        foreach (var e in entries)
        {
            var author = db.Users.Local.First(u => u.Id == e.AuthorId);
            AddAudit("DiaryEntry", e.Id.ToString(), "Created",
                $"Создана запись «{e.Title}» ({e.Type.ToRussian()}) на {e.OccurredAt:dd.MM.yyyy HH:mm}", author, e.CreatedAt);
        }

        // пара правок «кто и когда менял»
        AddAudit("DiaryEntry", illness.Id.ToString(), "Updated",
            "Изменена запись «Хромота на переднюю правую лапу» (№" + illness.Id + "). диагноз: «Ушиб» → «Поверхностная травма подушечки лапы»; публичность: «публичная» → «служебная»",
            vet, illness.CreatedAt.AddHours(6));
        AddAudit("DiaryEntry", feedingToday.Id.ToString(), "Updated",
            "Изменена запись «Утренняя кормёжка» (№" + feedingToday.Id + "). масса корма: «2,20» → «2,40»",
            keeper, feedingToday.CreatedAt.AddMinutes(20));

        foreach (var c in comments)
            AddAudit("DiaryComment", c.Id.ToString(), "CommentAdded",
                $"Добавлен комментарий к записи №{c.DiaryEntryId}", db.Users.Local.First(u => u.Id == c.AuthorId), c.CreatedAt);

        // ---------------------------------------------------- Донаты
        var donations = new List<Donation>
        {
            new() { DonorName = "Ирина К.", Amount = 1500m, Purpose = DonationPurposes.Food, Comment = "Умке на оленину!", CreatedAt = now.AddDays(-42), AnimalId = umka.Id, IsProcessed = true },
            new() { DonorName = "Школьный класс 7«Б», г. Будённовск", Amount = 7500m, Purpose = DonationPurposes.Food, Comment = "Собран всем классом на корм стае", CreatedAt = now.AddDays(-35), AnimalId = umka.Id, IsProcessed = true, IsRecurring = false },
            new() { DonorName = "Анонимный даритель", Amount = 3000m, Purpose = DonationPurposes.Treatment, CreatedAt = now.AddDays(-30), AnimalId = umka.Id, IsProcessed = true },
            new() { DonorName = "ООО «АгроТрейд»", Amount = 25000m, Purpose = DonationPurposes.Enclosure, Comment = "На обустройство теневого навеса", CreatedAt = now.AddDays(-26), IsProcessed = true },
            new() { DonorName = "Алексей П.", Amount = 500m, Purpose = DonationPurposes.Food, IsRecurring = true, CreatedAt = now.AddDays(-21), IsProcessed = true },
            new() { DonorName = "Мария и Пётр", Amount = 2000m, Purpose = DonationPurposes.Treatment, Comment = "Поправляйся, Умка!", CreatedAt = now.AddDays(-18), AnimalId = umka.Id, IsProcessed = true },
            new() { DonorName = "Посетители выходного дня", Amount = 4200m, Purpose = DonationPurposes.Free, Comment = "Общий ящик для пожертвований у вольера", CreatedAt = now.AddDays(-12), IsProcessed = true },
            new() { DonorName = "Студенческий отряд «Волк-45»", Amount = 12000m, Purpose = DonationPurposes.Food, CreatedAt = now.AddDays(-9), AnimalId = umka.Id, IsProcessed = true },
            new() { DonorName = "Наталья Ж.", Amount = 350m, Purpose = DonationPurposes.Food, IsRecurring = true, CreatedAt = now.AddDays(-7), IsProcessed = true },
            new() { DonorName = "Анонимный даритель", Amount = 10000m, Purpose = DonationPurposes.Treatment, CreatedAt = now.AddDays(-5), AnimalId = umka.Id, IsProcessed = false },
            new() { DonorName = "Семья Гринько", Amount = 1200m, Purpose = DonationPurposes.Free, Comment = "Свободное пожертвование на нужды вольера", CreatedAt = now.AddDays(-3), IsProcessed = false },
            new() { DonorName = "Егор В.", Amount = 700m, Purpose = DonationPurposes.Food, CreatedAt = now.AddDays(-2), IsProcessed = false },
            new() { DonorName = "Туристическая группа «Кавказ-Тур»", Amount = 5300m, Purpose = DonationPurposes.Food, Comment = "Экскурсия 28 человек, спасибо за стаю!", CreatedAt = now.AddHours(-30), AnimalId = umka.Id, IsProcessed = false },
            new() { DonorName = "Ксения Л.", Amount = 300m, Purpose = DonationPurposes.Free, CreatedAt = now.AddHours(-9), IsProcessed = false }
        };
        db.Donations.AddRange(donations);
        await db.SaveChangesAsync();

        foreach (var d in donations)
            AddAudit("Donation", d.Id.ToString(), "DonationCreated",
                $"Донат {d.Amount:N0} ₽ на «{d.Purpose}» от {d.DonorName}", admin, d.CreatedAt);

        // ---------------------------------------------------- Входы в систему
        AddAudit("AppUser", keeper.Id, "Login", $"Вход в систему: {Name(keeper)} ({Roles.ToRussian(keeper.Role)})", keeper, now.AddHours(-6).AddMinutes(-12));
        AddAudit("AppUser", vet.Id, "Login", $"Вход в систему: {Name(vet)} ({Roles.ToRussian(vet.Role)})", vet, now.AddDays(-1).AddHours(-3));
        AddAudit("AppUser", admin.Id, "Login", $"Вход в систему: {Name(admin)} ({Roles.ToRussian(admin.Role)})", admin, now.AddDays(-2).AddHours(-4));
        AddAudit("AppUser", keeper2.Id, "Login", $"Вход в систему: {Name(keeper2)} ({Roles.ToRussian(keeper2.Role)})", keeper2, now.AddDays(-3).AddHours(-5));

        db.AuditLogs.AddRange(audit.OrderBy(a => a.TimestampUtc));
        await db.SaveChangesAsync();
    }
}
