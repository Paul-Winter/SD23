namespace Свинья_Визенау.Models
{
    public class Donation
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Purpose { get; set; } = string.Empty;  
        public string? DonorName { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UserId { get; set; }
    }
}
