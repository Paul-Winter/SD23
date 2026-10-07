namespace Coati_Space_Project.Models
{
    public class Coati : Animal
    {
        public int Id { get; set; }
        public string Species { get; set; } = "Южноамериканская носуха (Nasua nasua)";
        public string Gender { get; set; } = "Самка";
        public DateOnly BirthDate { get; set; } = new DateOnly(2021, 5, 14);
        public string Biography { get; set; } = string.Empty;
        public string Diet { get; set; } = string.Empty;
        public string Habitat { get; set; } = string.Empty;
        public string HealthStatus { get; set; } = "Клинически здоров";
        public string PhotoUrl { get; set; } = "/images/coati-main.jpg";
        public string VideoUrl { get; set; } = "/videos/coati-stream.mp4";

        public Coati()
        {
            Name = "Хахатуха";
        }

        public Coati(int id, string name, string species, string gender, DateOnly birthDate,
                     string biography, string diet, string habitat, string healthStatus,
                     string photoUrl, string videoUrl)
        {
            Id = id;
            Name = name;
            Species = species;
            Gender = gender;
            BirthDate = birthDate;
            Biography = biography;
            Diet = diet;
            Habitat = habitat;
            HealthStatus = healthStatus;
            PhotoUrl = photoUrl;
            VideoUrl = videoUrl;
        }

        public override string ToString()
        {
            return $"Id: {Id}; Name: {Name}; Species: {Species}; BirthDate: {BirthDate}; Status: {HealthStatus}";
        }
    }
}
