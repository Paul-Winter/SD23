using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Урок__1.ADO.NET
{
    public class Groups
    {
        SqlConnection connection = null;

        public Groups()
        {
            connection = new SqlConnection(@"Data Source=ITSTEP-42\SQLEXPRESS; Initial Catalog=Academy; Integrated Security=SSPI;");
        }

        string selectGroupsString = @"select * from Groups";
        string insertGroupsString = null;
        string updateGroupsString = @"update Groups set GroupName = @GroupName, Spec = @Spec, EduForm = @EduForm where id = @id";
        string deleteGroupsString = @"delete from Groups where id = 1";

        public DataTable SelectGroup()
        {
            SqlDataReader dataReader = null;
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = selectGroupsString;

                SqlDataAdapter dbAdapter = new SqlDataAdapter(cmd);

                DataTable dtRecords = new DataTable();
                dbAdapter.Fill(dtRecords);

                connection.Close();
                return dtRecords;


                /*SqlCommand sqlCommand = new SqlCommand(selectGroupsString, connection);
                Console.WriteLine("Создана команда на извлечение");
                dataReader = sqlCommand.ExecuteReader();
                Console.WriteLine("================================Чтение_записей:================================");
                Console.WriteLine("===============================================================================");
                while (dataReader.Read())
                {
                    Console.WriteLine($"{dataReader[0]}\t|\t{dataReader[1]}\t|\t{dataReader[2]}\t|\t{dataReader[3]}");
                }
                Console.WriteLine("===============================================================================");
                Console.WriteLine("=================================Конец_записей=================================");*/
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

        public void InsertGroups()
        {
            Console.WriteLine("Введите название группы: ");
            string groupName = Console.ReadLine();
            Console.WriteLine("Введите специальность: ");
            string spec = Console.ReadLine();
            Console.WriteLine("Введите форму обучения: ");
            string eduForm = Console.ReadLine();

            insertGroupsString = $@"insert into Groups (GroupName, Spec, EduForm) values ('{groupName}','{spec}','{eduForm}')";

            // запрос на добавление
            SqlCommand insertCommand = new SqlCommand();
            insertCommand.Connection = connection;
            insertCommand.CommandText = insertGroupsString;

            SqlCommand insertCmd = new SqlCommand(insertGroupsString, connection);
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
                updateGroupsString = @"update Groups set GroupName = @GroupName where id = @id";
            }
            if (ans == 2)
            {
                updateGroupsString = @"update Groups set Spec = @Spec where id = @id";
            }
            if (ans == 3)
            {
                updateGroupsString = @"update Groups set EduForm = @EduForm where id = @id";
            }
            SqlCommand updateCmd = new SqlCommand(updateGroupsString, connection);

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
        public void DeleteGroups()
        {
            Console.WriteLine("Введите ID:");
            int ansID = Convert.ToInt32(Console.ReadLine());
            deleteGroupsString = @"delete from Groups where id = @id";
            SqlCommand updateCmd = new SqlCommand(deleteGroupsString, connection);
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
