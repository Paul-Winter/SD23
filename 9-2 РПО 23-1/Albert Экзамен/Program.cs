using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

delegate void StudentDelegate(string firstName, string lastName, string otchestvo, int age, string group, int kurs, double sredniBall);

delegate void EczamenDelegate(double ocenka);

namespace Albert_Экзамен
{
    internal class Program
    {
        static void StudentInfo(string firstName, string lastName, string otchestvo, int age, string group, int kurs, double sredniBall)
        {
            Console.WriteLine("Данные о студенте:\n");
            Console.WriteLine($"ФИО: {firstName} {lastName} {otchestvo}");
            Console.WriteLine($"Лет: {age}");
            Console.WriteLine($"Наименование группы: {group}");
            Console.WriteLine($"Курс: {kurs}");
            Console.WriteLine($"Средний балл по Разработке програмных модулей: {sredniBall}");
        }
        static void EczamenInfo(string firstName, string lastName, string otchestvo, int age, string group, int kurs, double sredniBall)
        {
            Console.WriteLine($"Ваша оценка: {sredniBall}");
        }
        static void ZachetInfo(string firstName, string lastName, string otchestvo, int age, string group, int kurs, double sredniBall)
        {
            Console.WriteLine($"У вас по зачету: {sredniBall}");
        }
        static void Main(string[] args)
        {
            StudentDelegate student1 = StudentInfo;
            
            //StudentDelegate student = EczamenInfo;

            student1("Альберт", "Кубанов", "Азрет-Алиевич", 17, "РПО/23/02", 2, 8.2);
            Console.WriteLine("========================================");
            student1 = EczamenInfo;
            student1("Альберт", "Кубанов", "Азрет-Алиевич", 17, "РПО/23/02", 2, 5);
            Console.WriteLine("========================================");
            student1 = ZachetInfo;
            student1("Альберт", "Кубанов", "Азрет-Алиевич", 17, "РПО/23/02", 2, 50);
        }
    }
}
