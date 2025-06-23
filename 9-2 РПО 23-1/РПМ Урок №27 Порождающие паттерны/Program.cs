using System;

namespace РПМ_Урок__27_Порождающие_паттерны
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Singleton single = Singleton.Instance();
            single.GetSingletonData();
        }
    }

    class Singleton
    {
        private static Singleton uniqueInstance;
        private string singletonData;

        private Singleton()
        { }

        public static Singleton Instance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new Singleton();
            }
            return uniqueInstance;
        }

        public string GetSingletonData()
        {
            return singletonData;
        }
    }
}
