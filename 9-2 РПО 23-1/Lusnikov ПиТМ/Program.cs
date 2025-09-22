using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lusnikov_ПиТМ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Class2 class2 = new Class2();

            //Console.ReadLine();

            Console.WriteLine(class2.Stroka("Hello, World!"));

            class2.Stroka("Hello, World!");
        }

        public string name;
    }

    public class Class2 
    {
        public string HelloWorld { get; set; }

        public string Stroka(string name)
        {
            HelloWorld = name;

            return HelloWorld;

        }
    }
}