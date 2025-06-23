using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NET_Урок__13_LINQ
{
    #region Extension методы
    static class Example
    {
        public static int NumberWords(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return 0;
            }

            string[] result = input.Trim().Split(',');
            return result.Length;
        }
    }
    #endregion

    #region Регулярные выражения
    #endregion

    internal class Program
    {
        static void Main(string[] args)
        {
            string emailPattern = @"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$";
            Console.WriteLine("Enter e-mail: ");
            string email = Console.ReadLine();
            Regex regex = new Regex(emailPattern);
            if (regex.IsMatch(email))
            {
                Console.WriteLine("E-mail введён правильно!");
            }
            else
            {
                Console.WriteLine("Неверный e-mail!");
            }
            // написать extension-методы 3 шт. и
            // продемонстрировать их работу

            //Console.WriteLine("Введите текст строки:");
            //string str = Console.ReadLine();
            //Console.WriteLine($"Количество слов в строке: {str.NumberWords()}");
        }
    }
}
