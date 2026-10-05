namespace ZooRabbit.Web.Models;

public class Animal
{
    public int Id { get; set; }
    public string Slug { get; set; } = "";        
    public string Name { get; set; } = "";       
    public string Species { get; set; } = "";
    public string Description { get; set; } = ""; 
    public string? PhotoUrl { get; set; }
    public string? VideoUrl { get; set; }
    public string? WebcamUrl { get; set; }
    public List<DiaryEntry> Diary { get; set; } = new();
}
