using System;
using System.Data.SqlClient;

namespace Урок__1.ADO.NET
{
    public class Menu
    {
        bool exit = false;
        SqlConnection connection = null;
        Groups groups = new Groups();
        Student student = new Student();
        Teacher teacher = new Teacher();

        public Menu()
        {
            connection = new SqlConnection(@"Data Source=ITSTEP-42\SQLEXPRESS; Initial Catalog=Academy; Integrated Security=SSPI;");
        }

        public void mainMenu()
        {
            Console.Clear();
            while (!exit)
            {
                Console.WriteLine("Выберите действие:\n" +
                                  "(1) Просмотр таблиц\n" +
                                  "(2) Добавление в таблицу\n" +
                                  "(3) Изменение таблицы\n" +
                                  "(4) Удаление из таблицы\n" +
                                  "(5) Выход\n");
                int choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 1)
                {
                    Console.Clear();
                    int a = userChoice();
                    if (a == 1)
                    {
                        groups.SelectGroup();
                        Console.WriteLine("Нажмите Enter для продолжения");

                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                        }
                    }
                    else if (a == 2)
                    {
                        student.SelectStudent();
                        Console.WriteLine("Нажмите Enter для продолжения");

                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                        }
                    }
                    else if (a == 3)
                    {
                        teacher.SelectTeacher();
                        Console.WriteLine("Нажмите Enter для продолжения");

                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                        }
                    }
                }
                else if (choice == 2)
                {
                    Console.Clear();
                    int a = userChoice();
                    if (a == 1)
                    {
                        groups.InsertGroups();
                        Console.WriteLine("Нажмите Enter для продолжения");

                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                        }
                    }
                    else if (a == 2)
                    {
                        student.InsertStudent();
                        Console.WriteLine("Нажмите Enter для продолжения");

                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                        }
                    }
                    else if (a == 3)
                    {
                        teacher.InsertTeacher();
                        Console.WriteLine("Нажмите Enter для продолжения");

                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                        }
                    }
                }
                else if (choice == 3)
                {
                    Console.WriteLine("Выберите таблицу:");
                    Console.WriteLine("1.Groups");
                    Console.WriteLine("2.Student");
                    Console.WriteLine("3.Teacher");
                    int answ = Convert.ToInt32(Console.ReadLine());
                    switch (answ)
                    {
                        case 1:
                            groups.UpdateGroups();
                            break;

                        case 2:
                            student.UpdateStudent();
                            break;

                        case 3:
                            teacher.UpdateTeacher();
                            break;

                    }

                }
                else if (choice == 4)
                {
                    Console.WriteLine("Выберите таблицу:");
                    Console.WriteLine("1.Groups");
                    Console.WriteLine("2.Student");
                    Console.WriteLine("3.Teacher");
                    int answ = Convert.ToInt32(Console.ReadLine());
                    switch (answ)
                    {
                        case 1:
                            groups.DeleteGroups();
                            break;

                        case 2:
                            student.DeleteStudent();
                            break;

                        case 3:
                            teacher.DeleteTeacher();
                            break;

                    }
                }
                else if (choice == 5)
                {
                    Console.Clear();
                    Console.WriteLine("До свидания :)");
                    exit = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Вы ввели неверное число. Повторите попытку");
                }
            }
        }
        public int userChoice()
        {
            Console.WriteLine("Выбор таблицы:\n" +
                              "(1) Groups\n" +
                              "(2) Student\n" +
                              "(3) Teacher\n");
            int choice = Convert.ToInt32(Console.ReadLine());
            bool notRight = true;

            while (notRight)
            {
                if (choice == 1)
                {
                    return 1;
                }
                else if (choice == 2)
                {
                    return 2;
                }
                else if (choice == 3)
                {
                    return 3;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Вы ввели неверное число. Повторите попытку");
                }
            }
            return 0;
        }
        public int IdChecker(string groupName)
        {
            SqlDataReader dataReader = null;
            string selectString = $@"select id from Groups where exists (select * from Groups where GroupName = '{groupName}')";

            connection.Open();
            SqlCommand sqlCommand = new SqlCommand(selectString, connection);
            dataReader = sqlCommand.ExecuteReader();

            if (dataReader.Read())
            {
                connection.Close();


                string insertString = $@"insert into Groups (GroupName, Spec, EduForm) values ('{groupName}','Неизвестно','Неизвестно')";

                SqlCommand insertCommand = new SqlCommand();
                insertCommand.Connection = connection;
                insertCommand.CommandText = insertString;

                SqlCommand insertCmd = new SqlCommand(insertString, connection);

                try
                {
                    connection.Open();
                    Console.WriteLine("Открыто соединение");
                    insertCmd.ExecuteNonQuery();
                    Console.WriteLine("Выполнен запрос");
                }
                finally
                {
                    Console.WriteLine("Закрыто соединение");
                    connection.Close();
                }
            }

            if (connection != null)
            {
                connection.Close();
            }

            connection.Open();
            dataReader = sqlCommand.ExecuteReader();
            connection.Close();
            return Convert.ToInt32(dataReader[0]);
        }
    }
}
