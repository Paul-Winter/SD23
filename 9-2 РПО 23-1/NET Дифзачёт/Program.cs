using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static NET_Дифзачёт.Student;

namespace NET_Дифзачёт
{
    internal class Program
    {


        static void Main(string[] args)
        {
            Student student1 = new Student();
            Console.WriteLine("Введите первое число: ");
            int number1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите второе число: ");
            int number2 = int.Parse(Console.ReadLine());

            //Sum sum = new Sum(12, 2);
            //Sum sim = new Sum(22,0);
            //Console.WriteLine(sum / sim);
            

            //Bird bird = new Bird("");

            Student student = new Student();
            student.name = "Альберт";
            student.age = 17;
            Console.WriteLine(student);

            Human human = new Human();
            human.name = "алиса ";
            human.age = 18;
            Console.WriteLine(human);
            



            Student.Dissertation dissertation = new Student.Dissertation();
            dissertation.LastName = "Rabota";
            dissertation.srok = 3;
            Console.WriteLine(dissertation);
        }
    }
    class Sum
    {
        
        public int a;
        public int b;
        public Sum(int a, int b)
        {
            this.a = a;
            this.b = b;
        }
        public static Sum operator + (Sum a, Sum b)
        {
            return new Sum(a.a + b.a, a.b + b.b);
        }

        public static Sum operator - (Sum a, Sum b)
        {
            return new Sum(a.a - b.a, a.b - b.b);
        }

        public static Sum operator /(Sum a, Sum b)
        {
            try 
            {
                Console.WriteLine(a.a / a.b);
            }
            catch(DivideByZeroException) 
            {
                Console.WriteLine("Деление на ноль!");
            }
            return new Sum(a.a / b.a, a.b / b.b);
        }

        public static Sum operator %(Sum a, Sum b)
        {
            return new Sum(a.a % b.a, a.b % b.b);
        }

        public static Sum operator *(Sum a, Sum b)
        {
            return new Sum(a.a * b.a, a.b * b.b);
        }

        public void hello()
        {
            Console.WriteLine($"Привет, вам даны числа {a} и {b}, радуйтесь!!!");
        }

        public override string ToString()
        {
            return $"{a},{b}";
        }
    }
    
    class Bird : Sum
    {
        string name;
        int age;

        public Bird(string name, int age, int a, int b) : base (a, b)
        {
            this.name = name;
            this.age = age;
        }

        public override string ToString()
        {
            return $"{name}, {age}, {a}, {b}";
        }
    }

    class Student:Human
    {
        
        


        public string name;
        public int age;
        public void number(int number1, int number2) 
        {
            if (number1 > number2)
            {
                Console.WriteLine($"Наибольшее число: {number1}");
            }
            else
            {
                Console.WriteLine($"Наибольшее число: {number2}");
            }

        }

        public override string ToString()
        {
            return $"{name}, {age}";
        }

        public class Dissertation
        {
            public string LastName;
            public int srok;

            public override string ToString()
            {
                return $"{LastName}, {srok}";
            }


        }
       
        
    }

    public class Human
    {
        public string name;
        public int age;

        public override string ToString()
        {
            return $"{name}, {age}";
        }


    }

}
