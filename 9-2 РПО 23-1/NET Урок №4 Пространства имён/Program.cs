using System;
using Home;
using VUZ.Cathedra;

namespace NET_Урок__4_Пространства_имён
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Lake.Naturalist.Fishing();
            Forest.Naturalist.Mushroom();
            Friend.HelloFrom();
            Work.Friend.HelloFrom();
            VUZ.Friend.HelloFrom();
            ZavKaf.SetOK();

            MethodicStaff metodist = new MethodicStaff();
            metodist.GetMethodic();
        }
    }
}

namespace Home
{
    class Friend
    {
        public static void HelloFrom()
        {
            Console.WriteLine("Привет из дома!");
        }
    }
}

namespace VUZ
{
    namespace Cathedra
    {
        class MethodicStaff
        {
            public void GetMethodic()
            {
                Console.WriteLine("Возьмите методичку!");
            }
        }

        class ZavKaf
        {
            public static void SetOK()
            {
                Console.WriteLine("Одобрено!");
            }
        }
    }

    class Friend
    {
        public static void HelloFrom()
        {
            Console.WriteLine("Привет из универа!");
        }
    }
}

namespace Work
{
    class Friend
    {
        public static void HelloFrom()
        {
            Console.WriteLine("Привет с работы!");
        }
    }
}
namespace Lake
{
    class Naturalist
    {
        public static void Fishing()
        {
            Console.WriteLine("рыбалка");
        }
    }
}
namespace Forest
{
    class Naturalist
    {
        public static void Mushroom()
        {
            Console.WriteLine("Сбор грибов");
        }
    }

}