namespace StavZoo.Models;

public class Donation
{
    public int Id { get; set; }
    public int LionId { get; set; }
    public Lion? Lion { get; set; }
    public int Amount { get; set; }
    public DateTime DonatedAt { get; set; } = DateTime.UtcNow;
}