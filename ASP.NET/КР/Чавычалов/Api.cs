using Microsoft.EntityFrameworkCore;

namespace StavZoo;

public static class Api
{
    public static void MapApi(this WebApplication app)
    {
        var api = app.MapGroup("/api");

        // ---------- Аутентификация ----------
        api.MapPost("/auth/login", async (LoginDto dto, AuthService auth) =>
        {
            var token = await auth.LoginAsync(dto.UserName, dto.Password);
            return token is null ? Results.Unauthorized() : Results.Ok(new { token });
        });

        // ---------- Публичная часть ----------
        api.MapGet("/animals", async (ZooDb db) =>
            await db.Animals.Select(a => new { a.Slug, a.Name, a.Species, a.PhotoUrl }).ToListAsync());

        api.MapGet("/animals/{slug}", async (string slug, ZooDb db) =>
            await db.Animals.FirstOrDefaultAsync(a => a.Slug == slug) is { } a
                ? Results.Ok(a) : Results.NotFound());

        api.MapGet("/animals/{slug}/donations/total", async (string slug, ZooDb db) =>
        {
            var a = await db.Animals.FirstOrDefaultAsync(x => x.Slug == slug);
            if (a is null) return Results.NotFound();
            // SQLite не умеет SUM по decimal на стороне БД — считаем в памяти
            var amounts = await db.Donations.Where(d => d.AnimalId == a.Id)
                .Select(d => new { d.Amount, d.Purpose }).ToListAsync();
            return Results.Ok(new
            {
                total = amounts.Sum(x => x.Amount),
                food = amounts.Where(x => x.Purpose == DonationPurpose.Food).Sum(x => x.Amount),
                treatment = amounts.Where(x => x.Purpose == DonationPurpose.Treatment).Sum(x => x.Amount)
            });
        });

        // Free donation (платёжный шлюз не подключён — донат просто фиксируется)
        api.MapPost("/animals/{slug}/donations", async (string slug, DonationDto dto, ZooDb db) =>
        {
            var a = await db.Animals.FirstOrDefaultAsync(x => x.Slug == slug);
            if (a is null) return Results.NotFound();
            if (dto.Amount <= 0 || dto.Amount > 1_000_000)
                return Results.BadRequest(new { error = "Некорректная сумма" });

            var name = string.IsNullOrWhiteSpace(dto.DonorName) ? "Аноним" : dto.DonorName.Trim();
            if (name.Length > 60) name = name[..60];
            db.Donations.Add(new Donation
            {
                AnimalId = a.Id, Amount = dto.Amount, DonorName = name, Purpose = dto.Purpose
            });
            await db.SaveChangesAsync();
            return Results.Ok(new { message = "Спасибо за поддержку!" });
        });

        // ---------- Дневник (работники) ----------
        var staff = api.MapGroup("/animals/{slug}/diary").RequireAuthorization("Staff");

        staff.MapGet("", async (string slug, ZooDb db, DiaryType? type) =>
        {
            var a = await db.Animals.FirstOrDefaultAsync(x => x.Slug == slug);
            if (a is null) return Results.NotFound();
            var q = db.Diary.Where(d => d.AnimalId == a.Id);
            if (type is not null) q = q.Where(d => d.Type == type);
            return Results.Ok(await q.OrderByDescending(d => d.OccurredAt).ToListAsync());
        });

        staff.MapPost("", async (string slug, DiaryDto dto, ZooDb db, HttpContext ctx) =>
        {
            var a = await db.Animals.FirstOrDefaultAsync(x => x.Slug == slug);
            if (a is null) return Results.NotFound();
            if (string.IsNullOrWhiteSpace(dto.Text) || dto.Text.Length > 2000)
                return Results.BadRequest(new { error = "Текст обязателен (до 2000 символов)" });

            var e = new DiaryEntry
            {
                AnimalId = a.Id,
                Type = dto.Type,
                Text = dto.Text.Trim(),
                SubjectName = string.IsNullOrWhiteSpace(dto.SubjectName) ? null : dto.SubjectName.Trim(),
                OccurredAt = (dto.OccurredAt ?? DateTime.UtcNow).ToUniversalTime(),
                Author = ctx.User.Identity?.Name ?? "unknown"
            };
            db.Diary.Add(e);
            await db.SaveChangesAsync();
            return Results.Created($"/api/animals/{slug}/diary/{e.Id}", e);
        });

        // ---------- Администратор ----------
        api.MapDelete("/diary/{id:int}", async (int id, ZooDb db) =>
        {
            var e = await db.Diary.FindAsync(id);
            if (e is null) return Results.NotFound();
            db.Diary.Remove(e);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).RequireAuthorization("AdminOnly");

        api.MapPut("/animals/{slug}", async (string slug, AnimalUpdateDto dto, ZooDb db) =>
        {
            var a = await db.Animals.FirstOrDefaultAsync(x => x.Slug == slug);
            if (a is null) return Results.NotFound();
            a.Biography = dto.Biography;
            if (dto.PhotoUrl is not null) a.PhotoUrl = dto.PhotoUrl;
            if (dto.VideoUrl is not null) a.VideoUrl = dto.VideoUrl;
            if (dto.WebcamUrl is not null) a.WebcamUrl = dto.WebcamUrl;
            await db.SaveChangesAsync();
            return Results.Ok(a);
        }).RequireAuthorization("AdminOnly");
    }
}
