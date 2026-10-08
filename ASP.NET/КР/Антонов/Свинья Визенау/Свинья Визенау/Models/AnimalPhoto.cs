using System.ComponentModel.DataAnnotations;

namespace Свинья_Визенау.Models
{
    public class AnimalPhoto
    {
        public int Id { get; set; }

        public int AnimalId { get; set; }
        public Animal Animal { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string Url { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Caption { get; set; }   

        public int SortOrder { get; set; }


    }


}
