namespace StavZoo.Models;

public class Lion
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
    public string? VideoUrl { get; set; }
    public string? WebcamUrl { get; set; }
    public DateTime BirthDate { get; set; }
    public string? MotherName { get; set; }

    public List<LionDiaryEntry> DiaryEntries { get; set; } = new();
}