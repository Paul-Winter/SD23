using System;

namespace Урок__23_24.Функции__подпрограммы__рекурсия
{
    internal class Program
    {
        static int RashodZaMesyac = 1000;
        int RashodZaGod = 12000;
        public static string valuestring = "не тест";
        static void Main(string[] args)
        {
            ///*Program program = new Program();

            // program.TC();
            // program.TC2();
            // Program program2 = new Program();
            // program2.TC2();*/
            //int a = 123;
            //int b = 3222;
            //int c = 21;
            //double d = 34.45;
            //double e = 12.333;

            //Console.WriteLine(Plus(a, c));
            //Console.WriteLine(Plus(d, e));
            //Console.WriteLine(Plus(d, e, 23.5, 655.3));
            //Cout(a);

            int value = 10;
            Console.WriteLine($"1. До вызова: {value}");
            ModifValue(value);
            Console.WriteLine($"1. После вызова: {value}");

            Console.WriteLine($"2. До вызова: {value}");
            ModifRef(ref value);
            Console.WriteLine($"2. После вызова (ref): {value}");


            int OutValue;
            Console.WriteLine($"3. Передача по ссылке");
            ModifOut(out OutValue);
            Console.WriteLine($"3. После вызова (out): {OutValue}");

            
            Console.WriteLine($"4. До вызова: {valuestring}");
            ModifValueString(valuestring);
            Console.WriteLine($"4. После вызова: {valuestring}");

            Console.WriteLine($"5. До вызова: {valuestring}");
            ModifRefString(ref valuestring);
            Console.WriteLine($"5. После вызова(ref): {valuestring}");


            // Общие сведения о подпрограммах.Определение и вызов подпрограмм.
            int result = Add(8, 4);
            Console.WriteLine($"Результат сложения: {result}");

            Greet("Пользователь");

        }

        // Область видимости и время жизни переменной.
        public void Magazin()
        {
            Console.WriteLine("Введите имя магазина: ");
            string nameMagazin = Console.ReadLine();
            Console.WriteLine("Введите какое количество сотрудников: ");
            int kolvoSotrudnikov = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите адрес магазина: ");
            string adresMagazina = Console.ReadLine();
            Console.WriteLine("Введите расход за месяц: ");
            RashodZaMesyac = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("");
            Console.WriteLine("==================================");
            Console.WriteLine($"Наименование магазина: {nameMagazin}\nКоличество сотрудников: {kolvoSotrudnikov}\nАдрес магазина: {adresMagazina}\nРасход за месяц: {RashodZaMesyac}");
            Console.WriteLine("==================================");
        }
        public void TC()
        {
            RashodZaGod = 3;
            RashodZaMesyac = 2;

            Console.WriteLine($"{RashodZaGod},   {RashodZaMesyac}");
        }
        public void TC2()
        {
            
            Console.WriteLine($"{RashodZaGod},   {RashodZaMesyac}");
        }

        // Организация функций.
        public static int Plus(int a, int b)
        {
            return a + b;
        }
        public static double Plus(double a, double b)
        {
            return a + b;
        }
        public static double Plus(double a, double b, double c, double d)
        {
            return a + b + c + d;
        }


        public static void Cout(int a)
        {
            Console.WriteLine($"a = {a}");
        }


        static void ModifValue(int parm)
        {
            parm = 20;
            Console.WriteLine($"parmin: {parm}");
        }
        static void ModifRef(ref int parm)
        {
            parm = 20;
            Console.WriteLine($"parmin(ref): {parm}");
        }
        static void ModifOut(out int parm)
        {
            parm = 20;
            Console.WriteLine($"parmin(out): {parm}");
        }
        static void ModifValueString(string parm)
        {
            valuestring = "тест";
            Console.WriteLine($"parmin: {parm}");
        }
        static void ModifRefString(ref string parm)
        {
            parm = "тест";
            Console.WriteLine($"parmin(ref): {parm}");
        }




        // Общие сведения о подпрограммах.Определение и вызов подпрограмм.
        public static int Add(int a, int b)
        {
            int sum = a + b;
            return sum;
        }

        public static void Greet(string name)
        {
            Console.WriteLine($"Здраствуйте, {name}!");

            Console.WriteLine("Введите число для вычисления факториала: ");
            int number = Convert.ToInt32(Console.ReadLine());
            //long result = Factorial(number);
            //Console.WriteLine($"Факториал {number} = {chiclo}");


            Console.WriteLine("Введите число для вычисления факториала: ");
            int chislo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Факториал {number} {chislo}");


        }

        // Рекурсия.

        //static long Factorial(int n)
        //{
        //    if (n <= 1) return 1;  
        //    return n * Factorial(n - 1);
        //}

        static long Function(int c)
        {
            int chislo = 1;

            for (int i = 1; i < c; i++)
            {
                chislo *=i;

            }
            return chislo;
        }




    }

    // Механизм передачи параметров.
    // Область видимости и время жизни переменной.

    // Рекурсия.
}
