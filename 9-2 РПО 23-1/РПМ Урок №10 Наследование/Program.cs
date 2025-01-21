using System;

namespace РПМ_Урок__10_Наследование
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Human human = new Human("Ivan", "Petrov");
            //Console.WriteLine(human.Show());

            Employee[] employees = new Employee[]
            {
                new Employee(),
                new Manager("Ivan", "Ivanov", new DateTime(1998,04,25), 50000, "company"),
                new Scientist("Petr", "Lobachevskiy", new DateTime(2000, 02, 02), 10000, "physics"),
                new Specialist("Alex", "Maslov", new DateTime(1999, 03, 31), 30000, "engineer")
            };

            foreach (Employee empl in employees)
            {
                Console.WriteLine(empl.Show() + "\n");
            }
        }
    }

    public class Human
    {
        public string firstName;
        public string lastName;
        public DateTime birthDate;

        public Human(string firstName, string lastName, DateTime birthDate)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthDate = birthDate;
        }

        public Human(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public Human()
        {
            firstName = string.Empty;
            lastName = "anonymous";
        }

        public string Show()
        {
            return "First name: " + firstName + "\nLast name: " + lastName;
        }
    }

    public class Employee : Human
    {
        double salary;

        public Employee(string firstName, string lastName, DateTime birthDate, double salary) : base(firstName, lastName, birthDate)   
        {
            this.salary = salary;
        }

        public Employee(string firstName, string lastName) : base(firstName, lastName)
        {
            salary = 0.0;
        }

        public Employee() : base("John", "Doe")
        {
            salary = 0.0;
        }

        public new virtual string Show()
        {
            return base.Show() + "\nSalary: " + salary;
        }
    }

    public class Manager : Employee
    {
        public string activity;

        public Manager(string firstName, string lastName, DateTime birthDate, double salary, string activity) :
            base(firstName, lastName, birthDate, salary)
        {
            this.activity = activity;
        }

        public override string Show()
        {
            return base.Show() + "\nActivity: " + activity;
        }
    }

    // создать два класса наследника от класса Scientist на выбор (например: физик и химик)
    // создать у каждого из них по 1 полю, 1 конструктору и 1 методу (не меньше)
    // переопределить методы в классах наследниках
    // в методе main создать по 1 экземпляру этих классов
    // создать 2 массива типов: Employee, Scientist и поместить в них эти экземпляры
    // вывести содержимое в консоль

    public class Scientist : Employee
    {
        public string scienceDirection;

        public Scientist(string firstName, string lastName, DateTime birthDate, double salary, string scienceDirection) :
            base(firstName, lastName, birthDate, salary)
        {
            this.scienceDirection = scienceDirection;
        }

        public override string Show()
        {
            return base.Show() + "\nScience direction: " + scienceDirection;
        }
    }

    public class Specialist : Employee
    {
        public string qualification;

        public Specialist(string firstName, string lastName, DateTime birthDate, double salary, string qualification) :
            base(firstName, lastName, birthDate, salary)
        {
            this.qualification = qualification;
        }

        public override string Show()
        {
            return base.Show() + "\nQualification: " + qualification;
        }
    }
}
