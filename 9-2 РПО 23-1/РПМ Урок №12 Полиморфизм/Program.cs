using System;
using System.Security.Claims;

namespace РПМ_Урок__12_Полиморфизм
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a = 13;
            int b = 42;
            int c = 24;
            double x = 67.7;
            double y = 98.8;

            Console.WriteLine("a + b = " + Mathematic.Sum(a, b));
            Console.WriteLine("a + b + c = " + Mathematic.Sum(a, b, c));
            Console.WriteLine("a + x = " + Mathematic.Sum(a, x));
            Console.WriteLine("y + b = " + Mathematic.Sum(y, b));
            Console.WriteLine("x + y = " + Mathematic.Sum(x, y));

            Console.WriteLine("\n\n\n_________________________________________________________\n\n\n");

            int radius = 12;
            int sideA = 15;
            int sideB = 27;

            Rectangle rectangle = new Rectangle(sideA, sideB);
            Circle circle = new Circle(radius);

            rectangle.Draw();
            Console.WriteLine("Периметр: " + rectangle.Perimeter(sideA, sideB));
            Console.WriteLine("Площадь: " + rectangle.Area(a, b));

            circle.Draw();
            Console.WriteLine("Периметр: " + circle.Perimeter(radius));
            Console.WriteLine("Площадь: " + circle.Area(radius));
        }
    }

    class Mathematic
    {
        // перегрузка по количеству параметров
        public static int Sum(int a, int b)
        {
            return a + b;
        }

        public static int Sum(int a, int b, int c)
        {
            return a + b + c;
        }

        // перегрузка по типу параметров
        public static double Sum(double a, double b)
        {
            return a + b;
        }

        // перегрузка по очерёдности параметров
        public static double Sum(int a, double b)
        {
            return a + b;
        }

        public static double Sum(double a, int b)
        {
            return a + b;
        }
    }

    class Circle
    {
        int radius;

        public Circle(int radius)
        {
            this.radius = radius;
        }

        public double Perimeter(int radius)
        {
            return 2 * Math.PI * radius;
        }

        public double Area(int radius)
        {
            return Math.PI * radius * radius;
        }

        public void Draw()
        {
            Console.WriteLine("Круг");
        }
    }

    class Rectangle
    {
        int a;
        int b;

        public Rectangle(int a, int b)
        {
            this.a = a;
            this.b = b;
        }

        public double Perimeter(int a, int b)
        {
            return 2 * (a + b);
        }

        public double Area(int a, int b)
        {
            return a * b;
        }

        public void Draw()
        {
            Console.WriteLine("Прямоугольник");
        }
    }
}
