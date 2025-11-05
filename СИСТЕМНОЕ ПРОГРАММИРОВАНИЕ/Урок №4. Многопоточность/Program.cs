using System;
using System.Threading;

namespace Урок__4.Многопоточность
{
    internal class Program
    {
        // 1. Описать метод, который будет выполняться
        public static void TimerMethod(object a)
        {
            Console.WriteLine("Hello in timer!");
        }


        static void Main(string[] args)
        {
            // 2. Создать объект делегата и связать с ним метод
            TimerCallback callback = new TimerCallback(TimerMethod);

            // 3. Создать объект таймера и передать конструктор-делегат
            Timer timer = new Timer(callback);

            Console.WriteLine("Timer start!");

            // 4. Указать интервал таймера        
            timer.Change(10000, 2000);

            Console.ReadLine();
        }
    }
}
