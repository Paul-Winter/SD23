using System.ComponentModel.DataAnnotations;
using ZooStav.Wolf.Web.Models;

namespace ZooStav.Wolf.Web.Infrastructure;

// ---------- Модели представлений (ViewModels) ----------

public class WolfPageViewModel
{
    public Animal Animal { get; set; } = new();
    public List<MediaItem> Photos { get; set; } = new();
    public List<MediaItem> Videos { get; set; } = new();
    public MediaItem? Webcam { get; set; }
    public List<Animal> PackMembers { get; set; } = new();
    public List<DiaryEntry> RecentPublicEntries { get; set; } = new();
    public FeedingStatus Feeding { get; set; } = new();
    public DonationSummary Donations { get; set; } = new();
    public string? RequestedSubdomain { get; set; }
    public string MainSiteUrl { get; set; } = "https://zoostav.ru";
    public List<string> VisitorComments { get; set; } = new();
}

public class DiaryPageViewModel
{
    public Animal Animal { get; set; } = new();
    public PagedResult<DiaryEntry> Page { get; set; } = new();
    public DiaryFilter Filter { get; set; } = new();
    public List<AppUser> Authors { get; set; } = new();
    public List<Animal> Animals { get; set; } = new();
    public bool CanSeeNonPublic { get; set; }
}

public class LoginViewModel
{
    [Required(ErrorMessage = "Укажите логин")]
    [Display(Name = "Логин")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите пароль")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; } = string.Empty;

    public string? ReturnUrl { get; set; }
    public string? Error { get; set; }
}

public class DonationViewModel
{
    [Display(Name = "Ваше имя")]
    [MaxLength(128)]
    public string? DonorName { get; set; }

    [EmailAddress(ErrorMessage = "Некорректный e-mail")]
    [Display(Name = "E-mail для благодарности")]
    public string? Email { get; set; }

    [Range(0, 10_000_000, ErrorMessage = "Сумма должна быть неотрицательной")]
    [Display(Name = "Сумма, ₽ (0 — free donation)")]
    public decimal Amount { get; set; } = 500;

    [Display(Name = "Назначение")]
    public string Purpose { get; set; } = DonationPurposes.Food;

    [Display(Name = "Ежемесячно (регулярный взнос)")]
    public bool IsRecurring { get; set; }

    [Display(Name = "Сообщение вольеру")]
    public string? Comment { get; set; }
}

public class DonationResultViewModel
{
    public Donation Donation { get; set; } = new();
    public DonationSummary Summary { get; set; } = new();
    public int AnimalId { get; set; }
    public string AnimalName { get; set; } = string.Empty;
}

public class AuditPageViewModel
{
    public PagedResult<AuditLog> Page { get; set; } = new();
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string? UserId { get; set; }
    public string? Action { get; set; }
    public string? Entity { get; set; }
    public List<AppUser> Users { get; set; } = new();
    public List<string> Actions { get; set; } = new() { "Created", "Updated", "Deleted", "Login", "CommentAdded", "CommentDeleted", "DonationCreated" };
}

public class DonationsPageViewModel
{
    public PagedResult<Donation> Page { get; set; } = new();
    public DonationSummary Summary { get; set; } = new();
    public string? Purpose { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public bool OnlyUnprocessed { get; set; }
}
