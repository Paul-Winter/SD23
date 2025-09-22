using System;
using System.Collections;
using System.Collections.Generic;

namespace NET_Урок__7_Интерфейсы
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Human worker = new Employee("Раб", "Рабочий");
            //Human director = new Director("Упр", "Управляющий");
            //Human worker2 = new Employee("Труд", "Трудяжный");
            //Human[] humans = new Human[] { worker, director, worker2 };
            //foreach (Human h in humans)
            //{
            //    Console.WriteLine(h);
            //}
            //Console.WriteLine("\n\n");

            //IWorker rab1 = new Employee("Раб", "Трудяжный");
            //IWorker rab2 = new Employee("Труд", "Работяга");
            //IWorker[] workers = new IWorker[] { rab1, rab2 };
            //foreach (IWorker w in workers)
            //{
            //    Console.WriteLine(w);
            //}

            //Console.WriteLine("\n\n");
            //InterfaceInheriter iobject = new InterfaceInheriter();
            //((IA)iobject).Show();
            //((IB)iobject).Show();
            //iobject.Show();

            Auditory auditory = new Auditory();
            Console.WriteLine("_______________________________Список_студентов_______________________________");
            auditory.Sort();

            foreach (Student student in auditory)
            {
                Console.WriteLine(student);
            }

            Student student1 = new Student("Никита", "Бобиков");
            Student student2 = (Student)student1.Clone();

           



            Console.WriteLine(student1);
            Console.WriteLine(student2);

            student2.LastName = "Медведев";

            Console.WriteLine(student1);
            Console.WriteLine(student2);

            StudentCard studentcard1 = new StudentCard(123, "AGF");
            StudentCard studentcard2 = (StudentCard)studentcard1.Clone();

            Console.WriteLine(studentcard1);
            Console.WriteLine(studentcard2);

            studentcard2.Number = 145;
            Console.WriteLine(studentcard2);
            Console.WriteLine(studentcard1);
        }
    }

    public interface IWorker
    {
        bool IsWorking { get; }
        string Work();
    }
    public interface IManager
    {
        List<IWorker> ListOfWorkers { get; set; }
        void Organize();
        void MakeBudget();
        void Control();
    }

    public class Human
    {
        protected string name;
        protected string surname;

        public Human(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }

        public override string ToString()
        {
            return $"Human: {surname} {name}";
        }
    }
    public class Employee : Human, IWorker
    {
        public Employee(string name, string surname) : base(name, surname)
        {
            Console.WriteLine("Создан объект работника");
        }

        public bool IsWorking
        {
            get { return true; }
        }

        public string Work()
        {
            return "Работник работает";
        }
    }
    public class Director : Human, IManager
    {
        public Director(string name, string surname) : base(name, surname)
        {
            Console.WriteLine("Создан объект руководителя");
        }

        public List<IWorker> ListOfWorkers
        {
            get { return ListOfWorkers; }
            set { ListOfWorkers = value; }
        }

        public void Control()
        {
            Console.WriteLine("Директор контролирует ситуацию");
        }

        public void MakeBudget()
        {
            Console.WriteLine("Директор формирует бюджет");
        }

        public void Organize()
        {
            Console.WriteLine("Директор организовывает");
        }
    }

    interface IA
    {
        void Show();
    }
    interface IB
    {
        void Show();
    }
    interface IC
    {
        void Show();
    }

    public class InterfaceInheriter : IA, IB, IC
    {
        public void Show()
        {
            Console.WriteLine("Interface C");
        }
        void IA.Show()
        {
            Console.WriteLine("Interface A");
        }
        void IB.Show()
        {
            Console.WriteLine("Interface B");
        }
    }

    public class StudentCard : IComparable, ICloneable
    {
        public int Number { get; set; }
        public string Series { get; set; }

        public StudentCard(int Number, string Series)
        {
           this.Number = Number;
           this.Series = Series;
        }

        public StudentCard()
        {

        }        

        public int CompareTo(object obj)
        {
            return Number.CompareTo(obj);
        }

        public override string ToString()
        {
            return $"Студенческий билет: {Series} {Number}";
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    
    public class Student : IComparable, ICloneable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public StudentCard StudentCard { get; set; }

        public Student(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;            
        }

        public Student()
        {
            
        }

        public object Clone()
        {
            Student temp = (Student)this.MemberwiseClone();
            temp.StudentCard = new StudentCard(this.StudentCard.Number, this.StudentCard.Series);
            return temp;
        }

        public int CompareTo(object obj)
        {
            return StudentCard.CompareTo((obj as StudentCard));
        }

        public override string ToString()
        {
            return $"\nСтудент {LastName} {FirstName}\nДата рождения: {BirthDate}\n{StudentCard}";
        }
    }

    public class Auditory : IEnumerable
    {
        Student[] students =
        {
            new Student {
                FirstName = "Арсений",
                LastName = "Антонов",
                BirthDate = new DateTime(2007,09,06),
                StudentCard = new StudentCard { Number = 123, Series = "ААВ" }
            },
            new Student {
                FirstName = "Альберт",
                LastName = "Кубанов",
                BirthDate = new DateTime(2008,04,28),
                StudentCard = new StudentCard { Number = 234, Series = "КАА" }
            },
            new Student {
                FirstName = "Олег",
                LastName = "Лушников",
                BirthDate = new DateTime(2007,12,27),
                StudentCard = new StudentCard { Number = 345, Series = "ЛОА" }
            },
            new Student {
                FirstName = "Денис",
                LastName = "Золин",
                BirthDate = new DateTime(2007,11,10),
                StudentCard = new StudentCard { Number = 456, Series = "ЗДЛ" }
            },
            new Student {
                FirstName = "Кирилл",
                LastName = "Красицкий",
                BirthDate = new DateTime(2006,11,02),
                StudentCard = new StudentCard { Number = 567, Series = "ККИ" }
            },
            new Student {
                FirstName = "Анастасия",
                LastName = "Духина",
                BirthDate = new DateTime(2006,12,22),
                StudentCard = new StudentCard { Number = 678, Series = "ДАА" }
            },
            new Student {
                FirstName = "Виктория",
                LastName = "Мамонтова",
                BirthDate = new DateTime(2006,04,23),
                StudentCard = new StudentCard { Number = 789, Series = "МВВ" }
            }
        };

        public IEnumerator GetEnumerator()
        {
            return students.GetEnumerator();
        }

        public void Sort()
        {
            Array.Sort(students);
        }
    }
}
