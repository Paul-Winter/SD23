namespace Coati_Space_Project.Models
{
    public class DiaryEntry
    {
        public int Id { get; set; }
        public int CoatiId { get; set; }
        public DateTime EventDate { get; set; } = DateTime.Now;
        public string EventType { get; set; } = "Кормление";
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = "Кипер зоопарка";

        public DiaryEntry() { }

        public DiaryEntry(int id, int coatiId, DateTime eventDate, string eventType,
                          string title, string description, string author)
        {
            Id = id;
            CoatiId = coatiId;
            EventDate = eventDate;
            EventType = eventType;
            Title = title;
            Description = description;
            Author = author;
        }

        public override string ToString()
        {
            return $"[{EventDate:yyyy-MM-dd HH:mm}] ({EventType}) {Title} - {Author}";
        }
    }
}
