using System;

namespace РПМ_Урок__9_ООП
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            
            Pet cat = new Pet();
            Pet dog = new Pet();

            cat.color = "ginger";
            dog.age = 5;
            cat.Sound("meow");
            dog.Sound("gaw-gaw");
        }
    }

    public class Pet
    {
        public int age;
        public string color;
        public string name;

        public void Sound(string sound)
        {
            Console.WriteLine(sound);
        }
    }
}
