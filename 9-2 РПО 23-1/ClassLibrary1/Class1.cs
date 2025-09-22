using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Class1
    {
        public int Sum (int a, int b)
        {
            return a + b;
        }
        public int Subract(int a, int b) 
        {
            return a - b; 
        }
        public int Multiply(int a, int b)
        {
            return a * b;
        }
        public int Divide(int a, int b)
        {
            if (a == 0)
               throw new DivideByZeroException("делить на нуль не возможно ");
            return a / b;
        }
    }
}
