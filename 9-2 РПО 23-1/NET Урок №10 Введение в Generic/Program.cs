using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace NET_Урок__10_Введение_в_Generic
{
    internal class Program
    {
        /* Jeffrey Richter "The CLR via C#"
        private static void ValueTypePerTest()
        {
            const int COUNT = 10000000;

            using (new OperationTimer("List"))
            {
                List<int> list = new List<int>(COUNT);
                for (int i = 0; i < COUNT; i++)
                {
                    list.Add(i);
                    int x = list[i];
                }
                list = null;
            }

            using (new OperationTimer("ArrayList"))
            {
                ArrayList array = new ArrayList();
                for (int i = 0; i < COUNT; i++)
                {
                    array.Add(i);
                    int x = (int)array[i];
                }
                array = null;
            }
        }
        */

        static void Main(string[] args)
        {
            // Продемонстрировать работу обобщённых коллекций с типом согласно варианта:
            //                  Коллекция     Тип
            // Александрова -   List        float
            // Антонов      -   Stack       char
            // Духина       -   Dictionary  int, string
            // Землянский   -   Queue       double
            // Золин        -   LinkedList  string
            // Красицкий    -   SortedList  double, string
            // Кубанов      -   SortedList  string, decimal
            // Лушников     -   Stack       ulong
            // Мамонтова    -   Queue       int
            // Метелицин    -   Dictionary  string, int
            // Чавычалов    -   SortedList  char, double
            // Юнусов       -   List        string

            /* Boxing & Unboxing
            object obj = 45; // boxing
            Console.WriteLine($"Упаковка: {obj}");

            int number = (int)obj; // unboxing
            Console.WriteLine($"Распаковка: {number}");
            */
            //ValueTypePerTest();

            /* ArrayList
            ArrayList arrList1 = new ArrayList();
            ArrayList arrList2 = new ArrayList(5);
            ArrayList arrList3 = new ArrayList(new int[] { 1, 2, 5, 84 });

            Console.WriteLine($"Вместимость {arrList1.Capacity}, Количество элементов {arrList1.Count}");
            Console.WriteLine($"Вместимость {arrList2.Capacity}, Количество элементов {arrList2.Count}");
            Console.WriteLine($"Вместимость {arrList3.Capacity}, Количество элементов {arrList3.Count}");
            
            arrList3.Add(54);
            arrList3.Add(42);
            arrList3.Remove(1);
            arrList3.RemoveAt(0);
            Console.WriteLine(arrList3[0]);
            arrList3.Insert(0, "Hello");
            Console.WriteLine(arrList3.IndexOf("Hello"));

            foreach(object item in arrList3)
            {
                Console.WriteLine(item);
            }
            //arrList3.Sort();
            */

            /* Stack
            // LIFO (last-in-first-out)
            Stack stack1 = new Stack();
            Stack stack2 = new Stack(7);
            Stack stack3 = new Stack(new ArrayList { 3, 5 });
            Stack stack = new Stack();

            Console.WriteLine(stack1.Count);
            Console.WriteLine(stack2.Count);
            Console.WriteLine(stack3.Count);

            Console.WriteLine("Push");
            stack.Push("one");
            stack.Push("two");
            stack.Push("three");

            Console.WriteLine("Pop");
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());

            Console.WriteLine("Peek");
            Console.WriteLine(stack.Peek());
            Console.WriteLine(stack.Count);
            */

            /* Queue
            // FIFO (first-in-first-out)
            Queue queue1 = new Queue();
            Queue queue2 = new Queue(3);
            Queue queue3 = new Queue(new ArrayList { "one", 2, 3.45 });

            Console.WriteLine($"{queue1.Count}");
            Console.WriteLine($"{queue2.Count}");
            Console.WriteLine($"{queue3.Count}");

            queue3.Enqueue(4.0f);

            for(int i = 0; i < 4; i++)
            {
                Console.WriteLine(queue3.Dequeue());
            }
            */

            /* Hashtable
            // key-value
            Hashtable ht = new Hashtable();
            ht.Add(1, "студент");
            ht.Add("two", new Student { LastName = "Иванов" });

            foreach (object item in ht.Keys)
            {
                Console.WriteLine($"Key: {item} - Value: {ht[item]}");
            }

            ht.Add("Pi", 3.14159);

            foreach (object item in ht.Keys)
            {
                Console.WriteLine($"Key: {item} - Value: {ht[item]}");
            }
            */

            /* SortedList
            SortedList sortedList = new SortedList();
            sortedList.Add(3, 6.7);
            sortedList.Add(2, new Student { FirstName = "Иван" });

            foreach (object item in sortedList.Keys)
            {
                Console.WriteLine($"Key: {item}; Value: {sortedList[item]}");
            }
             */
        }
    }

    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public override string ToString()
        {
            return $"Студент: {LastName} {FirstName}\nДата рождения: {BirthDate.ToLongDateString()}";
        }
    }

    internal sealed class OperationTimer : IDisposable
    {
        long startTime;
        string text;
        int collectionCount;

        public OperationTimer(string text)
        {
            PrepareForOperation();
            this.text = text;
            collectionCount = GC.CollectionCount(0);
            startTime = Stopwatch.GetTimestamp();
        }

        public void Dispose()
        {
            Console.WriteLine($"{text}\t" +
                $"{(Stopwatch.GetTimestamp() - startTime) / (double)Stopwatch.Frequency:0.00} секунды " +
                $"(сборок мусора {GC.CollectionCount(0) - collectionCount})");
        }

        private static void PrepareForOperation()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
