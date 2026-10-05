namespace Свинья_Визенау.Models
{
    public class DiaryEntry
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; } = null!;
        public DiaryEntryType Type { get; set; }  
        public string Notes { get; set; } = string.Empty;
        public DateTime RecordedAt { get; set; }
        public string RecordedByUserId { get; set; } = string.Empty;
        public ApplicationUser RecordedBy { get; set; } = null!;
    }

    public enum DiaryEntryType
    {
        Feeding, Mating, Offspring, Observation, Medical
    }
}
