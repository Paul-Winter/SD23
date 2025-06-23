using System;

namespace NET_Урок__1_2_Типы_данных__массивы_и_строки
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Массивы
            /*
            int[] myArray = new int[10] { 1,2,3,4,5,6,7,8,9,0 };
            int[,] myArray2 = new int[4,5] { { 1,2,3,4,5}, { 4,5,6,7,8}, { 7,8,9,0,1 }, { 2,3,4,5,6} };
            int[][] myJagged = new int[5][];
            myJagged[0] = new int[] { 1, 2 };
            myJagged[1] = new int[] { 1, 3, 5, 7, 9 };
            myJagged[2] = new int[] { 1, 2, 3 };
            myJagged[3] = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            myJagged[4] = new int[] { 9, 8, 7, 6 };

            foreach (int i in myArray)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine("\n");

            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    Console.Write(myArray2[i,j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n");

            foreach(int[] i in myJagged)
            {
                foreach (int j in i)
                {
                    Console.Write(j + " ");
                }
                Console.WriteLine();
            }
            */

            // Строки
            /*string str1 = "ПРОСТАЯ СТРОКА";
            char[] charArr = { 'П', 'р', 'о', 'с', 'т', 'а', 'я', ' ', 'с', 'т', 'р', 'о', 'к', 'а' };
            string str2 = new string(charArr);
            string str3 = new string(charArr, 8, 6);
            string str4 = new string('$', 10);

            Console.WriteLine("str1: " + str1);
            Console.WriteLine("str2: " + str2);
            Console.WriteLine("str3: " + str3);
            Console.WriteLine("str4: " + str4);

            string strTA1 = "\\Компьютерная академия \"TOP\"\\";
            Console.WriteLine(strTA1);
            string strTA2 = @"\
                               Компьютерная академия 
                                       'ТОР'
\";
            Console.WriteLine(strTA2);
            string str5 = strTA1 + "\n" + strTA2;
            Console.WriteLine(str5);

            int x = 1000;
            int y = 20565;
            string str6 = "значение sdfjlksdl";
            Console.WriteLine($"x = {x}; y = {y}\nstr6 = {str6}");

            Console.Write("Пожалуйста, введите значение: ");
            string result = Console.ReadLine();
            if (str1.Equals(result.ToUpper()))
            {
                Console.WriteLine("Пароль верный!");
            }
            else
            {
                Console.WriteLine("Неправильный пароль!!!");
            }

            //String.Format("Печатаемый текст {индекс, размер:спецификатор}", данные);

            double test1 = 999999.987;
            int test2 = 99999;
            Console.WriteLine(String.Format("c format: {0,15:C}", test1));
            Console.WriteLine(String.Format("d format: {0:D9}", test2));
            Console.WriteLine(String.Format("e format: {0:E}", test1));
            Console.WriteLine(String.Format("f format: {0:F2}", test1));
            Console.WriteLine(String.Format("g format: {0:G}", test1));
            Console.WriteLine(String.Format("n format: {0,15:N}", test2));
            Console.WriteLine(String.Format("p format: {0:P}", test1));
            Console.WriteLine(String.Format("X format: {0:X}", test2));
            Console.WriteLine(String.Format("x format: {0:x}", test2));
            */

            // Продемонстрировать работу методов согласно варианта:

            // Александрова - CopyTo,       IndexOfAny,     Contains
            // Антонов      - Equals,       LastIndexOf,    Concat
            // Духина       - Compare,      CompareOrdinal, Remove
            // Землянский   - StartsWith,   EndsWith,       Insert
            // Золин        - IndexOf,      LastIndexOf,    Compare
            // Красицкий    - IndexOfAny,   LastIndexOfAny, Trim
            // Кубанов      - Substring,    Split,          Equals
            // Лушников     - Concat,       IndexOf,        LastIndexOfAny
            // Мамонтова    - Contains,     StartsWith,     EndsWith
            // Метелицин    - Insert,       Remove,         Compare
            // Чавычалов    - PadLeft,      PadRight,       Equals
            // Юнусов       - Split,        CopyTo,         Trim

            // ASCII-art
            // Александрова - утка
            // Антонов      - кабан
            // Духина       - коала
            // Землянский   - ёж
            // Золин        - барсук
            // Красицкий    - геккон
            // Кубанов      - гиббон
            // Лушников     - олень
            // Мамонтова    - медуза
            // Метелицин    - козёл
            // Чавычалов    - горилла
            // Юнусов       - лемур

            // Параметры командной строки
            foreach (string item in args)
            {
                Console.WriteLine(item);
            }
        }
    }
}
