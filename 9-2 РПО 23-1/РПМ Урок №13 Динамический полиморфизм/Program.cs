using System;


namespace РПМ_Урок__13_Динамический_полиморфизм
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Human[] people =
            {
                new Human(),
                new Employee("Иван", "Иванов", new DateTime(1999,05,03), 12000),
                new Manager("Фёдор", "Сумкин", new DateTime(2001,03,02), 35000, "банковское дело"),
                new Scientist("Мария", "Кюрия", new DateTime(2003,08,01), 21000, "теоретическая физика"),
                new Specialist("Александр", "Македонов", new DateTime(2007,12,12), 32000, "инженер")
            };

            foreach (Human human in people)
            {
                Console.WriteLine(human.Print());
            }
        }
    }

    public class Human
    {
        public string firstName;
        public string lastName;
        public DateTime birthDate;

        public Human()
        {
            this.firstName = "John";
            this.lastName  = "Doe";
            this.birthDate = new DateTime(2001,01,01);
        }

        public Human(string firstName, string lastName, DateTime birthDate)
        {
            this.firstName = firstName;
            this.lastName  = lastName;
            this.birthDate = birthDate;
        }

        public virtual string Print()
        {
            return "\nФамилия: " + this.lastName + "\nИмя: " + this.firstName + "\nДата рождения: " +
                birthDate.ToLongDateString();
        }
    }

    public class Employee : Human
    {
        public double salary;

        public Employee()
        {
            this.firstName = base.firstName;
            this.lastName  = base.lastName;
            this.birthDate = base.birthDate;
            this.salary    = 0.0;
        }

        public Employee(string firstName, string lastName, DateTime birthDate, double salary)
            : base(firstName, lastName, birthDate)
        {
            this.salary = salary;
        }

        public override string Print()
        {
            return base.Print() + "\nЗарплата: " + salary;
        }
    }

    public class Manager : Employee
    {
        public string fieldActivity;

        public Manager()
        {
            this.firstName     = base.firstName;
            this.lastName      = base.lastName;
            this.birthDate     = base.birthDate;
            this.salary        = base.salary;
            this.fieldActivity = "отсутствует";
        }

        public Manager(string firstName, string lastName, DateTime birthDate, double salary, string fieldActivity)
            : base(firstName, lastName, birthDate, salary)
        {
            this.fieldActivity = fieldActivity;
        }
        
        public override string Print()
        {
            return base.Print() + "\nСфера деятельности: " + fieldActivity;
        }
    }

    public class Scientist : Employee
    {
        public string scienceDirection;

        public Scientist()
        {
            this.firstName        = base.firstName;
            this.lastName         = base.lastName;
            this.birthDate        = base.birthDate;
            this.salary           = base.salary;
            this.scienceDirection = "отсутствует";
        }

        public Scientist(string firstName, string lastName, DateTime birthDate, double salary, string scienceDirection)
            : base(firstName, lastName, birthDate, salary)
        {
            this.scienceDirection = scienceDirection;
        }

        public override string Print()
        {
            return base.Print() + "\nНаучное направление: " + scienceDirection;
        }
    }

    public class Specialist : Employee
    {
        public string qualification;

        public Specialist()
        {
            this.firstName     = base.firstName;
            this.lastName      = base.lastName;
            this.birthDate     = base.birthDate;
            this.salary        = base.salary;
            this.qualification = "отсутствует";
        }

        public Specialist(string firstName, string lastName, DateTime birthDate, double salary, string qualification)
            : base(firstName, lastName, birthDate, salary)
        {
            this.qualification = qualification;
        }

        public override string Print()
        {
            return base.Print() + "\nКвалификация: " + qualification;
        }
    }
}
