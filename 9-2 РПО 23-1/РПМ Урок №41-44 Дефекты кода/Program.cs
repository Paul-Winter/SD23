using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using System.Security.AccessControl;
using System.Threading;

namespace РПМ_Урок__41_44_Дефекты_кода
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Student student = new Student();
            //StudentGroup group = new StudentGroup();
            //group.AddStudent(student);
            //group.ShowStudents();

            Main mainMenu = new Main();
            bool exit = true;

            while(exit)
            {
                Console.WriteLine("-----------------------------------------Фильмотека-----------------------------------------");
                Console.WriteLine("Что вы хотите?");
                Console.WriteLine("1. Посмотреть каталог");
                Console.WriteLine("2. Оформить аренду");
                Console.WriteLine("3. Сдать фильм");
                Console.WriteLine("4. Хз");
                int choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 1)
                {
                    mainMenu.Films();
                    Console.WriteLine("\n\nЧтобы выйти нажмите любую кнопку");
                    string a = Console.ReadLine();
                    Console.Clear();
                }
                else if (choice == 2)
                {
                    Console.WriteLine("Напишите жанр фильма: ");
                    string info = Console.ReadLine();
                    mainMenu.Open(info);
                    Console.WriteLine("\n\nЧтобы выйти нажмите любую кнопку");
                    string a = Console.ReadLine();
                    Console.Clear();
                }
                else if (choice == 3)
                {
                    Console.WriteLine("Сколько дней прошло с момента аренды? ");
                    int day = Convert.ToInt32(Console.ReadLine());
                    mainMenu.Close(day);
                    Console.WriteLine("\n\nЧтобы выйти нажмите любую кнопку");
                    string a = Console.ReadLine();
                    Console.Clear();

                }
                else if (choice == 4)
                {
                    Console.WriteLine("Ну тогда гг бб");
                    exit = false;
                }
                else
                {
                    Console.WriteLine("Вы ввели неверное число =(");
                    exit = false;
                }
            }
        }
    }

    // кино прокат с системой лояльности 
    public class Arenda
    {
        private double price;
        private double bonus;
        private double rating = 0;

        public double getMovie(string information)
        {
            string info=information;
            if (string.IsNullOrEmpty(info))
            {
                Console.WriteLine("нет информации о фильме");
                return 0;
            }
            else
            {
                if(info == "Комедия")
                {
                    price = 12.5;
                    if (rating >= 4)
                    {
                        bonus = 1;
                    }
                    else if (rating >= 3.5)
                    {
                        bonus = 2;
                    }
                    else if (rating <= 3.5)
                    {
                        bonus = 4;
                    }
                    getPrice(price, bonus, info);
                    //Console.WriteLine($"Вы выбрали {info}, Бонус: {bonus},\nЦена составила: ");
                    //price -= bonus;
                    //return price;


                }
                else if (info == "Драма")
                {
                    price = 9.9;
                    if (rating >= 4)
                    {
                        bonus = 1;
                    }
                    else if (rating >= 3.5)
                    {
                        bonus = 2;
                    }
                    else if (rating <= 3.5)
                    {
                        bonus = 4;
                    }
                    getPrice(price, bonus, info);

                    //Console.WriteLine($"Вы выбрали {info}, Бонус: {bonus},\nЦена составила: ");
                    //price -= bonus;
                    //return price;
                }
                else if (info == "Триллер")
                {
                    price = 15;
                    if (rating >= 4)
                    {
                        bonus = 1;
                    }
                    else if (rating >= 3.5)
                    {
                        bonus = 2;
                    }
                    else if (rating <= 3.5)
                    {
                        bonus = 4;
                    }
                    getPrice(price, bonus, info);

                    
                    
                    
                    //Console.WriteLine(  $"Вы выбрали {info}, Бонус: {bonus},\nЦена составила: "  );
                    //price -= bonus;
                    //return price;




                }

                


                


            }
            return getPrice(price, bonus, info);
        }
        double getPrice(double price, double bonus, string info)
        {
            Console.WriteLine($"Вы выбрали {info}, Бонус: {bonus},\nЦена составила: ");
            price -= bonus;
            return price;
        }

        public double GiveMovie(int d)
        {
            int day = d;
            if (day < 0)
            {
                return bonus;
            }
            else if (day <= 3)
            {
                //switch (day)
                //{
                //    case 0:
                    bonus = 5 - day;
                            Console.WriteLine($"Время сдачи фильма: {day} дня(ей)\nВаши бонусы:{bonus} ");
                        return bonus;
                //    case 1:
                
                    //bonus = 4 - 1;
                //        //Console.WriteLine($"Время сдачи фильма: {day} дня(ей)\nБонусы не будут начислены\nВаши бонусы: ");
                //        return bonus;
                //    case 2:
                    //bonus = 3;
                //        //Console.WriteLine($"Время сдачи фильма: {day} дня(ей)\nБонусы не будут начислены\nВаши бонусы: ");
                //        return bonus;
                //    case 3:
                //        bonus = 2;
                //        //Console.WriteLine($"Время сдачи фильма: {day} дня(ей)\nБонусы не будут начислены\nВаши бонусы: ");
                //        return bonus;
                //    default:
                //        return bonus;
                //}
            
            }
            
            else if (day >= 4)
            {
                bonus += 0;
                Console.WriteLine($"Время сдачи фильма: {day} дня(ей)\nБонусы не будут начислены\nВаши бонусы: ");
                return bonus;
            }
            return 0;
        }
        
    }

    public class Movie
    {
        private string name;
        private string genre;
        private double rating;
        private double price;
        private int releaseYear;
        private int duration;

        public string Name { get => name; set => name = value; }
        public string Genre { get => genre; set => genre = value; }
        public double Rating { get => rating; set => rating = value; }
        public double Price { get => price; set => price = value; }
        public int ReleaseYear { get => releaseYear; set => releaseYear = value; }
        public int Duration { get => duration; set => duration = value; }

        public Movie() 
        {
            name = "";
            genre = "";
            rating = 0;
            price = 0;
            releaseYear = 0;
            duration = 0;
        }
    }

    public class Main
    {
        Arenda arenda = new Arenda();
        Movie movie1 = new Movie();
        Movie movie2 = new Movie();
        Movie movie3 = new Movie();

        public void Films()
        {
            // 1
            movie1.Name = "Мальчишник в Вегасе";
            movie1.Genre = "Комедия";
            movie1.Rating = 4.6;
            movie1.Price = 300;
            movie1.ReleaseYear = 2009;
            movie1.Duration = 100;
            Console.WriteLine("Фильм 1.");
            ShowInfo(movie1);
            Console.WriteLine("\n\n");

            // 2
            movie2.Name = "Война и музыка";
            movie2.Genre = "Драма";
            movie2.Rating = 3.4;
            movie2.Price = 200;
            movie2.ReleaseYear = 2024;
            movie2.Duration = 120;
            Console.WriteLine("Фильм 2.");
            ShowInfo(movie2);
            Console.WriteLine("\n\n");

            // 3
            movie3.Name = "Форсаж 1";
            movie3.Genre = "Триллер";
            movie3.Rating = 5;
            movie3.Price = 350;
            movie3.ReleaseYear = 2001;
            movie3.Duration = 100;
            Console.WriteLine("Фильм 3.");
            ShowInfo(movie3);
        }

        public void ShowInfo(Movie movie)
        {
            Console.WriteLine($"Название фильма: {movie.Name}");
            Console.WriteLine($"Жанр фильма: {movie.Genre}");
            Console.WriteLine($"Рейтинг фильма: {movie.Rating}");
            Console.WriteLine($"Цена фильма: {movie.Price}");
            Console.WriteLine($"Год выхода фильма: {movie.ReleaseYear}");
            Console.WriteLine($"Продолжительность фильма: {movie.Duration}");
        }

        private bool Film()
        {
            return true;
        }

        public void Open(string information)
        {
            bool returnTrue = Film();
            int filmCount = 1;
            if(returnTrue)
            {
                Console.WriteLine("Фильм есть в наличии");
                Console.Write($"Стоимость фильма: ");
                arenda.getMovie(information);
                Console.WriteLine("Оплата прошла успешно!");
                filmCount -= 1;
                Console.WriteLine($"Копий фильма в наличии: {filmCount}");
            }
            else
            {
                Console.WriteLine("Фильма нет в наличии");
            }
        }

        private bool FilmID()
        {
            return true;
        }

        public void Close(int d)
        {
            Arenda arenda = new Arenda();
            bool returnTrue = FilmID();
            int filmCount = 0;
            double bonus = 0;
            int day = d;
            if (returnTrue)
            {
                Console.WriteLine("Фильм успешно возвращён");
                bonus = arenda.GiveMovie(d);
                filmCount += 1;
                Console.WriteLine($"Копий фильма в наличии: {filmCount}");
            }
            else
            {
                Console.WriteLine("Фильма не из нашего магазина =(");
            }
        }
    }



    // класс данных
    public class Student
    {
        private int id;
        private string name;
        private string surname;
        private int age;
        private bool gender;
        private Address address;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public int Age { get => age; set => age = value; }
        public bool Gender { get => gender; set => gender = value; }
        public Address Address { get => address; set => address = value; }

        public Student()
        {
            Id = 0;
            Name = "John";
            Surname = "Doe";
            Age = 0;
            Gender = true;
        }

        public void ShowStudent()
        {
            Console.WriteLine("Student\nId:\t" + Id + "\nName:\t" + Name +
                                     "\nSurname:\t" + Surname + "\nAge:\t" + Age +
                                     "\nGender\t" + Gender);
            Address.ShowAddress();
        }

        //public void ShowStudentAddress()
        //{
        //    address.ShowAddress();
        //}
    }
    public class Address
    {
        private string country;
        private string city;
        private string region;
        private string street;
        private string house;

        public string Country { get => country; set => country = value; }
        public string City { get => city; set => city = value; }
        public string Region { get => region; set => region = value; }
        public string Street { get => street; set => street = value; }
        public string House { get => house; set => house = value; }

        public void ShowAddress()
        {
            Console.WriteLine("Address:\nCountry:\t" + Country + 
                                      "\nCity:\t" + City + "\nRegion:\t" + Region +
                                      "\nStreet:\t" + Street + "\nHouse:\t" + House);
        }
    }
    // большой класс
    public class StudentGroup
    {
        private List<Student> group;

        public List<Student> Group { get => group; set => group = value; }

        public StudentGroup()
        {
            Group = new List<Student>();
        }

        // "жадная" функция
        public void AddStudent(Student student)
        {
            // избыточные временные переменные
            //Student stud = new Student();
            //stud.id = student.id;
            //stud.name = student.name;
            //stud.surname = student.surname;
            //stud.age = student.age;
            //stud.gender = student.gender;

            Group.Add(student);

            //ShowStudent(student);

            // дублирование кода
            //Console.WriteLine("Add student:\n");
            //Console.WriteLine("Id:\t" + student.id);
            //Console.WriteLine("\n");
            //Console.WriteLine("Name:\t" + student.name);
            //Console.WriteLine("\n");
            //Console.WriteLine("Surname:\t" + student.surname);
            //Console.WriteLine("\n");
            //Console.WriteLine("Age:\t" + student.age);
            //Console.WriteLine("\n");
            //Console.WriteLine("Gender:\t" + student.gender);
            //Console.WriteLine("\n");
        }
        // длинный метод
        // длинный список параметров
        public void PrintStudentName(Student student)
        {
            // дублирование кода
            //Console.WriteLine("Student\n");

            // несгруппированные данные
            //Console.WriteLine("Id:\t" + studentId);
            //Console.WriteLine("\n");

            Console.WriteLine("Name:\t" + student.Name);
            Console.WriteLine("\n");
            Console.WriteLine("Surname:\t" + student.Surname);

            //Console.WriteLine("\n");
            //Console.WriteLine("Age:\t" + studentAge);
            //Console.WriteLine("\n");
            //Console.WriteLine("Gender:\t" + studentGender);
            //Console.WriteLine("\n");
        }

        public void ShowStudents()
        {
            Console.WriteLine("Group:\n");
            for (int i = 0; i < Group.Count; i++)
            {
                Console.WriteLine(Group[i].Id + " " + Group[i].Name);
            }
        }
    }
}
