using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {

        }
    }
    public class Human
    {
        private string name;
        private int age;
        private double salary;
        public static double MROT = 22440;

        public string GetName()
        {
            return name;
        }
        public void SetName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                this.name = name;
            }
            else
            {
                this.name = "Ivan Bezimanni";
            }
        }
        public int GetAge()
        {
            return age;
        }
        public void SetAge(int age)
        {
            if (age >= 0)
            {
                this.age = age;
            }
            else
            {
                this.age = 0;
            }
        }
        public double GetSalary()
        {
            return salary;
        }
        public void SetSalary(double salary)
        {
            
            if (salary > MROT)
            {
                this.salary = salary;
            }
            else
            {
                this.salary = MROT;
            }
        }
    }
}
