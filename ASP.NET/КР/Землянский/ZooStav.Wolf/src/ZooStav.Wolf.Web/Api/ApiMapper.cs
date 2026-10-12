using ZooStav.Wolf.Web.Infrastructure;
using ZooStav.Wolf.Web.Models;

namespace ZooStav.Wolf.Web.Api;

public static class ApiMapper
{
    public static AnimalDto ToDto(this Animal a, string publicBaseUrl) => new(
        a.Id, a.Name, a.Species, a.LatinName, a.SubdomainSlug, a.Gender, a.BirthDate,
        a.BirthDate == null ? null : (int)((DateTime.Now - a.BirthDate.Value).TotalDays / 365.25),
        a.Enclosure, a.Status, a.ConservationStatus, a.ShortDescription, a.History,
        a.HeroImageUrl, a.WeightKg, a.HeightCm, a.Partner?.Name,
        $"{publicBaseUrl.TrimEnd('/')}/");

    public static MediaDto ToDto(this MediaItem m) => new(m.Id, m.Kind, m.Title, m.Url, m.PosterUrl, m.Description);

    public static DiaryCommentDto ToDto(this DiaryComment c) => new(
        c.Id, c.DiaryEntryId, c.Text, c.AuthorId,
        c.Author?.DisplayName ?? "—", c.Author?.Role ?? "—",
        c.CreatedAt, c.UpdatedAt);

    public static DiaryEntryDto ToDto(this DiaryEntry e) => new(
        e.Id, e.AnimalId, e.Animal?.Name ?? "—",
        e.Type.ToRussian(), e.Type.ToString(),
        e.OccurredAt, e.Title, e.Details, e.FoodType, e.FoodAmountKg, e.Medication, e.Dosage,
        e.PartnerName, e.OffspringCount, e.WeightKg, e.TemperatureC, e.Diagnosis, e.IsPublic,
        e.AuthorId, e.Author?.DisplayName ?? "—", e.Author?.Role ?? "—", e.Author?.Position ?? "—",
        e.CreatedAt, e.UpdatedAt,
        e.Comments?.Count ?? 0,
        e.Comments?.OrderBy(c => c.CreatedAt).Select(c => c.ToDto()).ToList() ?? new List<DiaryCommentDto>());

    public static AuditLogDto ToDto(this AuditLog a) => new(
        a.Id, a.EntityName, a.EntityId, a.Action, a.Summary, a.UserId, a.UserName, a.UserRole, a.IpAddress, a.TimestampUtc);

    public static FeedingStatusDto ToDto(this FeedingStatus f) => new(
        f.LastFeeding?.OccurredAt, f.LastFeedingSpecialist, f.LastFoodType, f.LastFoodAmountKg,
        f.TimeSinceLastFeeding?.TotalHours is double h ? Math.Round(h, 2) : null,
        f.IsOverdue, f.FeedingsLast24h, f.FeedingsLast7Days, f.NextPlannedFeeding);

    public static DonationSummaryDto ToDto(this DonationSummary s) => new(
        s.Total, s.Food, s.Treatment, s.Enclosure, s.Free, s.Count, s.PercentFood, s.PercentTreatment);

    /// <summary>
    /// Преобразование страницы результатов в ответ API. Маппер обязателен: в API уходят
    /// только DTO, «сырые» EF-сущности наружу не отдаются (иначе получаем циклы ссылок).
    /// </summary>
    public static PagedResponse<TDst> ToPaged<TSrc, TDst>(this PagedResult<TSrc> p, Func<TSrc, TDst> map) =>
        new(p.Items.Select(map).ToList(), p.Page, p.PageSize, p.TotalCount, p.TotalPages);
}
