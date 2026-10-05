namespace ZooRabbit.Web.Models;

public enum DiaryEntryType { Feeding, Mating, Offspring, Health, Other }

public class DiaryEntry
{
    public int Id { get; set; }
    public int AnimalId { get; set; }
    public Animal Animal { get; set; } = null!;
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public DiaryEntryType Type { get; set; }
    public string Note { get; set; } = "";
    public string Author { get; set; } = "";
}
