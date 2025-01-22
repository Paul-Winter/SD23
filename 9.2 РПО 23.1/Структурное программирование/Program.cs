using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Структурное_программирование
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int x = 0;
            do
            {
                Console.Write("Введите число: ");
                x = Convert.ToInt32(Console.ReadLine());

                //  условный оператор
                if (x == 0)
                {
                    Console.WriteLine("Вы ввели ноль!");
                }

                if (x % 2 == 0)
                {
                    Console.WriteLine("Вы ввели чётное число!");
                }
                else
                {
                    Console.WriteLine("Вы ввели нечётное число!");
                }
            } while (x != 0);

            //  условная операция
            //Console.WriteLine((x % 2 == 0) ? "чёт" : "нечет");

            //  переключатель
            //switch (x)
            //{
            //    case 0: Console.WriteLine("Вы ввели ноль!"); break;
            //    case 1: Console.WriteLine("Вы ввели единицу!"); break;
            //    case -1: Console.WriteLine("Вы ввели минус один!"); break;
            //    default:
            //        Console.WriteLine("Вы ввели число: {0}", x); break;
            //}
        }
    }
}
