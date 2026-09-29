namespace WebAppMVC.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public Depart Depart { get; set; }
        public Employee(int id, string name, DateOnly birthdate, Depart departs)
        {
            Id = id;
            Name = name;
            BirthDate = birthdate;
            Depart = departs;
        }
        public override string ToString()
        {
            return $"Id: {Id};\nName: {Name};\nBirthDate: {BirthDate};\n" +
                   $"Depart: {Depart};\n";
        }

    }
}
