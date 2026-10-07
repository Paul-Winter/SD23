using System.ComponentModel.DataAnnotations;

namespace Свинья_Визенау.Models
{
    public class Donation
    {
        public int Id { get; set; }

        [Range(1, 1_000_000)]
        public decimal Amount { get; set; }

        [StringLength(200)]
        public string Purpose { get; set; } = string.Empty;

        [StringLength(100)]
        public string? DonorName { get; set; }

        [StringLength(500)]
        public string? Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? UserId { get; set; }
    }
}
