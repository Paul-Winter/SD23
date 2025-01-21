using System;

namespace РПМ_Урок__11_Инкапсуляция
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student student1 = new Student();
            student1.Show();

            student1.firstName = "Денис";
            student1.lastName = "Землянский";
            student1.group = "9/2 РПО 23/1";
            student1.Show();

            student1.Login = null;
            Console.WriteLine(student1.Login);
            student1.Password = "12345678";
            Console.WriteLine(student1.Password);
        }
    }

    public class Student
    {
        public int id;
        public string firstName;
        public string lastName;
        public string group;
        private string login;
        private string password;

        public Student()
        {
            this.firstName = "John";
            this.lastName = "Doe";
        }

        public string Password
        { 
            get => password;
            set
            {
                if (value == null || value == string.Empty)
                {
                    Console.WriteLine("Пароль не должен быть пустым!");
                }
                else
                {
                    password = value;
                }
            }
        }
        public string Login
        {
            get => login;
            set
            {
                if (value == null || value == string.Empty)
                {
                    Console.WriteLine("Запрещено использовать в качестве логина пустую строку!");
                }
                else
                {
                    login = value;
                }
            }
        }

        public void Show()
        {
            Console.WriteLine("Студент " + firstName + " " + lastName + "\n");
        }
    }
}
