using System;
using System.CodeDom;

namespace РПМ_Урок__14_Абстракция
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Human[] people =
            {
                new Student("John", "Doe", new DateTime(2007,07,07), "IT Top College", "Graphic Design"),
                new SchoolChild("Jack", "Daniels", new DateTime(2012,12,12), "IT Top School", "8a")
            };

            foreach (Learner learner in people)
            {
                Console.WriteLine(learner.Print());
                learner.Think();
                learner.Study();
            }
        }
    }

    public abstract class Human
    {
        public string firstName;
        public string lastName;
        public DateTime birthDate;

        public Human()
        {
            this.firstName = "John";
            this.lastName  = "Doe";
            this.birthDate = new DateTime(2001, 01, 01);
        }

        public Human(string firstName, string lastName, DateTime birthDate)
        {
            this.firstName = firstName;
            this.lastName  = lastName;
            this.birthDate = birthDate;
        }

        public virtual string Print()
        {
            return "\nФамилия: " + this.lastName + "\nИмя: " + this.firstName + "\nДата рождения:" +
                birthDate.ToLongDateString();
        }

        public abstract void Think();
    }

    public abstract class Learner : Human
    {
        public string institution;

        public Learner()
        {
            this.firstName   = base.firstName;
            this.lastName    = base.lastName;
            this.birthDate   = base.birthDate;
            this.institution = "отсутствует";
        }

        public Learner(string firstName, string lastName, DateTime birthDate, string institution)
            : base(firstName, lastName, birthDate)
        {
            this.institution = institution;
        }

        public override string Print()
        {
            return base.Print() + "\nОбразовательное учреждение: " + institution;
        }

        public abstract void Study();
    }

    public class Student : Learner
    {
        public string groupName;

        public Student(string firstName, string lastName, DateTime birthDate, string institution, string groupName)
            : base(firstName, lastName, birthDate, institution)
        {
            this.groupName = groupName;
        }

        public override string Print()
        {
            return base.Print() + "\nГруппа: " + groupName;
        }

        public override void Study()
        {
            Console.WriteLine("\nЯ изучаю предметы в колледже\n");
        }

        public override void Think()
        {
            Console.WriteLine("\nЯ думаю как студент\n");
        }
    }

    public class SchoolChild : Learner
    {
        public string className;

        public SchoolChild(string firstName, string lastName, DateTime birthDate, string institution, string className)
            : base (firstName, lastName, birthDate, institution)
        {
            this.className = className;
        }

        public override string Print()
        {
            return base.Print() + "\nКласс: " + className;
        }

        public override void Study()
        {
            Console.WriteLine("\nЯ изучаю предметы в школе\n");
        }

        public override void Think()
        {
            Console.WriteLine("\nЯ думаю как школьник\n");
        }
    }
}
