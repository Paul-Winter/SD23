using System;

namespace NET_Урок__3_Классы
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student.PrintVUZ();

            Student st1;
            st1 = new Student();
            st1.Print();
            Console.WriteLine(st1.GetMark());

            Student st2 = new Student(1, "Пётр", "Безымянный", "1");
            st2.Print();
            Console.WriteLine(st2.GetMark());

            Student st3 = new Student("Макар", "Сидоров");
            st3.Print();
            Console.WriteLine(st3.GetMark());

            MyClass.MyMethod1();
            MyClass.MyMethod2();
        }
    }

    // 1) Создать класс, описывающий студента.
    // 2) Предусмотреть поля: фамилия, имя, отчество, возраст, массив оценок (зубчатый) по:
    // - программированию
    // - системному администрированию
    // - кибербезопасности
    // 3) Добавить методы для работы с полями в т.ч.:
    // - установки оценки
    // - получения оценки
    // - получения среднего балла по заданному предмету
    // - получения данных о студенте

    internal class Student
    {
        static string VUZ = "Компьютерная академия \"ТОР\"";

        int studentID;
        string studentName;
        string studentLastName;
        string group;


        public Student(int id, string name, string lastName, string group)
        {
            studentID = id;
            studentName = name;
            studentLastName = lastName;
            this.group = group;
            Console.WriteLine("\nРаботает главный конструктор");
        }

        public Student(): this(0, "Иван", "Бесфамильный", "")
        {
            Console.WriteLine("Работает конструктор по умолчанию\n");
        }
        public Student(string name, string lastName): this(0, name, lastName, "")
        {
            Console.WriteLine("Работает параметризованный конструктор\n");
        }

        public void Print()
        {
            Console.WriteLine($"Студент: {studentID} {studentName} {studentLastName} {group}");
        }

        public static void PrintVUZ()
        {
            Console.WriteLine($"Учебное заведение: {VUZ}");
        }

        public int GetMark()
        {
            return new Random().Next(1, 13);
        }
    }

    partial class MyClass
    {
        public static void MyMethod2()
        {
            Console.WriteLine("MyClass Method_2");
        }
    }
}
