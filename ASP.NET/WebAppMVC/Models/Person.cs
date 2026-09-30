using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections;

namespace WebAppMVC.Models
{
    public class Person : Human
    {
        [BindRequired]
        public string Surname { get; set; }
        [BindingBehavior(BindingBehavior.Optional)]
        public string Email { get; set; }
        //[BindNever]
        public DateOnly BirthDate { get; set; }

        public Person(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
        public Person() : this("John", "Doe") { }
        public override string ToString()
        {
            return $"{Name} {Surname}\n{BirthDate}\n{Email}\n";
        }
    }
}
