using System;

namespace РПМ_Урок__16_Структуры
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dimensions dimensions = new Dimensions(4.821, 7.396);
            Console.WriteLine(dimensions + "\n\n\n");

            Dimensions dim1 = new Dimensions();
            Coordinates cord1 = new Coordinates();            
            Console.WriteLine(dim1);
            Console.WriteLine(cord1);
            Console.WriteLine();

            Dimensions dim2 = dim1;
            Coordinates cord2 = cord1;
            Console.WriteLine("dim2 = dim1\ncord2 = cord1\n");

            Console.WriteLine("dim1 += 2\ncord1 += 3");
            dim1.length += 2;
            dim1.width += 2;
            cord1.latitude += 3;
            cord1.longitude += 3;
            Console.WriteLine("\ndim1\n" + dim1);
            Console.WriteLine("\ncord1\n" + cord1);
            Console.WriteLine("\ndim2\n" + dim2);
            Console.WriteLine("\ncord2\n" + cord2);

            // создать методы принимающие экземпляры структуры и класса
            // и производящие с ними какие-то изменения
            // передать в них значения и убедиться в разнице работы
        }
    }

    public struct Dimensions
    {
        public double length;
        public double width;

        public Dimensions(double length, double width)
        {
            this.length = length;
            this.width  = width;
        }

        public override string ToString()
        {
            return "Длина: " + length + "\nШирина: " + width;
        }

        public static Dimensions operator + (Dimensions a, Dimensions b)
        { 
            return new Dimensions(a.length + b.length, a.width + b.width);
        } 
    }

    public class Coordinates
    {
        public double latitude;
        public double longitude;

        public Coordinates()
        {
            this.latitude  = 0.0d;
            this.longitude = 0.0d;
        }

        public Coordinates(double latitude, double longitude)
        {
            this.latitude  = latitude;
            this.longitude = longitude;
        }

        public override string ToString()
        {
            return "Широта: " + latitude + "\nДолгота: " + longitude;
        }

        public static Coordinates operator + (Coordinates a, Coordinates b)
        {
            return new Coordinates(a.latitude + b.latitude, a.longitude + b.longitude);
        }
    }
}
