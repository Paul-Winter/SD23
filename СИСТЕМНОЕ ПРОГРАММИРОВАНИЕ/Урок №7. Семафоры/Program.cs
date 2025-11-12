using System;
using System.Threading;
using System.Threading.Tasks;

namespace Урок__7.Семафоры
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Синхронизация семафорами:");
            Semaphore semaphore = new Semaphore(3, 3, "My_Semaphore");

            for (int i = 0; i < 6; i++)
            {
                ThreadPool.QueueUserWorkItem(SemaphoreSync, semaphore);
            }
            Console.ReadLine();
        }
        static void SemaphoreSync(object obj)
        {
            Semaphore semaphore = obj as Semaphore;
            bool stop = false;
            while (!stop)
            {
                if (semaphore.WaitOne(500))
                {
                    try
                    {
                        Console.WriteLine($"Поток №{Thread.CurrentThread.ManagedThreadId} заблокирован");
                        Thread.Sleep(2000);
                    }
                    finally
                    {
                        stop = true;
                        semaphore.Release();
                        Console.WriteLine($"Поток №{Thread.CurrentThread.ManagedThreadId} разблокирован");
                    }
                }
                else
                {
                    Console.WriteLine($"Таймаут потока №{Thread.CurrentThread.ManagedThreadId} истёк");
                }
            }
        }
    }
}
