using System.ComponentModel.DataAnnotations;

namespace Goat_Zoo.Models
{
    public class Note
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; } = "Прочее";
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
