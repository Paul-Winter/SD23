namespace Coati_Space_Project.Models
{
    public class Donation
    {
        public int Id { get; set; }
        public string DonorName { get; set; } = "Анонимный друг";
        public string? Email { get; set; }
        public decimal Amount { get; set; } = 300;
        public string Target { get; set; } = "На лакомства и фрукты";
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Donation() { }

        public Donation(int id, string donorName, string? email, decimal amount,
                        string target, string? message, DateTime createdAt)
        {
            Id = id;
            DonorName = donorName;
            Email = email;
            Amount = amount;
            Target = target;
            Message = message;
            CreatedAt = createdAt;
        }

        public override string ToString()
        {
            return $"Donation #{Id} by {DonorName}: {Amount} руб. ({Target})";
        }
    }
}
