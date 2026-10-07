using System.ComponentModel.DataAnnotations;

namespace Свинья_Визенау.Models
{
    public class DiaryEntry
    {
        public int Id { get; set; }

        public int AnimalId { get; set; }
        public Animal Animal { get; set; } = null!;

        public DiaryEntryType Type { get; set; }

        [StringLength(2000)]
        public string Notes { get; set; } = string.Empty;

        public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

        public string? RecordedByUserId { get; set; }
        public ApplicationUser? RecordedBy { get; set; }
    }

    public enum DiaryEntryType
    {
        [Display(Name = "Кормление")]
        Feeding,

        [Display(Name = "Спаривание")]
        Mating,

        [Display(Name = "Потомство")]
        Offspring,

        [Display(Name = "Наблюдение")]
        Observation,

        [Display(Name = "Медицинский осмотр")]
        Medical
    }
}
