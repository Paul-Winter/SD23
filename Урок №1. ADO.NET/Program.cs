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
        string insertGroups = @"insert into Groups (GroupName, Spec, EduForm) values ('9-РПО-23/1','Разработка ПО','Колледж')";
        // SELECT STRING
        string selectGroups = @"select * from Groups";
        string selectStudent = @"select * from Student";
        string selectTeacher = @"select * from Teacher";
        // UPDATE STRING
        string updateString = @"update Groups set GroupName = @GroupName, Spec = @Spec, EduForm = @EduForm where id = @id";

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
                    Console.WriteLine("Выберите таблицу:");
                    Console.WriteLine("1.Groups");
                    Console.WriteLine("2.Student");
                    Console.WriteLine("3.Teacher");
                    int answ = Convert.ToInt32(Console.ReadLine());
                    switch(answ)
                    {
                        case 1:
                            UpdateGroups();
                            break;

                        case 2:
                            UpdateStudent();
                            break;

                        case 3:
                            UpdateTeacher();
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
                            DeleteGroups();
                            break;

                        case 2:
                            DeleteStudent();
                            break;

                        case 3:
                            DeleteTeacher();
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
            connection.Close();
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
                    Console.WriteLine($"{dataReader[0]}\t|\t{dataReader[1]}\t|\t{dataReader[2]}\t|\t{dataReader[3]}");
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
                    Console.WriteLine($"{dataReader[0]}\t|\t{dataReader[1]}\t|\t{dataReader[2]}\t|\t{((DateTime)dataReader[3]).ToLongDateString()}\t|\t{dataReader[4]}\t|\t{dataReader[5]}\t|\t{dataReader[6]}");
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
                    Console.WriteLine($"{dataReader[0]}\t|\t{dataReader[1]}\t|\t{dataReader[2]}\t|\t{((DateTime)dataReader[3]).ToLongDateString()}");
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
            //connection.Open();

            Console.WriteLine("Введите Имя студента: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Введите Фамилию студента: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Введите День рождения: ");
            string birthDay = Console.ReadLine();
            Console.WriteLine("Введите Студенческий билет: ");
            int ticket = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите ID группы: ");
            int groupName = Convert.ToInt32(Console.ReadLine());
            //connection.Close();
            //int groupId = IdChecker(groupName);
            //connection.Close();
            //connection.Open();
            Console.WriteLine("Введите Зачётную книжку: ");
            int testBook = Convert.ToInt32(Console.ReadLine());

            string insertStudent = $@"insert into Student (FirstName, LastName, BirthDay, Ticket, GroupId, TestBook) values ('{firstName}','{lastName}','{birthDay}', {ticket}, {groupName}, {testBook})";

            // запрос на добавление
            SqlCommand insertCommand = new SqlCommand();
            insertCommand.Connection = connection;
            insertCommand.CommandText = insertStudent;

            SqlCommand insertCmd = new SqlCommand(insertStudent, connection);
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

            string insertTeacher = $@"insert into Teacher (FirstName, LastName, WorkDay, Category) values ('{firstName}','{lastName}','{workDay}', '{category}')";

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

        // UPDATE FUNC
        public void UpdateGroups()
        {
            Console.WriteLine("Введите ID:");
            int ansID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Выберите необходимые данные для изменения");
            Console.WriteLine("1. Название группы");
            Console.WriteLine("2. Специальность");
            Console.WriteLine("3. Форма обучения");
            
            int ans = Convert.ToInt32(Console.ReadLine());
            if (ans == 1)
            {
                updateString = @"update Groups set GroupName = @GroupName where id = @id";
            }
            if (ans == 2)
            {
                updateString = @"update Groups set Spec = @Spec where id = @id";
            }
            if (ans == 3)
            {
                updateString = @"update Groups set EduForm = @EduForm where id = @id";
            }
            SqlCommand updateCmd = new SqlCommand(updateString, connection);

            updateCmd.Parameters.AddWithValue("@id", ansID);
            switch (ans)
            {
                case 1:
                    Console.WriteLine("Введите новое название группы:");
                    string NewGroup = Console.ReadLine();
                    updateCmd.Parameters.AddWithValue("@GroupName", NewGroup);
                    
                    break;
                   

                case 2:
                    Console.WriteLine("Введите новую специальность:");
                    string NewSpec = Console.ReadLine();
                    updateCmd.Parameters.AddWithValue("@Spec", NewSpec);
                    
                    break;

                case 3:
                    Console.WriteLine("Введите новую форму обучения:");
                    string NewForm = Console.ReadLine();
                    
                    updateCmd.Parameters.AddWithValue("@EduForm", NewForm);
                    break;

            }
            try
            {
                // открываем соединение
                connection.Open();
                Console.WriteLine("Открыто соединение");
                updateCmd.ExecuteNonQuery();
                Console.WriteLine("Выполнен запрос");
            }
            finally
            {
                // закрыть соединение
                connection.Close();
                Console.WriteLine("Закрыто соединение");
            }


        }
        public void UpdateStudent()
        {
            Console.WriteLine("Введите ID:");
            int ansID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Выберите необходимые данные для изменения");
            Console.WriteLine("1. Имя студента");
            Console.WriteLine("2. Фамилию студента");
            Console.WriteLine("3. День рождения");
            Console.WriteLine("4. Студенческий билет");
            Console.WriteLine("5. Название групп");
            Console.WriteLine("6. Зачётную книжку");

            int ans = Convert.ToInt32(Console.ReadLine());
            if (ans == 1)
            {
                updateString = @"update Student set FirstName = @FirstName where id = @id";
            }
            if (ans == 2)
            {
                updateString = @"update Student set LastName = @LastName where id = @id";
            }
            if (ans == 3)
            {
                updateString = @"update Student set BirthDay = @BirthDay where id = @id";
            }
            if (ans == 4)
            {
                updateString = @"update Student set Ticket = @Ticket where id = @id";
            }
            if (ans == 5)
            {
                updateString = @"update Student set GroupId = @GroupId where id = @id";
            }
            if (ans == 6)
            {
                updateString = @"update Student set TestBook = @TestBook where id = @id";
            }
            SqlCommand updateCmd = new SqlCommand(updateString, connection);

            updateCmd.Parameters.AddWithValue("@id", ansID);
            switch (ans)
            {
                case 1:
                    Console.WriteLine("Введите новое имя студента:");
                    string NewFName = Console.ReadLine();
                    updateCmd.Parameters.AddWithValue("@FirstName", NewFName);

                    break;


                case 2:
                    Console.WriteLine("Введите новую фамилию:");
                    string NewLName = Console.ReadLine();
                    updateCmd.Parameters.AddWithValue("@LastName", NewLName);

                    break;

                case 3:
                    Console.WriteLine("Введите новую дату рождения:");
                    string NewBirthDay = Console.ReadLine();

                    updateCmd.Parameters.AddWithValue("@BirthDay", NewBirthDay);
                    break;

                case 4:
                    Console.WriteLine("Введите новый студенческий билет:");
                    string NewTicket = Console.ReadLine();

                    updateCmd.Parameters.AddWithValue("@Ticket", NewTicket);
                    break;

                case 5:
            
                    Console.WriteLine("Введите новый студенческий билет:");
                    string NewGroupId = Console.ReadLine();

                    updateCmd.Parameters.AddWithValue("@GroupId", NewGroupId);
                    break;

                case 6:

                    Console.WriteLine("Введите новый студенческий билет:");
                    string NewTestBook = Console.ReadLine();

                    updateCmd.Parameters.AddWithValue("@TestBook", NewTestBook);
                    break;

            }
            try
            {
                // открываем соединение
                connection.Open();
                Console.WriteLine("Открыто соединение");
                updateCmd.ExecuteNonQuery();
                Console.WriteLine("Выполнен запрос");
            }
            finally
            {
                // закрыть соединение
                connection.Close();
                Console.WriteLine("Закрыто соединение");
            }

        }
        public void UpdateTeacher()
        {
            Console.WriteLine("Введите ID:");
            int ansID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Выберите необходимые данные для изменения");
            Console.WriteLine("1. Имя учителя");
            Console.WriteLine("2. Фамилия учителя");
            Console.WriteLine("3. Первый рабочий день");
            Console.WriteLine("4. Категория");

            int ans = Convert.ToInt32(Console.ReadLine());
            if (ans == 1)
            {
                updateString = @"update Teacher set FirstName = @FirstName where id = @id";
            }
            if (ans == 2)
            {
                updateString = @"update Teacher set LastName = @LastName where id = @id";
            }
            if (ans == 3)
            {
                updateString = @"update Teacher set WorkDay = @WorkDay where id = @id";
            }
            if (ans == 4)
            {
                updateString = @"update Teacher set Category = @Category where id = @id";
            }
            SqlCommand updateCmd = new SqlCommand(updateString, connection);

            updateCmd.Parameters.AddWithValue("@id", ansID);
            switch (ans)
            {
                case 1:
                    Console.WriteLine("Введите новое имя:");
                    string NewFirstName = Console.ReadLine();
                    updateCmd.Parameters.AddWithValue("@FirstName", NewFirstName);

                    break;


                case 2:
                    Console.WriteLine("Введите новую фамилию:");
                    string NewLastName = Console.ReadLine();
                    updateCmd.Parameters.AddWithValue("@LastName", NewLastName);

                    break;

                case 3:
                    Console.WriteLine("Введите новый первый рабочий день:");
                    string NewWorkDay = Console.ReadLine();

                    updateCmd.Parameters.AddWithValue("@WorkDay", NewWorkDay);
                    break;

                case 4:
                    Console.WriteLine("Введите новую категорию:");
                    string NewCategory = Console.ReadLine();

                    updateCmd.Parameters.AddWithValue("@Category", NewCategory);
                    break;


            }
            try
            {
                // открываем соединение
                connection.Open();
                Console.WriteLine("Открыто соединение");
                updateCmd.ExecuteNonQuery();
                Console.WriteLine("Выполнен запрос");
            }
            finally
            {
                // закрыть соединение
                connection.Close();
                Console.WriteLine("Закрыто соединение");
            }
        }


        //DELETE FUNC
        public void DeleteGroups()
        {
            Console.WriteLine("Введите ID:");
            int ansID = Convert.ToInt32(Console.ReadLine());
            deleteString = @"delete from Groups where id = @id";
            SqlCommand updateCmd = new SqlCommand(deleteString, connection);
            updateCmd.Parameters.AddWithValue("@id", ansID);
            try
            {
                // открываем соединение
                connection.Open();
                Console.WriteLine("Открыто соединение");
                updateCmd.ExecuteNonQuery();
                Console.WriteLine("Выполнен запрос");
            }
            finally
            {
                // закрыть соединение
                connection.Close();
                Console.WriteLine("Закрыто соединение");
            }
        }
        public void DeleteStudent()
        {
            Console.WriteLine("Введите ID:");
            int ansID = Convert.ToInt32(Console.ReadLine());


            deleteString = @"delete from Student where id = @id";
            SqlCommand updateCmd = new SqlCommand(deleteString, connection);
            updateCmd.Parameters.AddWithValue("@id", ansID);
            try
            {
                // открываем соединение
                connection.Open();
                Console.WriteLine("Открыто соединение");
                updateCmd.ExecuteNonQuery();
                Console.WriteLine("Выполнен запрос");
            }
            finally
            {
                // закрыть соединение
                connection.Close();
                Console.WriteLine("Закрыто соединение");
            }
        }
        public void DeleteTeacher()
        {
            Console.WriteLine("Введите ID:");
            int ansID = Convert.ToInt32(Console.ReadLine());

            deleteString = @"delete from Teacher where id = @id";
            SqlCommand updateCmd = new SqlCommand(deleteString, connection);
            updateCmd.Parameters.AddWithValue("@id", ansID);
            try
            {
                // открываем соединение
                connection.Open();
                Console.WriteLine("Открыто соединение");
                updateCmd.ExecuteNonQuery();
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
