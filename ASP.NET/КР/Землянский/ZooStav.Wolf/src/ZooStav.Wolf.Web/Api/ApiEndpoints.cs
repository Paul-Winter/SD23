using Microsoft.EntityFrameworkCore;
using ZooStav.Wolf.Web.Data;
using ZooStav.Wolf.Web.Infrastructure;
using ZooStav.Wolf.Web.Models;

namespace ZooStav.Wolf.Web.Api;

/// <summary>
/// API приложения: публичная информация о животном, дневник особи (чтение/запись),
/// комментарии, донаты, служебные отчёты и журнал изменений.
/// Публичные методы — без токена, служебные — JWT (Bearer) или cookie работника.
/// </summary>
public static class ZooApiEndpoints
{
    public static void Map(WebApplication app)
    {
        // Основной префикс. На поддомене животного (wolf.zoostav.ru/api/...) путь сохраняется,
        // поэтому один и тот же API доступен и с главного домена, и с поддомена.
        var api = app.MapGroup("/api");

        MapAuth(api);
        MapPublic(api);
        MapDiary(api);
        MapComments(api);
        MapStaff(api);
        MapAudit(api);
        MapDonations(api);
        MapMeta(api);
    }

    // ---------------------------------------------------------------- Авторизация
    private static void MapAuth(RouteGroupBuilder api)
    {
        var g = api.MapGroup("/auth").WithTags("Авторизация");

        g.MapPost("/login", async (LoginRequest req, AppDbContext db, JwtTokenService jwt,
            PasswordService passwords, AuditService audit, CurrentUser current) =>
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.UserName == req.UserName);
            if (user == null || !user.IsActive || !passwords.Verify(user, req.Password))
                return Results.Json(new ApiError("Неверный логин или пароль"), statusCode: StatusCodes.Status401Unauthorized);

            user.LastLoginAt = DateTime.UtcNow;
            await db.SaveChangesAsync();

            var token = jwt.CreateToken(user);
            await audit.LogAsAsync(user.Id, user.DisplayName, user.Role, current.IpAddress,
                "AppUser", user.Id, "Login",
                $"Вход в API: {user.DisplayName} ({Roles.ToRussian(user.Role)})");

            var perms = new List<string>();
            if (Permissions.CanEditDiary(user.Role)) perms.Add("diary:write");
            if (Permissions.CanManageUsers(user.Role)) perms.Add("users:manage");
            if (Permissions.CanDeleteAnyEntry(user.Role)) perms.Add("diary:delete");
            if (Permissions.CanViewAudit(user.Role)) perms.Add("audit:read");
            if (Permissions.CanViewNonPublic(user.Role)) perms.Add("diary:read:internal");

            return Results.Ok(new LoginResponse(token.Token, "Bearer", token.ExpiresAtUtc,
                user.Id, user.UserName, user.DisplayName, user.Role, user.Position, perms.ToArray()));
        })
        .AllowAnonymous()
        .WithSummary("Получить JWT-токен (логин работника)")
        .WithDescription("Демо-логины: keeper / Keeper#2026, vet / Vet#2026, admin / Admin#2026, intern / Intern#2026");

        g.MapGet("/me", async (CurrentUser current, AppDbContext db) =>
        {
            if (!current.IsAuthenticated) return Results.Unauthorized();
            var user = await db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == current.Id);
            if (user == null) return Results.Unauthorized();
            return Results.Ok(new
            {
                user.Id, user.UserName, user.DisplayName, user.Position, user.Role,
                RoleRussian = Roles.ToRussian(user.Role),
                user.Email, user.LastLoginAt,
                Permissions = BuildPermissions(user.Role)
            });
        })
        .RequireAuthorization("ApiAuth")
        .WithTags("Авторизация")
        .WithSummary("Профиль текущего пользователя и его права");
    }

    // ---------------------------------------------------------------- Публичная информация
    private static void MapPublic(RouteGroupBuilder api)
    {
        var g = api.MapGroup("/animals").WithTags("Животные (публично)");

        g.MapGet("", async (AppDbContext db, IConfiguration cfg) =>
        {
            var baseUrl = cfg["Zoo:PublicBaseUrl"] ?? "https://wolf.zoostav.ru";
            var animals = await db.Animals.Include(a => a.Partner).AsNoTracking().OrderBy(a => a.Id).ToListAsync();
            return Results.Ok(animals.Select(a => a.ToDto(baseUrl)));
        })
        .AllowAnonymous()
        .WithSummary("Список животных зоопарка (стаи)");

        g.MapGet("/{slug}", async (string slug, AppDbContext db, DiaryService diary, IConfiguration cfg) =>
        {
            var animal = await db.Animals.Include(a => a.Partner).Include(a => a.Media)
                .AsNoTracking().FirstOrDefaultAsync(a => a.SubdomainSlug == slug);
            if (animal == null) return Results.NotFound(new ApiError($"Животное «{slug}» не найдено"));

            var baseUrl = cfg["Zoo:PublicBaseUrl"] ?? "https://wolf.zoostav.ru";
            var entries = await db.DiaryEntries.Include(e => e.Author)
                .Where(e => e.AnimalId == animal.Id && e.IsPublic)
                .OrderByDescending(e => e.OccurredAt).Take(10).ToListAsync();

            return Results.Ok(new
            {
                Animal = animal.ToDto(baseUrl),
                Media = animal.Media.Where(m => m.IsPublished).Select(m => m.ToDto()),
                Feeding = (await diary.GetFeedingStatusAsync(animal.Id, cfg.GetValue("Zoo:FeedingAlertHours", 24))).ToDto(),
                RecentPublicDiary = entries.Select(e => e.ToDto())
            });
        })
        .AllowAnonymous()
        .WithSummary("Карточка животного по поддомену (слагу): фото, видео, веб-камера, публичный дневник");

        g.MapGet("/{slug}/media", async (string slug, AppDbContext db) =>
        {
            var animal = await db.Animals.FirstOrDefaultAsync(a => a.SubdomainSlug == slug);
            if (animal == null) return Results.NotFound(new ApiError($"Животное «{slug}» не найдено"));
            var media = await db.Media.Where(m => m.AnimalId == animal.Id && m.IsPublished).ToListAsync();
            return Results.Ok(media.Select(m => m.ToDto()));
        })
        .AllowAnonymous()
        .WithSummary("Фото, видео и веб-камера животного");

        api.MapGet("/feeding-status", async (string? slug, int? animalId, AppDbContext db, DiaryService diary, IConfiguration cfg) =>
        {
            var animal = animalId.HasValue
                ? await db.Animals.FirstOrDefaultAsync(a => a.Id == animalId)
                : await db.Animals.FirstOrDefaultAsync(a => a.SubdomainSlug == slug);
            if (animal == null) return Results.NotFound(new ApiError("Животное не найдено"));
            var status = await diary.GetFeedingStatusAsync(animal.Id, cfg.GetValue("Zoo:FeedingAlertHours", 24));
            return Results.Ok(status.ToDto());
        })
        .AllowAnonymous()
        .WithTags("Животные (публично)")
        .WithSummary("Последняя кормёжка: время, специалист, рацион");
    }

    // ---------------------------------------------------------------- Дневник
    private static void MapDiary(RouteGroupBuilder api)
    {
        var g = api.MapGroup("/diary").WithTags("Дневник особи");

        g.MapGet("", async (DiaryService diary, CurrentUser current,
            DateTime? from, DateTime? to, DiaryEntryType? type, string? authorId, int? animalId,
            string? search, bool? includeNonPublic, string? sort, int page = 1, int pageSize = 10) =>
        {
            var filter = new DiaryFilter
            {
                From = from, To = to, Type = type, AuthorId = authorId, AnimalId = animalId,
                Search = search, IncludeNonPublic = includeNonPublic == true, Sort = sort ?? "date_desc",
                Page = page, PageSize = pageSize
            };

            if (filter.IncludeNonPublic && !current.IsEmployee)
                return Results.Json(new ApiError("Просмотр служебных записей доступен только работникам зоопарка"),
                    statusCode: StatusCodes.Status403Forbidden);

            var result = await diary.GetPagedAsync(filter, current.IsEmployee);
            return Results.Ok(result.ToPaged(e => e.ToDto()));
        })
        // без AllowAnonymous: метод открыт для всех, но при наличии токена работника учитывается его роль
        .WithSummary("Лента дневника с фильтрацией: дата с/по, тип, автор, животное, поиск")
        .WithDescription("Параметры: from, to, type (Feeding|Vaccination|Mating|Offspring|Illness|Treatment|Observation|Weighing|Examination|Relocation|Comment), " +
                         "authorId, animalId, search, includeNonPublic (только для работников), sort=date_desc|date_asc, page, pageSize");

        g.MapGet("/{id:int}", async (int id, AppDbContext db, CurrentUser current) =>
        {
            var entry = await db.DiaryEntries.Include(e => e.Author).Include(e => e.Animal)
                .Include(e => e.Comments).ThenInclude(c => c.Author)
                .AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            if (entry == null) return Results.NotFound(new ApiError("Запись не найдена"));
            if (!entry.IsPublic && !current.IsEmployee) return Results.Forbid();
            return Results.Ok(entry.ToDto());
        })
        // без AllowAnonymous: метод открыт для всех, но при наличии токена работник распознаётся
        .WithSummary("Запись дневника по идентификатору");

        g.MapPost("", async (CreateDiaryEntryRequest req, AppDbContext db, CurrentUser current, AuditService audit) =>
        {
            if (!Permissions.CanEditDiary(current.Role))
                return Results.Json(new ApiError("Недостаточно прав: нужна роль Admin, Keeper или Veterinarian"),
                    statusCode: StatusCodes.Status403Forbidden);
            if (string.IsNullOrWhiteSpace(req.Title))
                return Results.ValidationProblem(new Dictionary<string, string[]> { ["title"] = new[] { "Заголовок обязателен" } });
            if (!await db.Animals.AnyAsync(a => a.Id == req.AnimalId))
                return Results.NotFound(new ApiError("Животное не найдено"));

            var entry = new DiaryEntry
            {
                AnimalId = req.AnimalId,
                Type = req.Type,
                OccurredAt = req.OccurredAt == default ? DateTime.Now : req.OccurredAt,
                Title = req.Title.Trim(),
                Details = req.Details ?? string.Empty,
                FoodType = req.FoodType,
                FoodAmountKg = req.FoodAmountKg,
                Medication = req.Medication,
                Dosage = req.Dosage,
                PartnerName = req.PartnerName,
                OffspringCount = req.OffspringCount,
                WeightKg = req.WeightKg,
                TemperatureC = req.TemperatureC,
                Diagnosis = req.Diagnosis,
                IsPublic = req.IsPublic,
                AuthorId = current.Id!,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            db.DiaryEntries.Add(entry);
            await db.SaveChangesAsync();

            await audit.LogAsync("DiaryEntry", entry.Id, "Created",
                $"Создана запись «{entry.Title}» ({entry.Type.ToRussian()}) на {entry.OccurredAt:dd.MM.yyyy HH:mm}");

            var created = await db.DiaryEntries.Include(e => e.Author).Include(e => e.Animal)
                .FirstAsync(e => e.Id == entry.Id);
            return Results.Created($"/api/diary/{entry.Id}", created.ToDto());
        })
        .RequireAuthorization("ApiDiaryEditor")
        .WithSummary("Создать запись дневника (кормёжка, вакцинация, спаривание, потомство, болезнь и др.)");

        g.MapPut("/{id:int}", async (int id, UpdateDiaryEntryRequest req, AppDbContext db, CurrentUser current, AuditService audit) =>
        {
            var entry = await db.DiaryEntries.Include(e => e.Author).FirstOrDefaultAsync(e => e.Id == id);
            if (entry == null) return Results.NotFound(new ApiError("Запись не найдена"));
            if (!Permissions.CanEditDiary(current.Role))
                return Results.Json(new ApiError("Недостаточно прав для изменения дневника"),
                    statusCode: StatusCodes.Status403Forbidden);
            if (current.Role != Roles.Admin && entry.AuthorId != current.Id)
                return Results.Json(new ApiError("Кипер/ветеринар может изменять только свои записи (или обратитесь к администратору)"),
                    statusCode: StatusCodes.Status403Forbidden);

            var before = new DiaryEntry
            {
                Type = entry.Type, OccurredAt = entry.OccurredAt, Title = entry.Title, Details = entry.Details,
                FoodType = entry.FoodType, FoodAmountKg = entry.FoodAmountKg, Medication = entry.Medication,
                Dosage = entry.Dosage, Diagnosis = entry.Diagnosis, WeightKg = entry.WeightKg, IsPublic = entry.IsPublic
            };

            if (req.Type.HasValue) entry.Type = req.Type.Value;
            if (req.OccurredAt.HasValue) entry.OccurredAt = req.OccurredAt.Value;
            if (req.Title != null) entry.Title = req.Title.Trim();
            if (req.Details != null) entry.Details = req.Details;
            if (req.FoodType != null) entry.FoodType = req.FoodType;
            if (req.FoodAmountKg.HasValue) entry.FoodAmountKg = req.FoodAmountKg;
            if (req.Medication != null) entry.Medication = req.Medication;
            if (req.Dosage != null) entry.Dosage = req.Dosage;
            if (req.OffspringCount.HasValue) entry.OffspringCount = req.OffspringCount;
            if (req.WeightKg.HasValue) entry.WeightKg = req.WeightKg;
            if (req.TemperatureC.HasValue) entry.TemperatureC = req.TemperatureC;
            if (req.Diagnosis != null) entry.Diagnosis = req.Diagnosis;
            if (req.IsPublic.HasValue) entry.IsPublic = req.IsPublic.Value;
            entry.UpdatedAt = DateTime.UtcNow;

            await db.SaveChangesAsync();

            var diff = AuditService.Diff(before, entry);
            await audit.LogAsync("DiaryEntry", entry.Id, "Updated",
                $"Изменена запись «{entry.Title}» (№{entry.Id}). {diff}");

            return Results.Ok(entry.ToDto());
        })
        .RequireAuthorization("ApiDiaryEditor")
        .WithSummary("Изменить запись дневника (все изменения фиксируются в журнале: кто и когда)");

        g.MapDelete("/{id:int}", async (int id, AppDbContext db, CurrentUser current, AuditService audit) =>
        {
            var entry = await db.DiaryEntries.FirstOrDefaultAsync(e => e.Id == id);
            if (entry == null) return Results.NotFound(new ApiError("Запись не найдена"));
            if (!Permissions.CanDeleteAnyEntry(current.Role))
                return Results.Json(new ApiError("Удаление записей доступно только администратору"),
                    statusCode: StatusCodes.Status403Forbidden);

            var title = entry.Title;
            db.DiaryEntries.Remove(entry);
            await db.SaveChangesAsync();
            await audit.LogAsync("DiaryEntry", id, "Deleted", $"Удалена запись «{title}» (№{id})");
            return Results.NoContent();
        })
        .RequireAuthorization("ApiAdminOnly")
        .WithSummary("Удалить запись дневника (только администратор)");

        g.MapGet("/{id:int}/history", async (int id, AppDbContext db, CurrentUser current) =>
        {
            if (!Permissions.CanViewAudit(current.Role) && !current.IsEmployee) return Results.Forbid();
            var history = await db.AuditLogs.Where(a => a.EntityName == "DiaryEntry" && a.EntityId == id.ToString())
                .OrderBy(a => a.TimestampUtc).ToListAsync();
            return Results.Ok(history.Select(a => a.ToDto()));
        })
        .RequireAuthorization("ApiEmployee")
        .WithSummary("История изменений конкретной записи дневника (кто и когда менял)");
    }

    // ---------------------------------------------------------------- Комментарии
    private static void MapComments(RouteGroupBuilder api)
    {
        var g = api.MapGroup("/diary/{entryId:int}/comments").WithTags("Комментарии работников");

        g.MapGet("", async (int entryId, AppDbContext db, CurrentUser current) =>
        {
            var entry = await db.DiaryEntries.AsNoTracking().FirstOrDefaultAsync(e => e.Id == entryId);
            if (entry == null) return Results.NotFound(new ApiError("Запись не найдена"));
            if (!entry.IsPublic && !current.IsEmployee) return Results.Forbid();

            var comments = await db.DiaryComments.Include(c => c.Author)
                .Where(c => c.DiaryEntryId == entryId).OrderBy(c => c.CreatedAt).ToListAsync();
            return Results.Ok(comments.Select(c => c.ToDto()));
        })
        // без AllowAnonymous: служебные записи видны только работнику с токеном
        .WithSummary("Комментарии к записи (с указанием специалиста и времени)");

        g.MapPost("", async (int entryId, CommentRequest req, AppDbContext db, CurrentUser current, AuditService audit) =>
        {
            if (!current.IsEmployee)
                return Results.Json(new ApiError("Комментарии могут оставлять только работники зоопарка"),
                    statusCode: StatusCodes.Status403Forbidden);
            if (string.IsNullOrWhiteSpace(req.Text))
                return Results.ValidationProblem(new Dictionary<string, string[]> { ["text"] = new[] { "Текст комментария пуст" } });
            var entry = await db.DiaryEntries.FirstOrDefaultAsync(e => e.Id == entryId);
            if (entry == null) return Results.NotFound(new ApiError("Запись не найдена"));

            var comment = new DiaryComment
            {
                DiaryEntryId = entryId,
                Text = req.Text.Trim(),
                AuthorId = current.Id!,
                CreatedAt = DateTime.UtcNow
            };
            db.DiaryComments.Add(comment);
            await db.SaveChangesAsync();
            await audit.LogAsync("DiaryComment", comment.Id, "CommentAdded",
                $"Добавлен комментарий к записи №{entryId} «{entry.Title}»: {Truncate(comment.Text, 120)}");

            var created = await db.DiaryComments.Include(c => c.Author).FirstAsync(c => c.Id == comment.Id);
            return Results.Created($"/api/diary/{entryId}/comments/{comment.Id}", created.ToDto());
        })
        .RequireAuthorization("ApiEmployee")
        .WithSummary("Добавить комментарий от имени работника (фиксируются автор, должность и время)");

        api.MapDelete("/diary/comments/{id:int}", async (int id, AppDbContext db, CurrentUser current, AuditService audit) =>
        {
            var c = await db.DiaryComments.FirstOrDefaultAsync(x => x.Id == id);
            if (c == null) return Results.NotFound(new ApiError("Комментарий не найден"));
            if (!Permissions.CanModerateComments(current.Role) && c.AuthorId != current.Id) return Results.Forbid();

            db.DiaryComments.Remove(c);
            await db.SaveChangesAsync();
            await audit.LogAsync("DiaryComment", id, "CommentDeleted", $"Удалён комментарий №{id} к записи №{c.DiaryEntryId}");
            return Results.NoContent();
        })
        .RequireAuthorization("ApiEmployee")
        .WithTags("Комментарии работников")
        .WithSummary("Удалить комментарий (автор или администратор)");
    }

    // ---------------------------------------------------------------- Служебные разделы
    private static void MapStaff(RouteGroupBuilder api)
    {
        var g = api.MapGroup("/staff").WithTags("Кабинет работника");

        g.MapGet("/dashboard", async (string? slug, int? animalId, AppDbContext db, DiaryService diary, IConfiguration cfg) =>
        {
            var animal = animalId.HasValue
                ? await db.Animals.FirstOrDefaultAsync(a => a.Id == animalId)
                : await db.Animals.FirstOrDefaultAsync(a => a.SubdomainSlug == slug);
            if (animal == null) return Results.NotFound(new ApiError("Животное не найдено"));

            var dto = await diary.GetDashboardAsync(animal.Id, cfg.GetValue("Zoo:FeedingAlertHours", 24));
            return Results.Ok(new
            {
                Animal = animal.ToDto(cfg["Zoo:PublicBaseUrl"] ?? "https://wolf.zoostav.ru"),
                Feeding = dto.Feeding.ToDto(),
                dto.TotalEntries,
                dto.NonPublicEntries,
                dto.OpenIllnesses,
                dto.VaccinationsThisYear,
                dto.CommentsLast7Days,
                Donations = new { Food = dto.DonationsFood, Treatment = dto.DonationsTreatment },
                CountByType = dto.CountByType.ToDictionary(k => k.Key.ToRussian(), v => v.Value),
                RecentEntries = dto.RecentEntries.Select(e => e.ToDto()),
                RecentComments = dto.RecentComments.Select(c => c.ToDto()),
                RecentChanges = dto.RecentChanges.Select(a => a.ToDto())
            });
        })
        .RequireAuthorization("ApiEmployee")
        .WithSummary("Сводка для кипера: последняя кормёжка, счётчики, последние изменения и комментарии");

        g.MapGet("/feeding-status", async (string? slug, int? animalId, AppDbContext db, DiaryService diary, IConfiguration cfg) =>
        {
            var animal = animalId.HasValue
                ? await db.Animals.FirstOrDefaultAsync(a => a.Id == animalId)
                : await db.Animals.FirstOrDefaultAsync(a => a.SubdomainSlug == slug);
            if (animal == null) return Results.NotFound(new ApiError("Животное не найдено"));

            var status = await diary.GetFeedingStatusAsync(animal.Id, cfg.GetValue("Zoo:FeedingAlertHours", 24));
            return Results.Ok(new
            {
                Animal = animal.Name,
                LastFeeding = status.LastFeeding?.OccurredAt,
                status.LastFeedingSpecialist,
                status.LastFoodType,
                status.LastFoodAmountKg,
                HoursAgo = status.TimeSinceLastFeeding is TimeSpan tsl ? Math.Round(tsl.TotalHours, 2) : (double?)null,
                status.IsOverdue,
                status.FeedingsLast24h,
                status.FeedingsLast7Days,
                status.NextPlannedFeeding
            });
        })
        .RequireAuthorization("ApiEmployee")
        .WithSummary("Детальный статус кормления (когда кормили в последний раз, кто, чем, не пора ли снова)");

        g.MapGet("/users", async (AppDbContext db) =>
            Results.Ok(await db.Users.AsNoTracking()
                .Select(u => new { u.Id, u.UserName, u.DisplayName, u.Position, u.Role, u.Email, u.IsActive, u.LastLoginAt })
                .ToListAsync()))
        .RequireAuthorization("ApiAdminOnly")
        .WithSummary("Список работников (только администратор)");
    }

    // ---------------------------------------------------------------- Журнал изменений (аудит)
    private static void MapAudit(RouteGroupBuilder api)
    {
        var g = api.MapGroup("/audit").WithTags("Журнал изменений");

        g.MapGet("", async (AppDbContext db, DateTime? from, DateTime? to, string? userId,
            string? action, string? entity, int page = 1, int pageSize = 20) =>
        {
            var q = db.AuditLogs.AsNoTracking().AsQueryable();
            if (from.HasValue) q = q.Where(a => a.TimestampUtc >= from.Value);
            if (to.HasValue) q = q.Where(a => a.TimestampUtc < to.Value.Date.AddDays(1));
            if (!string.IsNullOrEmpty(userId)) q = q.Where(a => a.UserId == userId);
            if (!string.IsNullOrEmpty(action)) q = q.Where(a => a.Action == action);
            if (!string.IsNullOrEmpty(entity)) q = q.Where(a => a.EntityName == entity);

            page = page < 1 ? 1 : page;
            pageSize = pageSize is < 1 or > 200 ? 20 : pageSize;
            var total = await q.CountAsync();
            var items = await q.OrderByDescending(a => a.TimestampUtc)
                .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var pageResult = new PagedResult<AuditLog>
            {
                Items = items, Page = page, PageSize = pageSize, TotalCount = total
            };
            return Results.Ok(pageResult.ToPaged(a => a.ToDto()));
        })
        .RequireAuthorization("ApiAuditReader")
        .WithSummary("Журнал изменений: кто, когда и что изменял (фильтры: дата, пользователь, действие, сущность)");

        g.MapGet("/summary", async (AppDbContext db) =>
        {
            var byUser = await db.AuditLogs.GroupBy(a => new { a.UserName, a.UserRole })
                .Select(gr => new { gr.Key.UserName, gr.Key.UserRole, Count = gr.Count(), Last = gr.Max(a => a.TimestampUtc) })
                .OrderByDescending(x => x.Count).ToListAsync();

            var byAction = await db.AuditLogs.GroupBy(a => a.Action)
                .Select(gr => new { Action = gr.Key, Count = gr.Count() }).ToListAsync();

            return Results.Ok(new { ByUser = byUser, ByAction = byAction });
        })
        .RequireAuthorization("ApiAuditReader")
        .WithSummary("Сводка по изменениям: сколько правок внёс каждый специалист");
    }

    // ---------------------------------------------------------------- Донаты
    private static void MapDonations(RouteGroupBuilder api)
    {
        var g = api.MapGroup("/donations").WithTags("Донаты");

        g.MapGet("/summary", async (DonationService donations) => Results.Ok((await donations.GetSummaryAsync()).ToDto()))
        .AllowAnonymous()
        .WithSummary("Сводка сборов: корм, лечение, вольер, свободное назначение");

        g.MapGet("/recent", async (AppDbContext db, int take = 10) =>
        {
            take = take is < 1 or > 50 ? 10 : take;
            var list = await db.Donations.AsNoTracking().OrderByDescending(d => d.CreatedAt).Take(take)
                .Select(d => new { d.Id, d.DonorName, d.Amount, d.Currency, d.Purpose, d.IsRecurring, d.Comment, d.CreatedAt })
                .ToListAsync();
            return Results.Ok(list);
        })
        .AllowAnonymous()
        .WithSummary("Последние донаты");

        g.MapPost("", async (DonationRequest req, DonationService donations, AuditService audit, CurrentUser current, AppDbContext db) =>
        {
            if (req.Amount < 0) return Results.ValidationProblem(new Dictionary<string, string[]> { ["amount"] = new[] { "Сумма не может быть отрицательной" } });
            var purpose = DonationPurposes.All.Contains(req.Purpose ?? "") ? req.Purpose! : DonationPurposes.Free;

            var donation = await donations.AddAsync(new Donation
            {
                DonorName = string.IsNullOrWhiteSpace(req.DonorName) ? "Анонимный даритель" : req.DonorName!.Trim(),
                Email = req.Email,
                Amount = req.Amount,
                Purpose = purpose,
                IsRecurring = req.IsRecurring,
                Comment = req.Comment,
                Provider = "demo",
                AnimalId = await db.Animals.Where(a => a.IsMainPage).Select(a => (int?)a.Id).FirstOrDefaultAsync()
            });

            await audit.LogAsync("Donation", donation.Id, "DonationCreated",
                $"Донат {donation.Amount:N0} ₽ на «{donation.Purpose}» от {donation.DonorName}" +
                (string.IsNullOrEmpty(current.UserName) ? "" : $" (авторизован: {current.UserName})"));

            return Results.Created($"/api/donations/{donation.Id}", new DonationResponse(
                donation.Id, donation.DonorName, donation.Amount, donation.Currency, donation.Purpose,
                donation.IsRecurring, donation.CreatedAt,
                "Спасибо! Донат принят в демонстрационном режиме (реальный платёж не проводился)."));
        })
        // без AllowAnonymous: донат могут оформить все, но для авторизованного работника это фиксируется в журнале
        .WithSummary("Оформить донат, в том числе free donation (любая сумма, назначение — свободное)");

        g.MapGet("/staff/list", async (AppDbContext db, string? purpose, DateTime? from, DateTime? to, bool? onlyUnprocessed, int page = 1, int pageSize = 20) =>
        {
            var q = db.Donations.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(purpose)) q = q.Where(d => d.Purpose == purpose);
            if (from.HasValue) q = q.Where(d => d.CreatedAt >= from.Value);
            if (to.HasValue) q = q.Where(d => d.CreatedAt < to.Value.Date.AddDays(1));
            if (onlyUnprocessed == true) q = q.Where(d => !d.IsProcessed);

            page = page < 1 ? 1 : page;
            var total = await q.CountAsync();
            var items = await q.OrderByDescending(d => d.CreatedAt).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var pageResult = new PagedResult<Donation> { Items = items, Page = page, PageSize = pageSize, TotalCount = total };
            return Results.Ok(pageResult.ToPaged(d => new
            {
                d.Id, d.DonorName, d.Email, d.Amount, d.Currency, d.Purpose, d.IsRecurring,
                d.Comment, d.Provider, d.CreatedAt, d.IsProcessed, d.AnimalId
            }));
        })
        .RequireAuthorization("ApiEmployee")
        .WithTags("Донаты")
        .WithSummary("Список донатов для работников (фильтры: назначение, дата, необработанные)");
    }

    // ---------------------------------------------------------------- Справочники
    private static void MapMeta(RouteGroupBuilder api)
    {
        api.MapGet("/meta/enums", () => Results.Ok(new
        {
            DiaryEntryTypes = Enum.GetValues<DiaryEntryType>()
                .Select(t => new { Code = t.ToString(), Value = (int)t, Title = t.ToRussian() }),
            Roles = Roles.Employees.Select(r => new { Code = r, Title = Roles.ToRussian(r) }),
            DonationPurposes = DonationPurposes.All
        }))
        .AllowAnonymous()
        .WithTags("Справочники")
        .WithSummary("Справочники: типы записей дневника, роли, назначения донатов");
    }

    private static string[] BuildPermissions(string role)
    {
        var perms = new List<string>();
        if (Permissions.CanEditDiary(role)) perms.Add("diary:write");
        if (Permissions.CanDeleteAnyEntry(role)) perms.Add("diary:delete");
        if (Permissions.CanManageUsers(role)) perms.Add("users:manage");
        if (Permissions.CanViewAudit(role)) perms.Add("audit:read");
        if (Permissions.CanViewNonPublic(role)) perms.Add("diary:read:internal");
        if (Permissions.CanModerateComments(role)) perms.Add("comments:moderate");
        return perms.ToArray();
    }

    private static string Truncate(string text, int len) => text.Length <= len ? text : text[..len] + "…";
}
