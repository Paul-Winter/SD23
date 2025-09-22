using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace NET_Урок__12_Файловая_система
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // вывести в консоль содержимое (папки и файлы)
            // каталога с учебными проектами
            // Александрова : .NET и C#
            // Антонов      : мобильная разработка
            // Духина       : РПМ
            // Землянский   : текущий проект
            // Золин        : C++
            // Красицкий    : .NET и С#
            // Кубанов      : мобильная разработка
            // Лушников     : С++
            // Мамонтова    : текущий проект
            // Метелицин    : РПМ
            // Чавычалов    : мобильная разработка
            // Юнусов       : .NET и C#

            //Test test = new Test { name = "Test", count = 150, capacity = 50 };
            //Console.WriteLine(test);

            // создать класс пользовательского атрибута согласно варианта
            // и продемонстрировать его работу (класс и метод)
            // Александрова : 
            // Антонов      : ветаптека: наименование, режим работы, директор, адрес, правила
            // Духина       : программный продукт: имя комп. разработчика, девиз компании, маскот компании, жанр
            // Землянский   : 
            // Золин        : 
            // Красицкий    : букмекер: наименование, уставной капитал, инвестор с контрольным пакетом, рейтинг, сумма фрибета
            // Кубанов      : строительная компания: наименование, дата создания, страна размещения, количество работников, специализация
            // Лушников     : музей: название, фамилии основателей, год основания, адрес, музейный фонд
            // Мамонтова    : сеть кинотеатров: наименование, логотип, количество филиалов
            // Метелицин    : парк развлечений: наименование, количество аттракционов, время открытия, время закрытия, валюта
            // Чавычалов    : 
            // Юнусов       : 

            Console.WriteLine("Attributes of class Employee:");
            foreach (var attribute in typeof(Employee).GetCustomAttributes())
            {
                Console.WriteLine(attribute);
            }
            Console.WriteLine("\n\nAttributes of members class Employee:");
            foreach (MemberInfo member in typeof(Employee).GetMembers())
            {
                foreach (var attribute in member.GetCustomAttributes(true))
                {
                    Console.WriteLine(attribute);
                }
            }


            // текущий каталог
            DirectoryInfo dir = new DirectoryInfo(".");
            Console.WriteLine($"Full path to the directory:\n{dir.FullName}");
            Console.WriteLine($"Time of creation:\n{dir.CreationTime}");
            Console.WriteLine("All directory files:");

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                Console.WriteLine($"{file.Name}");
            }

            // байтовые потоки
            string filePath = "test.bin";
            WriteFile(filePath);
            Console.WriteLine($"\nДанные считаные из файла: {ReadFile(filePath)}");
            Console.WriteLine("\n\n\n_____________________________________\n\n\n");

            // текстовые потоки
            filePath = "test.txt";
            WriteText(filePath);
            Console.WriteLine($"\nДанные считаные из файла: {ReadText(filePath)}");
            Console.WriteLine("\n\n\n_____________________________________\n\n\n");

            // бинарные потоки
            filePath = "test.dat";
            WriteBin(filePath);
            Console.WriteLine($"Данные считанные из файла: {ReadBin(filePath)}");
            Console.WriteLine("\n\n\n_____________________________________\n\n\n");
        }

        static void WriteFile(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate,
                FileAccess.Write, FileShare.None))
            {
                // получаем данные для записи в файл
                Console.WriteLine("Please, enter the data to write to the file:");
                string writeText = Console.ReadLine();

                // преобразуем строку в массив байт
                byte[] writeBytes = Encoding.Default.GetBytes(writeText);

                // записываем данные в файл
                fs.Write(writeBytes, 0, writeBytes.Length);
            }
        }
        static string ReadFile(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open,
                FileAccess.Read, FileShare.Read))
            {
                byte[] readBytes = new byte[(int)fs.Length];

                // считываем данные из файла
                fs.Read(readBytes, 0, readBytes.Length);

                // преобразуем байты в строку
                return Encoding.Default.GetString(readBytes);
            }
        }

        static void WriteText(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    // получаем данные для записи в файл         
                    Console.WriteLine("Please, enter the data to write to the file:");
                    string writeText = Console.ReadLine();

                    // записываем данные в файл
                    sw.WriteLine(writeText);
                    foreach (var item in writeText)
                    {
                        sw.Write($"{item} ");
                    }
                    Console.WriteLine("Data recorded!");
                }
            }
        }
        static string ReadText(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    return $"Считанные данные: {sr.ReadToEnd()}";
                }
            }
        }

        static void WriteBin(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                using (BinaryWriter bw = new BinaryWriter(fs, Encoding.Unicode))
                {
                    Console.WriteLine("Please, enter data to write to the file:");
                    string writeText = Console.ReadLine();
                    double pi = 3.1415926;
                    int number = 123456;

                    bw.Write(writeText);
                    bw.Write(pi);
                    bw.Write(number);
                }
            }
        }
        static string ReadBin(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Unicode))
                {
                    return $"Data read from th file:\n" +
                        $"{br.ReadString()}\n{br.ReadDouble()}\n{br.ReadInt32()}";
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class CoderAttribute : Attribute
    {
        string name = "Pavel";
        DateTime date = DateTime.Now;

        public CoderAttribute() { }
        public CoderAttribute(string name, string date)
        {
            try
            {
                this.name = name;
                this.date = Convert.ToDateTime(date);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override string ToString()
        {
            return $"Coder: {name}, Date: {date}";
        }
    }
        [Coder]
        public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Salary { get; set; }

            [Coder("Paul", "2025-05-25")]
            public void IncreaseWages(double wage)
            {
                Salary += wage;
            }
        }
        /*
        public class Test
        {
            public string name;
            public int capacity;
            public int count;

            public Test(string name, int capacity, int count)
            {
                this.name = name;
                this.capacity = capacity;
                this.count = count;
            }

            public Test()
            {
                this.name = "";
                this.capacity = 0;
                this.count = 0;
            }

            public override string ToString()
            {
                return $"name: {name}\tcapacity: {capacity}\tcount: {count}";
            }
        }
        */
}
