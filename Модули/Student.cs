using System;
using System.Threading;

namespace Модули
{
    public class Student
    {
        public const double k = 0.3;
        public const int HUNGRY_TIME_DELAY = 10000;
        public const int SLEEP_TIME_DELAY = 30000;

        public void ShowStudent()
        {
            Console.WriteLine("Показать студента");
        }

        public void InsertStudent()
        {
            Console.WriteLine("Добавить студента в БД");
        }

        public void FeedStudent()
        {
            Console.WriteLine("АНИМАЦИЯ ГОЛОДНОГО СТУДЕНТА");
            Thread.Sleep(Convert.ToInt32(Math.Round(HUNGRY_TIME_DELAY * k)));
            Console.WriteLine("Покормить студента!");
            Console.WriteLine("АНИМАЦИЯ СЫТОГО СТУДЕНТА");
            Thread.Sleep(Convert.ToInt32(Math.Round(SLEEP_TIME_DELAY * k)));
            Console.WriteLine("АНИМАЦИЯ СПЯЩЕГО СТУДЕНТА");
        }
    }
}
