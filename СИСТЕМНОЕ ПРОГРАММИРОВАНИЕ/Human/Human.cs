using System;

namespace Human
{
    public abstract class Human
    {
        public string firstName;
        public string lastName;
        public int age;

        protected Human(string firstName, string lastName, int age)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
        }
        protected Human() : this(String.Empty, String.Empty, 0) {}

        public override string ToString()
        {
            return $"{firstName} {lastName} : {age}";
        }
    }

    public class Employee : Human
    {
        double salary;

        public Employee(string firstName, string lastName, int age, double salary) : base(firstName, lastName, age)
        {
            this.salary = salary;
        }
        public Employee() : this("John", "Doe", 0, 0.0) {}

        public override string ToString()
        {
            return base.ToString() + $"\nSalary: {salary}";
        }

        public void ShowEmployee()
        {
            Console.WriteLine($"{firstName} {lastName}: {age}\tSalary: {salary}");
        }
    }
}
