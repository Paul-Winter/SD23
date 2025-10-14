using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Урок__1.ADO.NET
{
    public class Teacher
    {
        SqlConnection connection = null;

        public Teacher()
        {
            string connStr = ConfigurationManager.ConnectionStrings["AcademyConnectionString"].ConnectionString;
            //connection = new SqlConnection(@"Data Source=ITSTEP-42\SQLEXPRESS; Initial Catalog=Academy; Integrated Security=SSPI;");
            connection = new SqlConnection(connStr);
        }

        string selectTeacherString = @"select * from Teacher";
        string insertTeacherString = null;
        string updateTeacherString = @"update Groups set GroupName = @GroupName, Spec = @Spec, EduForm = @EduForm where id = @id";
        string deleteTeacherString = @"delete from Groups where id = 1";


        public void SelectTeacher()
        {
            SqlDataReader dataReader = null;

            try
            {
                // открываем соединение
                connection.Open();
                Console.WriteLine("Открыто соединение");
                Console.WriteLine("Создан объект читателя");
                SqlCommand sqlCommand = new SqlCommand(selectTeacherString, connection);
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
        public void InsertTeacher()
        {
            Console.WriteLine("Введите Имя учителя: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Введите Фамилию учителя: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Введите Первый рабочий день: ");
            DateTime workDay = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Введите Категорию: ");
            string category = Console.ReadLine();

            //SqlParameter paramFirstName = new SqlParameter();
            //SqlParameter paramLastName = new SqlParameter();
            //SqlParameter paramWorkDay = new SqlParameter();
            //SqlParameter paramCategory = new SqlParameter();
            //// сопоставляем с параметром в запросе
            //paramFirstName.ParameterName = "@pfn";
            //paramLastName.ParameterName = "@pln";
            //paramWorkDay.ParameterName = "@pwd";
            //paramCategory.ParameterName = "@pc";
            //// указываем тип параметра
            //paramFirstName.SqlDbType = System.Data.SqlDbType.NVarChar;
            //paramLastName.SqlDbType = System.Data.SqlDbType.NVarChar;
            //paramWorkDay.SqlDbType = System.Data.SqlDbType.DateTime;
            //paramCategory.SqlDbType = System.Data.SqlDbType.NVarChar;
            //// значение параметра
            //paramFirstName.Value = firstName;
            //paramLastName.Value = lastName;
            //paramWorkDay.Value = workDay;
            //paramCategory.Value = category;

            insertTeacherString = @"insert into Teacher (FirstName, LastName, WorkDay, Category) values (@pfn, @pln, @pwd, @pc)";

            // запрос на добавление
            SqlCommand insertCommand = new SqlCommand(insertTeacherString,connection);

            insertCommand.Parameters.AddWithValue("@pfn", firstName);
            insertCommand.Parameters.AddWithValue("@pln", lastName);
            insertCommand.Parameters.AddWithValue("@pwd", workDay);
            insertCommand.Parameters.AddWithValue("@pc", category);


            // добавляем параметры в коллекцию параметров объекта SqlCommand
            //insertCommand.Parameters.Add(paramFirstName);
            //insertCommand.Parameters.Add(paramLastName);
            //insertCommand.Parameters.Add(paramWorkDay);
            //insertCommand.Parameters.Add(paramCategory);

            //insertCommand.Connection = connection;
            //insertCommand.CommandText = insertTeacherString;

            //SqlCommand insertCmd = new SqlCommand(insertTeacherString, connection);
            //Console.WriteLine("Создана команда на добавление");

            try
            {
                // открываем соединение
                connection.Open();
                Console.WriteLine("Открыто соединение");
                // выполняем запрос
                // ExecuteNonQuery работает с запросами: INSERT, UPDATE, DELETE

                insertCommand.ExecuteNonQuery();
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
