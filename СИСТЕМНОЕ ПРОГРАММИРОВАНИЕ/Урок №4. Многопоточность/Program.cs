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

        static void Method()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("\t\t\tHello in Thread!");
                Thread.Sleep(1000);
            }
        }

        static void Main(string[] args)
        {
            /* 2. Создать объект делегата и связать с ним метод
            //TimerCallback callback = new TimerCallback(TimerMethod);

            // 3. Создать объект таймера и передать конструктор-делегат
            //Timer timer = new Timer(callback);

            //Console.WriteLine("Timer start!");

            // 4. Указать интервал таймера        
            //timer.Change(10000, 2000);

            //Console.ReadLine();*/
            // Создать объект делегата
            ThreadStart threadStart = new ThreadStart(Method);

            // Создать объект потока
            Thread thread = new Thread(threadStart);

            thread.IsBackground = true;

            // Запуск потока
            thread.Start();

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Hello in Main");
                Thread.Sleep(300);
            }

            thread.Abort();
        }
    }
}
