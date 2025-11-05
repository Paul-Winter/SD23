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

        static void Method(object str)
        {
            string text = (string)str;
            for (int i = 0; i < 2000; i++)
            {
                Console.WriteLine($"{text}\t#{i.ToString()}");
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

            /* Создать объект делегата
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
            */

            ParameterizedThreadStart ts = new ParameterizedThreadStart(Method);

            Thread t1 = new Thread(ts);
            Thread t2 = new Thread(ts);
            Thread t3 = new Thread(ts);
            Thread t4 = new Thread(ts);
            Thread t5 = new Thread(ts);

            t1.Priority = ThreadPriority.Highest;
            //t2.Priority = ThreadPriority.AboveNormal;
            t3.Priority = ThreadPriority.Normal;
            //t4.Priority = ThreadPriority.BelowNormal;
            t5.Priority = ThreadPriority.Lowest;

            t5.Start((object)"\t\t\t\tt5");
            //t4.Start((object)"\t\t\tt4");
            t3.Start((object)"\t\tt3");
            //t2.Start((object)"\tt2");
            t1.Start((object)"t1");

            Console.ReadKey();
        }
    }
}
