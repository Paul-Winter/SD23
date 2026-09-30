using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebAppMVC.Models
{
    public class Person
    {
        [BindRequired]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        [BindNever]
        public DateOnly BirthDate { get; set; }

        //public Person(string name, string surname)
        //{
        //    Name = name;
        //    Surname = surname;
        //}
        //public Person() : this("John", "Doe") {}
        public override string ToString()
        {
            return $"{Name} {Surname}\n{BirthDate}\n{Email}\n";
        }
    }
}
