using Human;
using System;
using Workers;

////////////////////////////////////////////////////////////////////////////////////
//                                                                                //
//      ЗАДАНИЕ:                                                                  //
//  1. Создать проект-пример работы с унаследованным кодом                        //
//  2. Создать библиотеку и использовать её в проекте                             //
//  3. Создать проект библиотеки в рамках текущего решения                        //
//                                                                                //
////////////////////////////////////////////////////////////////////////////////////

namespace Урок__1.Унаследованный_код
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Employee employee = new Employee();
            Console.WriteLine(employee);
            Worker worker = new Worker("Ivan", "Ivanov", 18, 40000.00, "intern");
            Console.WriteLine(worker);
            Console.ReadLine();
        }
    }
}
