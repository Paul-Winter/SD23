using System;

namespace NET_Урок__11_Сборка_мусора
{
    public class GarbageHelper : IDisposable
    {
        private class Person : IDisposable
        {
            string name;
            string surname;
            DateTime birthDate;            

            ~Person()
            {
                Dispose();
            }

            public void Dispose()
            {
                Console.WriteLine("Освобождение ресурсов Person");
            }
        }

        public void MakeGarbage()
        {
            for (int i = 0; i < 1000; i++)
            {
                Person p = new Person();
            }
            Dispose();
        }

        public void Dispose()
        {
            Console.WriteLine("Освобождение ресурсов Helper");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Демонстрация System.GC");
            Console.WriteLine($"Максимальное поколение: {GC.MaxGeneration}");

            GarbageHelper gh = new GarbageHelper();

            Console.WriteLine($"Поколение объекта: {GC.GetGeneration(gh)}");
            Console.WriteLine($"Занято памяти: {GC.GetTotalMemory(false)} байт");

            gh.MakeGarbage();

            Console.WriteLine($"Занято памяти: {GC.GetTotalMemory(false)} байт");

            GC.Collect(0);

            Console.WriteLine($"Занято памяти: {GC.GetTotalMemory(false)} байт");
            Console.WriteLine($"Поколение объекта: {GC.GetGeneration(gh)}");

            GC.Collect();

            Console.WriteLine($"Занято памяти: {GC.GetTotalMemory(false)} байт");
            Console.WriteLine($"Поколение объекта: {GC.GetGeneration(gh)}");

            // продемонстрировать переполнение типа
            // Александрова double  -> int
            // Антонов      float   -> byte
            // Духина       decimal -> float
            // Землянский   uint    -> ushort
            // Золин        double  -> long
            // Красицкий    decimal -> double
            // Кубанов      float   -> int
            // Лушников     double  -> short
            // Мамонтова    uint    -> byte
            // Метелицин    float   -> short
            // Чавычалов    long    -> int
            // Юнусов       ulong   -> ushort

            Console.WriteLine("\n\n\n");
            try
            {
                byte b = 100;
                Console.WriteLine($"byte b = {b}\n");

                checked
                {
                    b = (byte)(b + 20);
                }
                Console.WriteLine($"(byte){b} = {b}\n");
            }
            catch (OverflowException e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                int n = 65540;
                Console.WriteLine($"int n = {n}\n");
                short s;

                checked
                {
                    s = (short)n;
                }
                Console.WriteLine($"(short){n} = {s}\n");
            }
            catch (OverflowException e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                //Console.WriteLine("Выполнено подключение к БД");
                //Console.WriteLine("Подключение к серверу");

                //Console.WriteLine("Работа с БД 1");
                ////throw new MyException1();
                //Console.WriteLine("Работа с сервером");
                //throw new MyException2();
                //Console.WriteLine("Работа с БД 2");
            }
            catch (MyException1 ex)
            {
                Console.WriteLine(ex.Message + " Работа с БД 1");
            }
            catch (MyException2 ex)
            {
                Console.WriteLine(ex.Message + " Работа с сервером");
            }
            catch (Exception e)
            {

            }
            finally
            {
                //Console.WriteLine("Отключение от БД");
                //Console.WriteLine("Отключение от сервера");
                //Console.WriteLine("Освобождение ресурсов");
            }
        }
    }

    public class MyException1 : ApplicationException
    {
        private string message;
        public DateTime TimeException { get; private set; }

        public MyException1()
        {
            message = "Моё исключение";
            TimeException = DateTime.Now;
        }

        public override string Message
        {
            get { return message; }
        }
    }

    [Serializable]
    public class MyException2 : Exception
    {
        public MyException2() { }
        public MyException2(string message) : base(message) { }
        public MyException2(string message, Exception inner) : base(message, inner) { }
        protected MyException2(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
