using ZooStav.Wolf.Web.Data;
using ZooStav.Wolf.Web.Models;

namespace ZooStav.Wolf.Web.Infrastructure;


public class AuditService
{
    private readonly AppDbContext _db;
    private readonly CurrentUser _user;

    public AuditService(AppDbContext db, CurrentUser user)
    {
        _db = db;
        _user = user;
    }

    public Task LogAsync(string entityName, object entityId, string action, string summary)
        => LogAsAsync(_user.Id ?? "anonymous", _user.DisplayName ?? "Гость", _user.Role ?? "Guest", _user.IpAddress,
            entityName, entityId, action, summary);

 
    public async Task LogAsAsync(string userId, string userName, string userRole, string? ipAddress,
        string entityName, object entityId, string action, string summary)
    {
        _db.AuditLogs.Add(new AuditLog
        {
            EntityName = entityName,
            EntityId = entityId.ToString() ?? string.Empty,
            Action = action,
            Summary = summary,
            UserId = userId,
            UserName = userName,
            UserRole = userRole,
            IpAddress = ipAddress,
            TimestampUtc = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();
    }

    public static string Diff(DiaryEntry before, DiaryEntry after)
    {
        var parts = new List<string>();
        void Cmp(string label, object? a, object? b)
        {
            if (!Equals(a?.ToString(), b?.ToString()))
                parts.Add($"{label}: «{a}» → «{b}»");
        }

        Cmp("дата события", before.OccurredAt.ToString("dd.MM.yyyy HH:mm"), after.OccurredAt.ToString("dd.MM.yyyy HH:mm"));
        Cmp("тип", before.Type.ToRussian(), after.Type.ToRussian());
        Cmp("заголовок", before.Title, after.Title);
        Cmp("описание", before.Details, after.Details);
        Cmp("рацион", before.FoodType, after.FoodType);
        Cmp("масса корма", before.FoodAmountKg, after.FoodAmountKg);
        Cmp("препарат", before.Medication, after.Medication);
        Cmp("дозировка", before.Dosage, after.Dosage);
        Cmp("диагноз", before.Diagnosis, after.Diagnosis);
        Cmp("вес", before.WeightKg, after.WeightKg);
        Cmp("публичность", before.IsPublic ? "публичная" : "служебная", after.IsPublic ? "публичная" : "служебная");

        return parts.Count == 0 ? "без изменений полей" : string.Join("; ", parts);
    }
}
