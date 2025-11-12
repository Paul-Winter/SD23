using System;
using System.Threading;

namespace Урок__6.Синхронизация
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(delegate ()
                {
                    for (int j = 0; j < 1000000; j++)
                    {
                        //++Counter.count;
                        Interlocked.Increment(ref Counter.count);
                    }
                });
                threads[i].Start();
            }
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
            Console.WriteLine($"counter = {Counter.count}");
        }
    }

    class Counter
    {
        public static int count;
    }
}
