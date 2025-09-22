using System;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace Metelicin_Practika
{
    public class Parent
    {
        protected string Name;
        protected int Age;
        protected string HairColor;
        protected string EyeColor;
        public Parent(string name, int age, string hairColor, string eyeColor)
        {
            this.Name = name;
            this.Age = age;
            this.HairColor = hairColor;
            this.EyeColor = eyeColor;
        }
    }
    public class Child : Parent
    {
        //protected new string Name;
        //protected new int Age;
        //protected new string HairColor;
        //protected new string EyeColor;
        public override string ToString()
        {
             return $"{Name} , {Age} , {HairColor} , {EyeColor}";;
        }
        public Child(string name, int age, string hairColor, string eyeColor) : base(name , age , hairColor , eyeColor)
        {
            
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Parent parent = new Parent("Денис" , 29 , "Чёрные" ,"Зелёные");
            Child child = new Child("Данил" , 9 , "Чёрные" , "Голубые");
            Console.WriteLine(child);
        }
    }
}
