using System;
using System.Security.Cryptography;

namespace РПМ_Урок__17_Параметры
{
    internal class Program
    {
        // демонстрация работы ref
        static void SomeFunc(ref int x, ref int[] someArr)
        {
            Console.WriteLine("Внутри метода SomeFunc до изменения переменная x = " + x);
            Console.Write("Массив some Array: [ ");
            foreach (int value in someArr)
            {
                Console.Write(value + " ");
            }
            Console.Write("]");
            Console.WriteLine("\n\n\n");

            x = 100;
            someArr = new int[] { 5, 4, 3, 2, 1 };

            Console.WriteLine("Внутри метода SomeFunc после изменения переменная x = " + x);
            Console.Write("Массив some Array: [ ");
            foreach (int value in someArr)
            {
                Console.Write(value + " ");
            }
            Console.Write("]");
            Console.WriteLine("\n\n\n");
        }

        // демонстрация работы out
        static void GetRandom(out int digit)
        {
            digit = new Random().Next(10);
        }

        // демонстрация работы params
        static int SumNums(params int[] arr)
        {
            int result = 0;
            foreach (int value in arr)
            {
                result += value;
            }
            return result;
        }

        static void Main(string[] args)
        {

            //  1) абстрактный класс Human
            //  2) абстрактный класс Employee
            //  3) интерфейс IWorker
            //  4) интерфейс IManager
            //  5) класс Director : Employee, IManager
            //  6) класс Seller : Employee, IWorker
            //  7) класс Cashier : Employee, IWorker
            //  8) создать в main экземпляры директора, продавца и кассира
            //  9) продемонстрировать разницу в реализации интерфейсов

            //Car bolid1 = new Car("Рубенс Барикелло", 100);
            //Car bolid2 = new Car(99, "Микка Хакканен");
            //for (int i = 0; i <= 10; i++)
            //{
            //    bolid1.SpeedUp(5);
            //    Console.WriteLine(bolid1 + "\t" + Car.brand);
            //    bolid2.SpeedUp(5);
            //    Console.WriteLine(bolid2 + "\t" + Car.brand);
            //}

            Console.WriteLine("---------------------------ref---------------------------");
            int x = 10;
            int[] someArr = new int[] { 3, 3, 4, 4, 5 };

            Console.WriteLine("Внутри метода main до вызова SomeFunc переменная x = " + x);
            Console.Write("Массив some Array: [ ");
            foreach (int value in someArr)
            {
                Console.Write(value + " ");
            }
            Console.Write("]");
            Console.WriteLine("\n\n\n");

            SomeFunc(ref x, ref someArr);

            Console.WriteLine("Внутри метода main после вызова SomeFunc переменная x = " + x);
            Console.Write("Массив some Array: [ ");
            foreach (int value in someArr)
            {
                Console.Write(value + " ");
            }
            Console.Write("]");
            Console.WriteLine("\n\n\n");

            Console.WriteLine("---------------------------out---------------------------");
            Console.WriteLine("\n\n\n");

            int y;
            //Console.WriteLine("до вызова GetRandom: y = " + y);
            GetRandom(out y);
            Console.WriteLine("после вызова GetRandom: y = " + y);
            Console.WriteLine("\n\n\n");

            Console.WriteLine("--------------------------params-------------------------");
            Console.WriteLine("\n");

            Console.WriteLine("Сумма = " + SumNums(1, 2, 3, 4, 5, 6, 7, 8, 9, 10));
        }
    }

    abstract class Human { }

    abstract class Employee : Human { }

    interface IWorker
    {
        void Work();
    }

    interface IManager
    {
        void Manage();
    }

    class Director : Employee, IManager
    {
        public void Manage()
        {
            Console.WriteLine("Директор руководит компанией");
        }
    }

    class Worker : Employee, IWorker
    {
        public void Work()
        {
            Console.WriteLine("Работник трудится");
        }
    }

    class Brigadir : Employee, IWorker, IManager
    {
        public void Work()
        {
            Console.WriteLine("Бригадир работает в бригаде");
        }

        public void Manage()
        {
            Console.WriteLine("Бригадир управляет рабочими");
        }
    }

    public class Car
    {
        public static string brand;
        private string driverName;
        private int currentSpeed;

        // конструктор по умолчанию
        public Car()
        {
            this.driverName   = "";
            this.currentSpeed = 0;
        }

        // статический конструктор
        static Car()
        {
            brand = "Mercedes-Benz";
        }

        // параметризованные конструкторы
        public Car(string driverName)
        {
            this.driverName   = driverName;
            this.currentSpeed = 10;
        }

        public Car(int currentSpeed)
        {
            this.driverName   = "unknown";
            this.currentSpeed = currentSpeed;
        }

        public Car(string driverName, int currentSpeed)
        {
            this.driverName   = driverName;
            this.currentSpeed = currentSpeed;
        }

        public Car(int currentSpeed, string driverName)
        {
            this.driverName   = driverName;
            this.currentSpeed = currentSpeed;
        }

        public void SpeedUp(int delta)
        {
            this.currentSpeed += delta;
        }

        public override string ToString()
        {
            return driverName + " едет со скоростью " + currentSpeed + "км/ч";
        }
    }
}
