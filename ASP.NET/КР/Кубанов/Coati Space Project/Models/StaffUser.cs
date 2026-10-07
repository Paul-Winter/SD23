namespace Coati_Space_Project.Models
{
    public class StaffUser
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = "Employee";
        public string Position { get; set; } = "Кипер";

        public StaffUser() { }

        public StaffUser(int id, string username, string password, string fullName, string role, string position)
        {
            Id = id;
            Username = username;
            Password = password;
            FullName = fullName;
            Role = role;
            Position = position;
        }

        public override string ToString()
        {
            return $"Staff #{Id}: {FullName} ({Username}) - {Role}/{Position}";
        }
    }
}
