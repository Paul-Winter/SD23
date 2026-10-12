namespace Donation.Models
{
    public class Animals
    {
        public string AnimalType { get; set; }
        public Animals(string animalType)
        {
            AnimalType = animalType;
        }
        public override string ToString()
        {
            return AnimalType;
        }
    }
}
