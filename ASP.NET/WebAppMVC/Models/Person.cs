namespace WebAppMVC.Models
{
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public Person(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
        public Person() : this("John", "Doe") {}
        public override string ToString()
        {
            return $"{Name} {Surname}\n";
        }
    }
}
