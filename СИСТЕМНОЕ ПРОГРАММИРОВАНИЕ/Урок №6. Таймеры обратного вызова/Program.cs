using System;
using System.Threading;

namespace Урок__6.Таймеры_обратного_вызова
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Timer timer = new Timer(TimerMethod, null, 0, 1000);
            Console.WriteLine($"Основной поток №{Thread.CurrentThread.ManagedThreadId} продолжается");
            Thread.Sleep(5000);
            timer.Dispose();
        }
        static void TimerMethod(Object obj)
        {
            Console.WriteLine($"Поток №{Thread.CurrentThread.ManagedThreadId} : {DateTime.Now}");
        }
    }
}
