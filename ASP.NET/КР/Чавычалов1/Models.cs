namespace StavZoo;

public enum DiaryType { Feeding, Mating, Offspring, Health, Other }
public enum DonationPurpose { Food, Treatment }

public static class EnumRu
{
    public static string Ru(this DiaryType t) => t switch
    {
        DiaryType.Feeding => "Кормёжка",
        DiaryType.Mating => "Спаривание",
        DiaryType.Offspring => "Потомство",
        DiaryType.Health => "Здоровье",
        _ => "Прочее"
    };
    public static string Ru(this DonationPurpose p) => p == DonationPurpose.Food ? "Корм" : "Лечение";
}

public class Animal
{
    public int Id { get; set; }
    public string Slug { get; set; } = "";
    public string Name { get; set; } = "";
    public string Species { get; set; } = "";
    public string Biography { get; set; } = "";
    public string PhotoUrl { get; set; } = "";
    public string VideoUrl { get; set; } = "";
    public string WebcamUrl { get; set; } = "";
}

public class DiaryEntry
{
    public int Id { get; set; }
    public int AnimalId { get; set; }
    public DiaryType Type { get; set; }
    public string? SubjectName { get; set; }
    public string Text { get; set; } = "";
    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
    public string Author { get; set; } = "";
}

public class Donation
{
    public int Id { get; set; }
    public int AnimalId { get; set; }
    public string DonorName { get; set; } = "Аноним";
    public decimal Amount { get; set; }
    public DonationPurpose Purpose { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string Role { get; set; } = "Keeper";
}

public record LoginDto(string UserName, string Password);
public record DiaryDto(DiaryType Type, string Text, string? SubjectName, DateTime? OccurredAt);
public record DonationDto(decimal Amount, string? DonorName, DonationPurpose Purpose);
public record AnimalUpdateDto(string Biography, string? PhotoUrl, string? VideoUrl, string? WebcamUrl);
