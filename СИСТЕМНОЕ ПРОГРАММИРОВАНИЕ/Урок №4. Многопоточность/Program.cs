using System;
using System.Threading;

namespace Урок__4.Многопоточность
{
    internal class Program
    {
        static Random random = new Random();
        // 1. Описать метод, который будет выполняться
        public static void TimerMethod(object a)
        {
            Console.WriteLine("Hello in timer!");
        }

        static void Method()
        {
            Console.WriteLine("Поток работает!");
            Thread.Sleep(500);
            Console.WriteLine("Ожидание завершения потока!");
        }
        static void Method(object str)
        {
            string text = (string)str;
            for (int i = 0; i < 2000; i++)
            {
                Console.WriteLine($"{text}\t#{i.ToString()}");
            }
        }

        static void ListenerClient()
        {
            int counter = 0;
            while (true)
            {
                Console.WriteLine("Нажмите любую клавишу для симуляции подключения");
                Console.ReadKey(true);
                ParameterizedThreadStart userInput = new ParameterizedThreadStart(UserThreadWork);
                Thread userThread = new Thread(userInput);
                userThread.Start((object)counter.ToString());
                counter++;
            }
        }

        private static void UserThreadWork(object a)
        {
            string userName = (string)a;
            Console.WriteLine($"пользователь\t{userName} подключился");

            while (true)
            {
                switch (GetUserCommand())
                {
                    case 0: Console.WriteLine($"#\t{userName} подписался на новости");
                        break;
                    case 1: Console.WriteLine($"#\t{userName} начал чат");
                        break;
                    case 2: Console.WriteLine($"#\t{userName} купил продукцию в магазине");
                        break;
                    case 3: Console.WriteLine($"#\t{userName} отправил письмо");
                        break;
                    default:
                        Console.WriteLine($"#\t{userName} отключился");
                        return;
                }
            }
        }

        private static int GetUserCommand()
        {
            return random.Next(0, 5);
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

            /*ParameterizedThreadStart ts = new ParameterizedThreadStart(Method);

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
            */

            /*ThreadStart ts = new ThreadStart(Method);
            Thread thread = new Thread(ts);
            Console.WriteLine("Запуск потока!");
            thread.Start();
            Thread.Sleep(2000);
            Console.WriteLine("Поток завершил работу!");
            */

            ThreadStart ts = new ThreadStart(ListenerClient);
            Thread userThread = new Thread(ts);
            userThread.IsBackground = false;
            userThread.Start();
        }
    }
}
