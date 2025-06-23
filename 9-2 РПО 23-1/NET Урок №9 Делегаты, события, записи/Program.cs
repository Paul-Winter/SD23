using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NET_Урок__9_Делегаты__события__записи
{
    public delegate double AnonimDelegateDouble(double x, double y);
    public delegate void AnonimDelegateVoid();

    internal class Program
    {
        //public static double result = 21;

        static void Main(string[] args)
        {
            // №1
            // создать делегат вызывающий метод структуры из прошлой пары
            // вызывать этот метод с помощью созданного делегата

            // №2
            // создать групповой делегат, вызывающий не менее 3-х методов
            // структуры из прошлой пары
            // продемонстрировать работу списка вызовов этого делегата

            /* Делегаты
            Calculator calculator = new Calculator();
            CalcDelegate delAll = null;
            while (true)
            {
                Console.Write($"Первое число: {result}\n");
                //double first = Double.Parse(Console.ReadLine());

                //Console.Write("Введите знак операции: ");
                //char operation = Char.Parse(Console.ReadLine());

                Console.Write("Введите второе число: ");
                double second = Double.Parse(Console.ReadLine());

                //switch (operation)
                //{
                //    case '+':
                //        del = new CalcDelegate(calculator.Add);
                //        break;
                //    case '-':
                //        del = calculator.Sub;
                //        break;
                //    case '*':
                //        del = new CalcDelegate(Calculator.Mult);
                //        break;
                //    case '/':
                //        del = calculator.Div;
                //        break;
                //    default:
                //        throw new InvalidOperationException();
                //        break;
                //}


                CalcDelegate delAdd = Calculator.Add;
                CalcDelegate delSub = Calculator.Sub;
                CalcDelegate delMul = Calculator.Mul;
                CalcDelegate delDiv = Calculator.Div;

                delAll += delAdd;
                delAll += delMul;
                delAll += delSub;

                foreach (CalcDelegate item in delAll.GetInvocationList())
                {
                    result = item(result, second);
                    Console.WriteLine($"Результат: {result}\n");
                }

                delAll -= delMul;

                foreach (CalcDelegate item in delAll.GetInvocationList())
                {
                    result = item(result, second);
                    Console.WriteLine($"Результат: {result}\n");
                }
            }*/

            List<Student> group = new List<Student>
            {
                new Student{FirstName = "Арсений", LastName = "Антонов", BirthDate = new DateTime(2007,9,6)},
                new Student{FirstName = "Анастасия", LastName = "Духина", BirthDate = new DateTime(2006,12,22)},
                new Student{FirstName = "Денис", LastName = "Землянский", BirthDate = new DateTime(2007,9,26)},
                new Student{FirstName = "Денис", LastName = "Золин", BirthDate = new DateTime(2007,11,10)},
                new Student{FirstName = "Олег", LastName = "Лушников", BirthDate = new DateTime(2007,12,27)},
                new Student{FirstName = "Виктория", LastName = "Мамонтова", BirthDate = new DateTime(2006,4,23)},
                new Student{FirstName = "Захар", LastName = "Метелицин", BirthDate = new DateTime(2008,3,7)}                
            };
            /* События

            Teacher teacher = new Teacher();

            foreach (Student student in group)
            {
                teacher.examEvent += student.Exam;
            }

            //teacher.examEvent += group[0].Exam;
            //teacher.examEvent += group[1].Exam;
            //teacher.examEvent += group[2].Exam;
            //teacher.examEvent += group[3].Exam;
            //teacher.examEvent += group[4].Exam;
            //teacher.examEvent += group[5].Exam;
            //teacher.examEvent += group[6].Exam;

            ExamEventArgs eventArgs = new ExamEventArgs { Task = "список билетов по 'Разработке программных модулей'" };
            teacher.Exam(eventArgs);
            */

            /* Анонимные методы и лямбда-выражения
            Dispatcher dispatcher = new Dispatcher();
            dispatcher.eventDouble += (double a, double b) =>
            {
                return a + b;
            };

            double x = 5.7;
            double y = 3.2;
            Console.WriteLine($"x + y = {dispatcher.OnEventDouble(x, y)}");

            AnonimDelegateVoid voidDel = new AnonimDelegateVoid(() => { Console.WriteLine("OK!"); });
            voidDel += () => { Console.WriteLine("Bye!"); };
            voidDel();
             */

            List<Student> students = group.FindAll(s => s.BirthDate.Month >= 3 && s.BirthDate.Month <= 12);

            foreach (Student student in students)
            {
                Console.WriteLine(student);
            }
        }
    }

    public class Dispatcher
    {
        public event AnonimDelegateDouble eventDouble;
        public event AnonimDelegateVoid eventVoid;

        public double OnEventDouble(double x, double y)
        {
            if (eventDouble != null)
            {
                return eventDouble(x, y);
            }
            throw new NullReferenceException();
        }
        public void OnEventVoid()
        {
            if (eventVoid != null)
            {
                eventVoid();
            }
        }
    }

    public delegate double CalcDelegate(double x, double y);
    public class Calculator
    {
        public static double Add(double x, double y)
        {
            return x + y;
        }
        
        public static double Sub(double x, double y)
        {
            return x - y;
        }

        public static double Mul(double x, double y)
        {
            return x * y;
        }

        public static double Div(double x, double y)
        {
            if (y != 0)
                return x / y;
            throw new DivideByZeroException();
        }
    }

    public class ExamEventArgs : EventArgs
    {
        public string Task { get; set; }
    }
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public void Exam(object sender, ExamEventArgs e)
        {
            Console.WriteLine($"Студент {LastName} отвечает на билет из {e.Task}");
        }

        public override string ToString()
        {
            return $"Студент: {LastName} {FirstName}\nДата рождения: {BirthDate.ToLongDateString()}";
        }
    }
    //public delegate void ExamDelegate(string t);
    public class Teacher
    {
        //public event ExamDelegate examEvent;
        public EventHandler<ExamEventArgs> examEvent;

        public void Exam(ExamEventArgs task)
        {
            if(examEvent != null)
                examEvent(this, task);
        }
    }
}
