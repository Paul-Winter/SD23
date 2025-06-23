using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLibrary;

namespace AppTestLib
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Human human = new Human();
            //Console.WriteLine("Введите имя: ");
            //string names = Console.ReadLine();
            //human.LibSetName(names);
            //human.LibGetName();
        }
    }
    public class Human
    {
        Class1 class1 = new Class1();
        
        public string LibGetName()
        {
            return class1.GetName();
        }
        public void LibSetName(string name)
        {
            class1.SetName(name);
        }


        public int LibGetAge()
        {
            return class1.GetAge();
        }
        public void LibSetAge(int age)
        {
            class1.SetAge(age);
        }



        public double LibGetSalary()
        {
            return class1.GetSalary();
        }
        public void LibSetSalary(double salary)
        {
            class1.SetSalary(salary);
        }

    }
}
