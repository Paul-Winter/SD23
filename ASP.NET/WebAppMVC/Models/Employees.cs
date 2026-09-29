namespace WebAppMVC.Models
{
    public class Employees
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public Departs Depart { get; set; }
        public Employees(int id, string name, DateOnly birthdate, Departs departs)
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
