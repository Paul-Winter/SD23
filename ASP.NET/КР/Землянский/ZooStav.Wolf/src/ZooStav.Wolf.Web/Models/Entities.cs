using System.ComponentModel.DataAnnotations;

namespace ZooStav.Wolf.Web.Models;

/// <summary>Пользователь системы: работник зоопарка (кипер, ветеринар, руководство) или наблюдатель.</summary>
public class AppUser
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");

    [Required, MaxLength(64)]
    public string UserName { get; set; } = string.Empty;     
    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required, MaxLength(128)]
    public string DisplayName { get; set; } = string.Empty;   

    [Required, MaxLength(32)]
    public string Role { get; set; } = Roles.Observer;

    [MaxLength(128)]
    public string Position { get; set; } = string.Empty;      

    [MaxLength(128)]
    public string? Email { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? LastLoginAt { get; set; }

    public ICollection<DiaryEntry> DiaryEntries { get; set; } = new List<DiaryEntry>();
}

public class Animal
{ 
    public int Id { get; set; }

    [Required, MaxLength(128)]
    public string Name { get; set; } = string.Empty;           

    [Required, MaxLength(128)]
    public string Species { get; set; } = string.Empty;       
    [MaxLength(128)]
    public string LatinName { get; set; } = string.Empty;     

    [Required, MaxLength(64)]
    public string SubdomainSlug { get; set; } = string.Empty;

    [MaxLength(32)]
    public string Gender { get; set; } = string.Empty;       

    public DateTime? BirthDate { get; set; }

    [MaxLength(128)]
    public string Enclosure { get; set; } = string.Empty;   

    [MaxLength(64)]
    public string Status { get; set; } = "Живёт в зоопарке";

    [MaxLength(128)]
    public string ConservationStatus { get; set; } = string.Empty;

    public string ShortDescription { get; set; } = string.Empty;

    public string History { get; set; } = string.Empty;

    [MaxLength(256)]
    public string HeroImageUrl { get; set; } = "/images/wolf-hero.jpg";

    public decimal? WeightKg { get; set; }
    public decimal? HeightCm { get; set; }

    public int? PartnerAnimalId { get; set; }
    public Animal? Partner { get; set; }

    public bool IsMainPage { get; set; }                       

    public ICollection<MediaItem> Media { get; set; } = new List<MediaItem>();
    public ICollection<DiaryEntry> DiaryEntries { get; set; } = new List<DiaryEntry>();
}

public class MediaItem
{
    public int Id { get; set; }
    public int AnimalId { get; set; }
    public Animal? Animal { get; set; }

    [Required, MaxLength(16)]
    public string Kind { get; set; } = MediaKinds.Photo;

    [Required, MaxLength(256)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(512)]
    public string Url { get; set; } = string.Empty;

    [MaxLength(512)]
    public string? PosterUrl { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsPublished { get; set; } = true;
}

public class DiaryEntry
{
    public int Id { get; set; }

    public int AnimalId { get; set; }
    public Animal? Animal { get; set; }

    [Required]
    public DiaryEntryType Type { get; set; }

    public DateTime OccurredAt { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public string Details { get; set; } = string.Empty;

    [MaxLength(200)] public string? FoodType { get; set; }      
    public decimal? FoodAmountKg { get; set; }                  
    [MaxLength(200)] public string? Medication { get; set; }     
    [MaxLength(100)] public string? Dosage { get; set; }         
    [MaxLength(200)] public string? PartnerName { get; set; }     
    public int? OffspringCount { get; set; }                      
    public decimal? WeightKg { get; set; }
    public decimal? TemperatureC { get; set; }
    [MaxLength(200)] public string? Diagnosis { get; set; }      

    public bool IsPublic { get; set; } = true;

    public string AuthorId { get; set; } = string.Empty;
    public AppUser? Author { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<DiaryComment> Comments { get; set; } = new List<DiaryComment>();
}

public class DiaryComment
{
    public int Id { get; set; }

    public int DiaryEntryId { get; set; }
    public DiaryEntry? DiaryEntry { get; set; }

    [Required]
    public string Text { get; set; } = string.Empty;

    public string AuthorId { get; set; } = string.Empty;
    public AppUser? Author { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

public class AuditLog
{
    public long Id { get; set; }

    [MaxLength(64)] public string EntityName { get; set; } = string.Empty;
    [MaxLength(64)] public string EntityId { get; set; } = string.Empty;
    [MaxLength(32)] public string Action { get; set; } = string.Empty;   
    public string Summary { get; set; } = string.Empty;                

    public string UserId { get; set; } = string.Empty;
    [MaxLength(128)] public string UserName { get; set; } = string.Empty;
    [MaxLength(32)] public string UserRole { get; set; } = string.Empty;
    [MaxLength(64)] public string? IpAddress { get; set; }

    public DateTime TimestampUtc { get; set; } = DateTime.UtcNow;
}

public class Donation
{
    public int Id { get; set; }

    [MaxLength(128)] public string DonorName { get; set; } = "Анонимный даритель";
    [MaxLength(128)] public string? Email { get; set; }

    public decimal Amount { get; set; }

    [MaxLength(8)] public string Currency { get; set; } = "RUB";

    [MaxLength(64)] public string Purpose { get; set; } = DonationPurposes.Food;

    public bool IsRecurring { get; set; }        

    public string? Comment { get; set; }

    [MaxLength(32)] public string Provider { get; set; } = "demo";  

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsProcessed { get; set; }

    public int? AnimalId { get; set; }
    public Animal? Animal { get; set; }
}

public static class DonationPurposes
{
    public const string Food = "Корм";
    public const string Treatment = "Лечение";
    public const string Enclosure = "Вольер";
    public const string Free = "Свободное назначение";

    public static readonly string[] All = { Food, Treatment, Enclosure, Free };
}
