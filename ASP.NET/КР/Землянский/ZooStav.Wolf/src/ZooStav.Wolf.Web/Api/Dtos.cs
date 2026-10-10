using ZooStav.Wolf.Web.Models;

namespace ZooStav.Wolf.Web.Api;

// ---------- DTO для API ----------
// Дневник читается и пишется строго через API (веб-страницы ходят в те же самые
// обработчики через общий слой DiaryService), хранение — в БД.

public record LoginRequest(string UserName, string Password);

public record LoginResponse(
    string AccessToken,
    string TokenType,
    DateTime ExpiresAtUtc,
    string UserId,
    string UserName,
    string DisplayName,
    string Role,
    string Position,
    string[] Permissions);

public record AnimalDto(
    int Id, string Name, string Species, string LatinName, string SubdomainSlug,
    string Gender, DateTime? BirthDate, int? AgeYears, string Enclosure, string Status,
    string ConservationStatus, string ShortDescription, string History, string HeroImageUrl,
    decimal? WeightKg, decimal? HeightCm, string? PartnerName, string PageUrl);

public record FeedingStatusDto(
    DateTime? LastFeedingAt, string LastFeedingSpecialist, string LastFoodType,
    double? LastFoodAmountKg, double? HoursSinceLastFeeding, bool IsOverdue,
    int FeedingsLast24h, int FeedingsLast7Days, string NextPlannedFeeding);

public record DiaryEntryDto(
    int Id, int AnimalId, string AnimalName, string Type, string TypeCode,
    DateTime OccurredAt, string Title, string Details,
    string? FoodType, decimal? FoodAmountKg, string? Medication, string? Dosage,
    string? PartnerName, int? OffspringCount, decimal? WeightKg, decimal? TemperatureC,
    string? Diagnosis, bool IsPublic,
    string AuthorId, string AuthorName, string AuthorRole, string AuthorPosition,
    DateTime CreatedAt, DateTime UpdatedAt,
    int CommentCount, List<DiaryCommentDto> Comments);

public record DiaryCommentDto(int Id, int DiaryEntryId, string Text, string AuthorId, string AuthorName,
    string AuthorRole, DateTime CreatedAt, DateTime? UpdatedAt);

public record AuditLogDto(long Id, string EntityName, string EntityId, string Action, string Summary,
    string UserId, string UserName, string UserRole, string? IpAddress, DateTime TimestampUtc);

public record MediaDto(int Id, string Kind, string Title, string Url, string? PosterUrl, string? Description);

public record DonationRequest(string? DonorName, string? Email, decimal Amount, string? Purpose, bool IsRecurring, string? Comment);

public record DonationResponse(int Id, string DonorName, decimal Amount, string Currency, string Purpose,
    bool IsRecurring, DateTime CreatedAt, string Message);

public record DonationSummaryDto(decimal Total, decimal Food, decimal Treatment, decimal Enclosure,
    decimal Free, int Count, int PercentFood, int PercentTreatment);

public record CreateDiaryEntryRequest(
    int AnimalId,
    DiaryEntryType Type,
    DateTime OccurredAt,
    string Title,
    string Details,
    string? FoodType = null,
    decimal? FoodAmountKg = null,
    string? Medication = null,
    string? Dosage = null,
    string? PartnerName = null,
    int? OffspringCount = null,
    decimal? WeightKg = null,
    decimal? TemperatureC = null,
    string? Diagnosis = null,
    bool IsPublic = true);

public record UpdateDiaryEntryRequest(
    DiaryEntryType? Type = null,
    DateTime? OccurredAt = null,
    string? Title = null,
    string? Details = null,
    string? FoodType = null,
    decimal? FoodAmountKg = null,
    string? Medication = null,
    string? Dosage = null,
    int? OffspringCount = null,
    decimal? WeightKg = null,
    decimal? TemperatureC = null,
    string? Diagnosis = null,
    bool? IsPublic = null);

public record CommentRequest(string Text);

public record DiaryQuery(
    DateTime? From = null,
    DateTime? To = null,
    DiaryEntryType? Type = null,
    string? AuthorId = null,
    int? AnimalId = null,
    string? Search = null,
    bool IncludeNonPublic = false,
    string Sort = "date_desc",
    int Page = 1,
    int PageSize = 10);

public record PagedResponse<T>(List<T> Items, int Page, int PageSize, int TotalCount, int TotalPages);

public record ApiError(string Error, string? Detail = null);
