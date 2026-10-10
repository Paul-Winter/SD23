using System.ComponentModel.DataAnnotations;
using ZooStav.Wolf.Web.Models;

namespace ZooStav.Wolf.Web.Infrastructure;

public class DiaryEntryInput
{
    [Display(Name = "Особь")]
    public int AnimalId { get; set; }

    [Display(Name = "Тип записи")]
    public DiaryEntryType Type { get; set; } = DiaryEntryType.Feeding;

    [Display(Name = "Дата и время события")]
    public DateTime OccurredAt { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Укажите заголовок записи")]
    [Display(Name = "Заголовок")]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "Подробности (что наблюдалось, как перенесено)")]
    public string Details { get; set; } = string.Empty;

    // --- Кормёжка ---
    [Display(Name = "Рацион / вид корма")]
    [MaxLength(200)]
    public string? FoodType { get; set; }

    [Display(Name = "Масса корма, кг")]
    [Range(0, 1000, ErrorMessage = "Некорректная масса")]
    public decimal? FoodAmountKg { get; set; }

    // --- Вакцинация / лечение ---
    [Display(Name = "Препарат / вакцина")]
    [MaxLength(200)]
    public string? Medication { get; set; }

    [Display(Name = "Дозировка и схема")]
    [MaxLength(100)]
    public string? Dosage { get; set; }

    // --- Спаривание / потомство ---
    [Display(Name = "Партнёр")]
    [MaxLength(200)]
    public string? PartnerName { get; set; }

    [Display(Name = "Количество детёнышей")]
    [Range(0, 30, ErrorMessage = "Некорректное количество")]
    public int? OffspringCount { get; set; }

    // --- Взвешивание / осмотр ---
    [Display(Name = "Вес, кг")]
    [Range(0, 300, ErrorMessage = "Некорректный вес")]
    public decimal? WeightKg { get; set; }

    [Display(Name = "Температура, °C")]
    [Range(0, 50, ErrorMessage = "Некорректная температура")]
    public decimal? TemperatureC { get; set; }

    // --- Болезнь ---
    [Display(Name = "Диагноз (служебное поле)")]
    [MaxLength(200)]
    public string? Diagnosis { get; set; }

    [Display(Name = "Видна посетителям (публичная запись)")]
    public bool IsPublic { get; set; } = true;

    public DiaryEntry ToEntity(string authorId) => new()
    {
        AnimalId = AnimalId,
        Type = Type,
        OccurredAt = OccurredAt,
        Title = Title.Trim(),
        Details = Details ?? string.Empty,
        FoodType = FoodType,
        FoodAmountKg = FoodAmountKg,
        Medication = Medication,
        Dosage = Dosage,
        PartnerName = PartnerName,
        OffspringCount = OffspringCount,
        WeightKg = WeightKg,
        TemperatureC = TemperatureC,
        Diagnosis = Diagnosis,
        IsPublic = IsPublic,
        AuthorId = authorId,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    };

    public void ApplyTo(DiaryEntry entry)
    {
        entry.AnimalId = AnimalId;
        entry.Type = Type;
        entry.OccurredAt = OccurredAt;
        entry.Title = Title.Trim();
        entry.Details = Details ?? string.Empty;
        entry.FoodType = FoodType;
        entry.FoodAmountKg = FoodAmountKg;
        entry.Medication = Medication;
        entry.Dosage = Dosage;
        entry.PartnerName = PartnerName;
        entry.OffspringCount = OffspringCount;
        entry.WeightKg = WeightKg;
        entry.TemperatureC = TemperatureC;
        entry.Diagnosis = Diagnosis;
        entry.IsPublic = IsPublic;
        entry.UpdatedAt = DateTime.UtcNow;
    }

    public static DiaryEntryInput From(DiaryEntry e) => new()
    {
        AnimalId = e.AnimalId,
        Type = e.Type,
        OccurredAt = e.OccurredAt,
        Title = e.Title,
        Details = e.Details,
        FoodType = e.FoodType,
        FoodAmountKg = e.FoodAmountKg,
        Medication = e.Medication,
        Dosage = e.Dosage,
        PartnerName = e.PartnerName,
        OffspringCount = e.OffspringCount,
        WeightKg = e.WeightKg,
        TemperatureC = e.TemperatureC,
        Diagnosis = e.Diagnosis,
        IsPublic = e.IsPublic
    };
}
