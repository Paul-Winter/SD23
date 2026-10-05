using System.ComponentModel.DataAnnotations;

namespace Свинья_Визенау.Models
{
    public class Animal
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Slug { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(2000)]
        public string Description { get; set; } = string.Empty;

        public string? PhotoUrl { get; set; }

        public DateTime ArrivedAt { get; set; } = DateTime.UtcNow;
    }
}
