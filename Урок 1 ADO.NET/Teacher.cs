using System;
using System.Data;
using System.Data.SqlClient;

namespace Урок__1.ADO.NET
{
    public class Teacher
    {
        SqlConnection connection = null;

        public Teacher()
        {
            connection = new SqlConnection(@"Data Source=ITSTEP-42\SQLEXPRESS; Initial Catalog=Academy; Integrated Security=SSPI;");
        }

        string selectTeacherString = @"select * from Teacher";
        string insertTeacherString = null;
        string updateTeacherString = @"update Groups set GroupName = @GroupName, Spec = @Spec, EduForm = @EduForm where id = @id";
        string deleteTeacherString = @"delete from Groups where id = 1";


        public DataTable SelectTeacher()
        {
            SqlDataReader dataReader = null;
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = selectTeacherString; ;

                SqlDataAdapter dbAdapter = new SqlDataAdapter(cmd);

                DataTable dtRecords = new DataTable();
                dbAdapter.Fill(dtRecords);

                connection.Close();

                return dtRecords;
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

            insertTeacherString = $@"insert into Teacher (FirstName, LastName, WorkDay, Category) values ('{firstName}','{lastName}','{workDay}', '{category}')";

            // запрос на добавление
            SqlCommand insertCommand = new SqlCommand();
            insertCommand.Connection = connection;
            insertCommand.CommandText = insertTeacherString;

            SqlCommand insertCmd = new SqlCommand(insertTeacherString, connection);
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
                updateTeacherString = @"update Teacher set FirstName = @FirstName where id = @id";
            }
            if (ans == 2)
            {
                updateTeacherString = @"update Teacher set LastName = @LastName where id = @id";
            }
            if (ans == 3)
            {
                updateTeacherString = @"update Teacher set WorkDay = @WorkDay where id = @id";
            }
            if (ans == 4)
            {
                updateTeacherString = @"update Teacher set Category = @Category where id = @id";
            }
            SqlCommand updateCmd = new SqlCommand(updateTeacherString, connection);

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
        public void DeleteTeacher()
        {
            Console.WriteLine("Введите ID:");
            int ansID = Convert.ToInt32(Console.ReadLine());

            deleteTeacherString = @"delete from Teacher where id = @id";
            SqlCommand updateCmd = new SqlCommand(deleteTeacherString, connection);
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
