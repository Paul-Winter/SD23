using System;
using System.Collections;
using System.Data.SqlClient;
using System.Security.AccessControl;

namespace Урок__1.ADO.NET
{
    internal class Program
    {
        SqlConnection connection = null;
        bool exit = false;
        // INSERT STRING
        // string insertGroups = @"insert into Groups (GroupName, Spec, EduForm) values ('9-РПО-23/1','Разработка ПО','Колледж')";
        // SELECT STRING
        string selectGroups = @"select * from Groups";
        string selectStudent = @"select * from Student";
        string selectTeacher = @"select * from Teacher";
        // UPDATE STRING
        string updateString = @"update GroupName set '9-РПО-25/1 where id = 1";
        //DELETE STRING
        string deleteString = @"delete from Groups where id = 1";


        // PROGRAM MENU
        public Program()
        {
            connection = new SqlConnection(@"Data Source=ITSTEP-42\SQLEXPRESS; Initial Catalog=Academy; Integrated Security=SSPI;");
            Console.WriteLine("Создан объект подключения");            
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
                        SelectGroup();
                        Console.WriteLine("Нажмите Enter для продолжения");
                        
                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                        }
                    }
                    else if (a == 2)
                    {
                        SelectStudent();
                        Console.WriteLine("Нажмите Enter для продолжения");

                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                        }
                    }
                    else if (a == 3)
                    {
                        SelectTeacher();
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
                        InsertGroups();
                        Console.WriteLine("Нажмите Enter для продолжения");

                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                        }
                    }
                    else if (a == 2)
                    {
                        InsertStudent();
                        Console.WriteLine("Нажмите Enter для продолжения");

                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                        }
                    }
                    else if (a == 3)
                    {
                        InsertTeacher();
                        Console.WriteLine("Нажмите Enter для продолжения");

                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                        }
                    }
                }
                else if (choice == 3)
                {

                }
                else if (choice == 4)
                {

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
            /*}

            //if (Convert.IsDBNull(dataReader[0]))
            //{*/

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
            //connection.Close();
            return Convert.ToInt32(dataReader[0]);
        }

        // SELECT FUNC
        public void SelectGroup()
        {
            SqlDataReader dataReader = null;

            try
            {
                // открываем соединение
                connection.Open();
                Console.WriteLine("Открыто соединение");
                Console.WriteLine("Создан объект читателя");
                SqlCommand sqlCommand = new SqlCommand(selectGroups, connection);
                Console.WriteLine("Создана команда на извлечение");
                dataReader = sqlCommand.ExecuteReader();
                Console.WriteLine("================================Чтение_записей:================================");
                Console.WriteLine("===============================================================================");
                while (dataReader.Read())
                {
                    Console.WriteLine($"{dataReader[0]}\t|\t{dataReader[1]}\t|\t{dataReader[2]}");
                }
                Console.WriteLine("===============================================================================");
                Console.WriteLine("=================================Конец_записей=================================");
            }
            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                }
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
        public void SelectStudent()
        {
            SqlDataReader dataReader = null;

            try
            {
                // открываем соединение
                connection.Open();
                Console.WriteLine("Открыто соединение");
                Console.WriteLine("Создан объект читателя");
                SqlCommand sqlCommand = new SqlCommand(selectStudent, connection);
                Console.WriteLine("Создана команда на извлечение");
                dataReader = sqlCommand.ExecuteReader();
                Console.WriteLine("================================Чтение_записей:================================");
                Console.WriteLine("===============================================================================");
                while (dataReader.Read())
                {
                    Console.WriteLine($"{dataReader[0]}\t|\t{dataReader[1]}\t|\t{dataReader[2]}");
                }
                Console.WriteLine("===============================================================================");
                Console.WriteLine("=================================Конец_записей=================================");
            }
            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                }
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
        public void SelectTeacher()
        {
            SqlDataReader dataReader = null;

            try
            {
                // открываем соединение
                connection.Open();
                Console.WriteLine("Открыто соединение");
                Console.WriteLine("Создан объект читателя");
                SqlCommand sqlCommand = new SqlCommand(selectTeacher, connection);
                Console.WriteLine("Создана команда на извлечение");
                dataReader = sqlCommand.ExecuteReader();
                Console.WriteLine("================================Чтение_записей:================================");
                Console.WriteLine("===============================================================================");
                while (dataReader.Read())
                {
                    Console.WriteLine($"{dataReader[0]}\t|\t{dataReader[1]}\t|\t{dataReader[2]}");
                }
                Console.WriteLine("===============================================================================");
                Console.WriteLine("=================================Конец_записей=================================");
            }
            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                }
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        // INSERT FUNC 
        /*public void InsertQuery()
        {
            // запрос на добавление
            SqlCommand insertCommand = new SqlCommand();
            insertCommand.Connection = connection;
            insertCommand.CommandText = insertString;

            SqlCommand insertCmd = new SqlCommand(insertString, connection);
            Console.WriteLine("Создана команда на добавление");

            try
            {
                // открываем соединение
                connection.Open();
                Console.WriteLine("Открыто соединение");
                // выполняем запрос
                // ExecuteNonQuery работает с запросами: INSERT, UPDATE, DELETE
                insertCmd.ExecuteNonQuery();
                Console.WriteLine("Выполнен запрос");
            }
            finally
            {
                // закрыть соединение
                connection.Close();
                Console.WriteLine("Закрыто соединение");
            }
        }
        public void InsertQuery(string insertQuery)
        {
            SqlCommand insertCommand = new SqlCommand();
            insertCommand.Connection = connection;
            insertCommand.CommandText = insertQuery;

            SqlCommand insertCmd = new SqlCommand(insertQuery, connection);
            Console.WriteLine("Создана команда на добавление");

            try
            {
                // открываем соединение
                connection.Open();
                Console.WriteLine("Открыто соединение");
                // выполняем запрос
                // ExecuteNonQuery работает с запросами: INSERT, UPDATE, DELETE
                insertCmd.ExecuteNonQuery();
                Console.WriteLine("Выполнен запрос");
            }
            finally
            {
                // закрыть соединение
                connection.Close();
                Console.WriteLine("Закрыто соединение");
            }
        }
        */
        public void InsertGroups()
        {
            Console.WriteLine("Введите название группы: ");
            string groupName = Console.ReadLine();
            Console.WriteLine("Введите специальность: ");
            string spec = Console.ReadLine();
            Console.WriteLine("Введите форму обучения: ");
            string eduForm = Console.ReadLine();

            string insertGroups = $@"insert into Groups (GroupName, Spec, EduForm) values ('{groupName}','{spec}','{eduForm}')";

            // запрос на добавление
            SqlCommand insertCommand = new SqlCommand();
            insertCommand.Connection = connection;
            insertCommand.CommandText = insertGroups;

            SqlCommand insertCmd = new SqlCommand(insertGroups, connection);
            Console.WriteLine("Создана команда на добавление");

            try
            {
                // открываем соединение
                connection.Open();
                Console.WriteLine("Открыто соединение");
                // выполняем запрос
                // ExecuteNonQuery работает с запросами: INSERT, UPDATE, DELETE
                insertCmd.ExecuteNonQuery();
                Console.WriteLine("Выполнен запрос");
            }
            finally
            {
                // закрыть соединение
                connection.Close();
                Console.WriteLine("Закрыто соединение");
            }
        }
        public void InsertStudent()
        {
            connection.Open();

            Console.WriteLine("Введите Имя студента: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Введите Фамилию студента: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Введите День рождения: ");
            string birthDay = Console.ReadLine();
            Console.WriteLine("Введите Студенческий билет: ");
            int ticket = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите Название группы: ");
            string groupName = Console.ReadLine();
            connection.Close();
            int groupId = IdChecker(groupName);
            connection.Close();
            connection.Open();
            Console.WriteLine("Введите Зачётную книжку: ");
            int testBook = Convert.ToInt32(Console.ReadLine());

            string insertStudent = $@"insert into Students (FirstName, LastName, BirthDay, Ticket, GroupId, TestBook) values ('{firstName}','{lastName}','{birthDay}', {ticket}, {groupId}, {testBook})";

            // запрос на добавление
            SqlCommand insertCommand = new SqlCommand();
            insertCommand.Connection = connection;
            insertCommand.CommandText = insertStudent;

            SqlCommand insertCmd = new SqlCommand(insertStudent, connection);
            Console.WriteLine("Создана команда на добавление");

            try
            {
                // открываем соединение
                Console.WriteLine("Открыто соединение");
                // выполняем запрос
                // ExecuteNonQuery работает с запросами: INSERT, UPDATE, DELETE
                insertCmd.ExecuteNonQuery();
                Console.WriteLine("Выполнен запрос");
            }
            finally
            {
                // закрыть соединение
                connection.Close();
                Console.WriteLine("Закрыто соединение");
            }
        }
        public void InsertTeacher()
        {
            Console.WriteLine("Введите Имя учителя: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Введите Фамилию учителя: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Введите Первый рабочий день: ");
            string workDay = Console.ReadLine();
            Console.WriteLine("Введите Категорию: ");
            string category = Console.ReadLine();

            string insertTeacher = $@"insert into Students (FirstName, LastName, WorkDay, Category) values ('{firstName}','{lastName}','{workDay}', '{category}')";

            // запрос на добавление
            SqlCommand insertCommand = new SqlCommand();
            insertCommand.Connection = connection;
            insertCommand.CommandText = insertTeacher;

            SqlCommand insertCmd = new SqlCommand(insertTeacher, connection);
            Console.WriteLine("Создана команда на добавление");

            try
            {
                // открываем соединение
                connection.Open();
                Console.WriteLine("Открыто соединение");
                // выполняем запрос
                // ExecuteNonQuery работает с запросами: INSERT, UPDATE, DELETE
                insertCmd.ExecuteNonQuery();
                Console.WriteLine("Выполнен запрос");
            }
            finally
            {
                // закрыть соединение
                connection.Close();
                Console.WriteLine("Закрыто соединение");
            }
        }

        static void Main(string[] args)
        {
            Program pr = new Program();
            //pr.SelectQuery();
            //pr.InsertQuery(@"insert into Groups (GroupName, Spec, EduForm) values ('ДВ-311','Графический дизайн','СТ-1')");

            pr.mainMenu();
        }
    }
}
