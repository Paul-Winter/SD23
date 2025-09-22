using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiletNomber11;

namespace Albert_PiTPM_Practica
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Summa summa = new Summa();

            Console.WriteLine(summa.Slojenie());
        }
    }
    
    public class Summa
    {
        Class1 class1 = new Class1();
        public float Slojenie()
        {
            float a = 20;
            float b = 25;
            
            return class1.AplusB(a, b);

        }
        public  void Vichitanie()
        {
            float a = 20;
            float b = 25;

            class1.AplusB(a, b);

        }
        public void Umnpjenie()
        {
            float a = 20;
            float b = 25;

            class1.AplusB(a, b);
        }
    }
}
