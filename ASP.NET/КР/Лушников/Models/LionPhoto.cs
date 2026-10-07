namespace StavZoo.Models;

public class LionPhoto
{
    public int Id { get; set; }
    public int LionId { get; set; }
    public Lion? Lion { get; set; }
    public string PhotoUrl { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}