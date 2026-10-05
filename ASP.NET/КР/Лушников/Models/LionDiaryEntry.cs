namespace StavZoo.Models;

public class LionDiaryEntry
{
    public int Id { get; set; }
    public int LionId { get; set; }
    public Lion? Lion { get; set; }
    public string EntryType { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
    public string RecordedBy { get; set; } = string.Empty;
}