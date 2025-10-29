using Human;
using System;

namespace Workers
{
    public class Worker : Employee
    {
        string qualification;
        public Worker(string firstName, string lastName, int age, double salary, string qualification)
            : base(firstName, lastName, age, salary)
        {
            this.qualification = qualification;
        }
        public Worker() : this("John", "Doe", 0, 0.0, "intern") {}
        public override string ToString()
        {
            return base.ToString() + $"\tQualification: {qualification}";
        }
    }
}
