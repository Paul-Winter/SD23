using System;

namespace РПМ_Урок__40_Оптимизация_программного_кода
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int A;
            int B = 2;
            int C = 3;
            int D;
            bool X = true;
            bool Y = false;
            bool Z = false;
            int[] Ar = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //// удаление бесполезных присваиваний
            //A = B * C;
            D = B + C;
            A = D * C;

            //// удаление избыточных вычислений
            //A = 2 * B * C * 3;
            //A = (2 * 3) * (B * C);
            A = 6 * B * C;

            //A = B * C + B * D;
            A = B * (C + D);

            //// свёртка объектного кода
            D = 1 + 1;      // D 2
            D = 3;          // D 3
            C = 6 * D + D;  // C 21

            //// исключений лишних операций
            D = D + C * B;
            A = D + C * B;
            C = D + C * B;
            // 1) C * B
            // 2) D + ^
            // 3) D = ^
            // 4) C * B
            // 5) D + ^
            // 6) A = ^
            // 7) C * B
            // 8) D + ^
            // 9) C = ^

            // 1) C * B
            // 2) D + ^
            // 3) D = ^
            // 4) A = D + ^1
            // 5) C = ^

            //// оптимизация логических выражений
            bool W = X || Y || Z;
            // bool W = X || F1(Y) || F2(Z);

            //// оптимизация циклов
            //// вынесение инвариантных вычислений
            for (int i = 0; i < 10; i++)
            {
                Ar[i] = B * C * Ar[i];
            }
            //
            D = B * C;
            for (int i = 0; i < 10; i++)
            {
                Ar[i] = D * Ar[i];
            }

            //// оптимизация циклов
            //// замена операций с индуктивными переменными
            int S = 10;
            for (int i = 0; i < 10; i++)
            {
                Ar[i] = i * S;
                Console.WriteLine("Ar[i] = " + Ar[i]);
            }
            Console.WriteLine("\n");
            //
            S = 10;
            int T = 0;
            int I = 0;
            while (I < 10)
            {
                Ar[I] = T;
                Console.WriteLine("Ar[I] = " + Ar[I]);
                T += S;
                I++;
            }

            //// оптимизация циклов
            //// слияние циклов
            int length = 3;
            int width = 3;
            int[,] Arr = new int[length,width];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Arr[i, j] = 0;
                }
            }
            //
            int size = length * width;
            int[] Array = new int[size];
            for (int i = 0; i < size; i++)
            {
                Array[i] = 0;
            }

            //// оптимизация циклов
            //// развёртывание циклов
            for (int i = 0; i < 3; i++)
            {
                Ar[i] = i;
            }
            //
            Ar[0] = 0;
            Ar[1] = 1;
            Ar[2] = 2;

            //// оптимизация кода для процессоров, допускающих распараллеливание вычислений
            // A + B + C + D + I + T
            //1 поток: ((((A + B) + C) + D) + I) + T
            //2 поток: ((A + B) + C)  +   ((D + I) + T)
            //3 поток: (A + B)    +   (C + D)     +   (I + T)

        }
    }
}
