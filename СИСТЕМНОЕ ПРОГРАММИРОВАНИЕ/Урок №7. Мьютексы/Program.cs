using System;
using System.Threading;

namespace Урок__7.Мьютексы
{
    internal class Program
    {
        static Mutex mutex = new Mutex();

        static void Main(string[] args)
        {
            Console.WriteLine("Синхронизация мьютексами:");
            Mutex mutex = new Mutex();
            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(MutexSync);
                threads[i].Start();
            }
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
            
        }
        static void MutexSync()
        {
            bool stop = false;
            while (!stop)
            {
                if (mutex.WaitOne(500))
                {
                    try
                    {
                        Console.WriteLine($"Поток №{Thread.CurrentThread.ManagedThreadId} заблокирован");
                        Thread.Sleep(2000);
                    }
                    finally
                    {
                        stop = true;
                        mutex.ReleaseMutex();
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

    class LockCounter
    {
        int count;
        object lockObj = new object();
        public int Count { get { return count; } }
        public void UpdateFields()
        {
            for (int i = 0; i < 1000000; i++)
            {
                lock(lockObj)
                {
                    ++count;
                }
            }
        }
    }
}
