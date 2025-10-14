using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Урок__1.ADO.NET
{
    public class Student
    {
        SqlConnection connection = null;

        public Student()
        {
            string connStr = ConfigurationManager.ConnectionStrings["AcademyConnectionString"].ConnectionString;
            //connection = new SqlConnection(@"Data Source=ITSTEP-42\SQLEXPRESS; Initial Catalog=Academy; Integrated Security=SSPI;");
            connection = new SqlConnection(connStr);
        }

        string selectStudentString = @"select * from Student";
        string insertStudentString = null;
        string updateStudentString = @"update Groups set GroupName = @GroupName, Spec = @Spec, EduForm = @EduForm where id = @id";
        string deleteStudentString = @"delete from Groups where id = 1";


        public void SelectStudent()
        {
            SqlDataReader dataReader = null;

            try
            {
                // открываем соединение
                connection.Open();
                Console.WriteLine("Открыто соединение");
                Console.WriteLine("Создан объект читателя");
                SqlCommand sqlCommand = new SqlCommand(selectStudentString, connection);
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

            insertStudentString = $@"insert into Student (FirstName, LastName, BirthDay, Ticket, GroupId, TestBook) values ('{firstName}','{lastName}','{birthDay}', {ticket}, {groupName}, {testBook})";

            // запрос на добавление
            SqlCommand insertCommand = new SqlCommand();
            insertCommand.Connection = connection;
            insertCommand.CommandText = insertStudentString;

            SqlCommand insertCmd = new SqlCommand(insertStudentString, connection);
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
                updateStudentString = @"update Student set FirstName = @FirstName where id = @id";
            }
            if (ans == 2)
            {
                updateStudentString = @"update Student set LastName = @LastName where id = @id";
            }
            if (ans == 3)
            {
                updateStudentString = @"update Student set BirthDay = @BirthDay where id = @id";
            }
            if (ans == 4)
            {
                updateStudentString = @"update Student set Ticket = @Ticket where id = @id";
            }
            if (ans == 5)
            {
                updateStudentString = @"update Student set GroupId = @GroupId where id = @id";
            }
            if (ans == 6)
            {
                updateStudentString = @"update Student set TestBook = @TestBook where id = @id";
            }
            SqlCommand updateCmd = new SqlCommand(updateStudentString, connection);

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
        public void DeleteStudent()
        {
            Console.WriteLine("Введите ID:");
            int ansID = Convert.ToInt32(Console.ReadLine());


            deleteStudentString = @"delete from Student where id = @id";
            SqlCommand updateCmd = new SqlCommand(deleteStudentString, connection);
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

    }
}
