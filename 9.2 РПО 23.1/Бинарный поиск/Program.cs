using System;

namespace Бинарный_поиск
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int nachalo = 1;
            int konec = 100;

            Console.WriteLine("\n\n-------------------------------Линейный поиск-----------------------------------\n\n");
            Console.WriteLine($"Загадайте число от {nachalo} до {konec}");

            for (int i = nachalo; i <= konec; i++)
            {
                Console.WriteLine($"Вы загадали {i}?");
                string answer = Console.ReadLine();
                if (answer == "y" || answer == "yes")
                {
                    Console.WriteLine($"Ура! Мы угадали! Ваше число: {i}");
                    break;
                }
                else
                {
                    continue;
                }
            }

            Console.WriteLine("\n\n------------------------------Бинарный поиск-----------------------------------\n\n");
            int number = konec;
            Console.WriteLine($"Загадайте число от {nachalo} до {konec}");
            number = konec / 2;
            while (true)
            {
                Console.WriteLine($"Ваше число {number}?");
                string answer = Console.ReadLine();
                if (answer == "да")
                {
                    Console.WriteLine($"Ура! Мы угадали! Ваше число: {number}");
                    break;
                }
                else if (answer == "больше")
                {
                    nachalo = number;
                    number = (number + konec) / 2;
                    continue;
                }
                else if (answer == "меньше")
                {
                    konec = number;
                    number = (nachalo + number) / 2;
                    continue;
                }
            }
        }
    }
}
