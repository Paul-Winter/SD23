using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace РПМ_ПР_3_Рекурсия
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Введите число: ");
                long number = Convert.ToInt64(Console.ReadLine());
                Console.WriteLine($"Факториал {number}! = {Factorial(number)}");
            }
        }

        //static тип_возвращаемого_значения ИмяФункции (тип_аргумента имя_аргумента)
        //{
        //     тело функции;
        //}

        // 5! = 1 * 2 * 3 * 4 * 5 = 120
        // 4! = 1 * 2 * 3 * 4 = 24
        // 3! = 1 * 2 * 3 = 6
        // 2! = 1 * 2 = 2
        // 1! = 1

        //Factorial
        static long Factorial(long x)
        {
            if (x == 0 || x == 1)
            {
                return 1;
            }
            else
            {
                return x * Factorial(x - 1);
            }
        }

        //NOD
        static int NOD(int x, int y)
        {
            int NOD = x * y;
            if (NOD >= x && NOD >= y)
                return NOD;
            return NOD;
        }

        // 0 1 1 2 3 5 8 13 21 34 55

        //Fibonacci
        static int Fib(int x)
        {
            return x;
        }

        //Palindrome
        static bool Palindrome(string str, int i, int j, bool isContinue)
        {
            char[] pal = str.ToCharArray();
            if (i == j && pal[i] == pal[j])
            {
                Console.WriteLine("Palindrome");
                return false;
            }
            i++;
            j--;
            return true;
        }
    }
}
