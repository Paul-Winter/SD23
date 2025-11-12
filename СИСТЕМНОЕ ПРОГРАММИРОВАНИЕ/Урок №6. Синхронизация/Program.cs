using System;
using System.Threading;

namespace Урок__6.Синхронизация
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
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
            */
            GoodLockAsync();
        }

        // Модифицировать пример решения взаимных блокировок на несколько полей
        // согласно варианта:
        // Антонов      - 4
        // Золин        - 5
        // Красицкий    - 5
        // Кубанов      - 4
        // Лушников     - 3
        // Метелицин    - 3

        //~Program()
        //{
        //    lock(this)
        //    {
        //        Console.WriteLine("какая-то работа");
        //    }
        //}

        private static void BadAsync()
        {
            Console.WriteLine("Синхронизация Interlocked-методами:");
            InterlockedCounter counter = new InterlockedCounter();
            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(counter.UpdateFields);
                threads[i].Start();
            }
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
            Console.WriteLine($"Field1 = {counter.Field1}\nField2 = {counter.Field2}");
        }
        private static void GoodAsync()
        {
            Console.WriteLine("Синхронизация блокировкой:");
            MonitorLockCounter counter = new MonitorLockCounter();
            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(counter.UpdateFields);
                threads[i].Start();
            }
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
            Console.WriteLine($"Field1 = {counter.Field1}\nField2 = {counter.Field2}");
        }
        private static void StaticAsync()
        {
            Console.WriteLine("Синхронизация блокировкой:");
            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(StaticLockCounter.UpdateFields);
                threads[i].Start();
            }
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
            Console.WriteLine($"Field1 = {StaticLockCounter.Field1}\nField2 = {StaticLockCounter.Field2}");
        }
        private static void GoodLockAsync()
        {
            Console.WriteLine("Синхронизация статического типа:");
            LockCounter counter = new LockCounter();
            Monitor.Enter(counter);
            Thread[] threads = new Thread[5];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(counter.UpdateFields);
                threads[i].Start();
            }
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
            Console.WriteLine($"count = {counter.Count}");
        }
    }


    class Counter
    {
        public static int count;
    }
    class InterlockedCounter
    {
        int field1;
        int field2;

        public int Field1
        {
            get { return field1; }
        }
        public int Field2
        {
            get { return field2; }
        }
        public void UpdateFields()
        {
            for (int i = 0; i < 1000000; i++)
            {
                Interlocked.Increment(ref field1);
                if (field1 % 2 == 0)
                {
                    Interlocked.Increment(ref field2);
                }
            }
        }
    }
    class MonitorLockCounter
    {
        int field1;
        int field2;
        public int Field1 { get { return field1; } }
        public int Field2 { get { return field2; } }
        public void UpdateFields()
        {
            for (int i = 0; i < 1000000; i++)
            {
                lock (this)
                {
                    ++field1;
                    if (field1 % 2 == 0)
                    {
                        ++field2;
                    }
                }
            }
        }
    }
    static class StaticLockCounter
    {
        static int field1;
        static int field2;
        public static int Field1 { get { return field1; } }
        public static int Field2 { get { return field2; } }
        public static void UpdateFields()
        {
            for (int i = 0; i < 1000000; i++)
            {
                lock (typeof(StaticLockCounter))
                {
                    ++field1;
                    if (field1 % 2 == 0)
                    {
                        ++field2;
                    }
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
