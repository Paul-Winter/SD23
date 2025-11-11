using System;
using System.Threading;

namespace Урок__5.Пул_потоков
{
    // Написать программу, демонстрирующую работу пула потоков
    // в нём нужно запустить методы, количеством, согласно варианта
    // методы должны выполняться разное количество времени

    // Антонов      -   4
    // Землянский   -   4
    // Золин        -   5
    // Красицкий    -   5
    // Кубанов      -   6
    // Лушников     -   3
    // Метелицин    -   7
    // Юнусов       -   3

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Основной поток: ставим в очередь рабочий элемент");
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(WorkingMethod, random.Next(10));
            }
            Console.WriteLine("Основной поток: выполняем другие задачи");
            Thread.Sleep(1000);
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadLine();
        }

        static void WorkingMethod(object state)
        {
            Console.WriteLine($"\tпоток: {Thread.CurrentThread.ManagedThreadId} состояние: {state}");
            Thread.Sleep(1000);
        }
    }
}
