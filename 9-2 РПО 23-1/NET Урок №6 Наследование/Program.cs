using System;

namespace NET_Урок__6_Наследование
{
    internal class Program
    {
        // Разработать архитектуру классов иерархии товаров при разработке
        // системы управления товарами для дистрибьюторской компании.
        // Прописать члены классов.
        // Должны быть предусмотрены типы товаров, в т.ч. бытовая химия и продукты питания.
        // Предусмотреть управление товарами (пришло, реализовано, списано, передано)
        // с помощью реализации соответствующих интерфейсов.

        static void Main(string[] args)
        {
            //Human human = new Human("Пётр", "Безымянный");          
            Human employee = new Employee("Иван", "Бесфамильный", 23456.78);
            Human tutor = new Tutor("Пётр", "Безымянный");

            //Human[] humans = { human, employee, tutor };
            Human[] humans = { employee, tutor };

            foreach (Human h in humans)
            {
                //h.Show();
                Console.WriteLine(h);
            }
        }
    }

    public abstract class Human
    {
        int id;
        protected string firstName;
        protected string lastName;
        protected DateTime birthDate;

        public Human()
        {
            //Console.WriteLine("Вызов конструктора Human");
        }

        public Human(string firstName, string lastName)
        {
            //Console.WriteLine("Вызов конструктора Human");
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public virtual void Show()
        {
            Console.WriteLine($"\nФамилия: {lastName}\nИмя: {firstName}");
        }

        public override string ToString()
        {
            return $"\nФамилия: {lastName}\nИмя: {firstName}";
        }
    }

    public sealed class Tutor : Human
    {
        public Tutor()
        {
           
        }

        public Tutor(string firstName, string lastName) : base(firstName, lastName)
        {
            
        }
    }

    //public class Curator : Tutor {}

    public class Employee : Human
    {
        double salary;
        public Employee(string firstName, string lastName, double salary) : base(firstName, lastName)
        {
            //Console.WriteLine("Вызов конструктора Employee");
            //this.firstName = firstName;
            //this.lastName = lastName;
            //base.birthDate = new DateTime();
            this.salary = salary;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Зарплата: {salary}");
        }

        public override string ToString()
        {
            return $"First name: {firstName};\nLast name: {lastName};\nSalary: {salary}$";
        }
    }
    // Александрова -   теплицы
    // Антонов      -   здания и сооружения
    // Духина       -   компания-разработчик
    // Землянский   -   ВУЗы
    // Золин        -   игорные заведения
    // Красицкий    -   медицина
    // Кубанов      -   С/Х строения
    // Лушников     -   здания общепита
    // Мамонтова    -   отели
    // Метелицин    -   общежитие
    // Чавычалов    -   учебные заведения
    // Юнусов       -   автомобили
}
