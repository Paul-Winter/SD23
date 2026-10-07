namespace ZooRabbit.Web.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string Role { get; set; } = "Employee"; 
}

public class Donation
{
    public int Id { get; set; }
    public int AnimalId { get; set; }
    public int Amount { get; set; }
    public string Purpose { get; set; } = "Корм"; 
    public string DonorName { get; set; } = "Аноним";
    public DateTime Date { get; set; } = DateTime.UtcNow;
}
