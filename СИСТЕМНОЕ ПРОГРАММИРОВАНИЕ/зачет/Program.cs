using System;
using System.Collections.Generic;
using System.Threading;

namespace зачет
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Обьект потока");
            for (int i = 0; i < 10;)
            {
                
            }

            /*Thread thread1 = new Thread(Method);
            thread1.Priority = ThreadPriority.Lowest;
            thread1.Start();
            Thread thread2 = new Thread(Method);
            thread2.Priority = ThreadPriority.Normal;
            thread2.Start();
            Thread thread3 = new Thread(Method);
            thread3.Priority = ThreadPriority.Highest;
            thread3.Start();

            Console.WriteLine("Начали работать! ");
            

            thread1.Join();
            thread2.Join();
            thread3.Join();*/
            
            Thread[] threads = new Thread[3];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(Method);
                Console.WriteLine(threads[i].ManagedThreadId.ToString());
                threads[i].Start();
            }
            Thread.Sleep(10000);
            Console.WriteLine("Основной поток приостановлен+++++");
            
            for (int i = 0;i < threads.Length;i++)
            {
                threads[i].Abort();
                Console.WriteLine("Поток прервался");
            }
             
        }
    
        static void Method()
        {
            for(int  i = 1; i <= 10 ; i++)
            {
                Console.WriteLine($"Id потока {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"Приоритет потока {i}");
                Thread.Sleep(500);
            }
        }
    }
}