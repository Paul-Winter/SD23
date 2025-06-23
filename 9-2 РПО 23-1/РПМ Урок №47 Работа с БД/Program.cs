using System;
using System.Configuration;
using System.Data.SqlClient;

namespace РПМ_Урок__47_Работа_с_БД
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                // создать консольное приложение работающее с БД согласно варианта
                // реализовать базовые функции: добавление, удаление, извлечение (1-9), (изменение - 10-12)
                // ввод и вывод данных реализовать через консоль

                // Варианты БД:
                // Александрова - страховая компания
                // Антонов      - цветочный магазин
                // Духина       - ПК предприятия
                // Землянский   - vape-shop
                // Золин        - авиакассы
                // Красицкий    - сервис такси
                // Кубанов      - гостиничный комплекс
                // Лушников     - музей
                // Мамонтова    - отель
                // Метелицин    - продуктовые магазины (сеть)
                // Чавычалов    - ЖД-кассы
                // Юнусов       - театр

                Console.WriteLine("Авторы библиотеки:\n________________________________________");
                SelectAuthors();
                Console.WriteLine("\n\n");
                Console.WriteLine("Книги библиотеки:\n________________________________________");
                SelectBooks();

                Console.WriteLine("Добавить автора в библиотеку:\n________________________________________");
                //Console.Write("Введите имя автора: ");
                //string firstName = Console.ReadLine();
                Console.Write("Введите фамилию автора: ");
                string lastName = Console.ReadLine();
                SelectAuthors(lastName);
                //Console.WriteLine("Добавляем автора в БД...");
                //InsertAuthors(firstName, lastName);

                //Console.WriteLine("Добавить книгу в библиотеку:\n________________________________________");
                //Console.Write("Введите ID автора: ");
                //int authorId = Int32.Parse(Console.ReadLine());
                //Console.Write("Введите название книги: ");
                //string title = Console.ReadLine();
                //Console.Write("Введите цену книги: ");
                //int price = Int32.Parse(Console.ReadLine());
                //Console.Write("Введите количество страниц: ");
                //int pages = Int32.Parse(Console.ReadLine());
                //Console.WriteLine("Добавляем книгу в БД...");
                //InsertBooks(authorId, title, price, pages);

                //Console.WriteLine("Удалить книгу из списка:\n________________________________________");
                //Console.Write("Введите название книги: ");
                //string title = Console.ReadLine();
                //DeleteBooks(title);

                //Console.WriteLine("Удалить автора из списка:\n________________________________________");
                //Console.Write("Введите фамилию автора: ");
                //string lastName = Console.ReadLine();
                //DeleteAuthors(lastName);
            }
        }

        public static void SelectAuthors()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;

            // запрос на извлечение данных из таблицы
            try
            {
                //conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Library; Integrated Security=SSPI");

                conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["MSSQLConnString"].ConnectionString;
                //Console.WriteLine("Создан объект подключения к БД");

                conn.Open();
                //Console.WriteLine("Подключение открыто");

                cmd = new SqlCommand(@"select * from Authors", conn);
                //Console.WriteLine("Создан объект команды на запрос");

                rdr = cmd.ExecuteReader();
                //Console.WriteLine("Создан объект читателя БД");

                int line = 0;

                do
                {
                    while (rdr.Read())
                    {
                        if (line == 0)
                        {
                            for (int i = 0; i < rdr.FieldCount; i++)
                            {
                                Console.Write($"{rdr.GetName(i).ToString()}\t");
                            }
                            Console.WriteLine();
                        }
                        line++;
                        //Console.WriteLine($"Author ID: {rdr[0]}\tИмя автора: {rdr[1]}\tФамилия автора: {rdr[2]}");
                        Console.WriteLine($"{rdr[0]}\t{rdr[1]}\t{rdr[2]}");
                    }
                    Console.WriteLine("Обработано записей: " + line.ToString());

                } while (rdr.NextResult());
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                    //Console.WriteLine("Завершена работа читателя БД");
                }

                if (conn != null)
                {
                    conn.Close();
                    //Console.WriteLine("Подключение закрыто");
                }
            }
        }
        public static void SelectAuthors(string lastName)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;

            // запрос на извлечение данных из таблицы
            try
            {
                //conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Library; Integrated Security=SSPI");

                conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["MSSQLConnString"].ConnectionString;
                //Console.WriteLine("Создан объект подключения к БД");

                conn.Open();
                //Console.WriteLine("Подключение открыто");
                string sql = @"select * from Authors where LastName = ";
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@p1";
                parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
                parameter.Value = lastName;

                cmd = new SqlCommand(sql+parameter, conn);
                cmd.Parameters.Add(parameter);

                //Console.WriteLine("Создан объект команды на запрос");

                rdr = cmd.ExecuteReader();
                //Console.WriteLine("Создан объект читателя БД");

                int line = 0;

                do
                {
                    while (rdr.Read())
                    {
                        if (line == 0)
                        {
                            for (int i = 0; i < rdr.FieldCount; i++)
                            {
                                Console.Write($"{rdr.GetName(i).ToString()}\t");
                            }
                            Console.WriteLine();
                        }
                        line++;
                        //Console.WriteLine($"Author ID: {rdr[0]}\tИмя автора: {rdr[1]}\tФамилия автора: {rdr[2]}");
                        Console.WriteLine($"{rdr[0]}\t{rdr[1]}\t{rdr[2]}");
                    }
                    Console.WriteLine("Обработано записей: " + line.ToString());

                } while (rdr.NextResult());
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                    //Console.WriteLine("Завершена работа читателя БД");
                }

                if (conn != null)
                {
                    conn.Close();
                    //Console.WriteLine("Подключение закрыто");
                }
            }
        }
        public static void InsertAuthors(string firstName, string lastName)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            // запрос на добавление автора в таблицу
            try
            {
                //conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Library; Integrated Security=SSPI");

                conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["MSSQLConnString"].ConnectionString;
                //Console.WriteLine("Создан объект подключения к БД");

                conn.Open();
                //Console.WriteLine("Подключение открыто");

                string insertString = @"insert into Authors (FirstName, LastName) values ('" + firstName + "', '" + lastName + "')";
                
                cmd = new SqlCommand(insertString, conn);
                //Console.WriteLine("Создан объект команды");

                cmd.Connection = conn;
                cmd.CommandText = insertString;

                int count = cmd.ExecuteNonQuery();
                Console.WriteLine($"Запрос к БД успешно выполнен. Изменено {count} строк");
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    //Console.WriteLine("Подключение закрыто");
                }
            }
        }
        public static void DeleteAuthors(string lastName)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            // запрос на добавление автора в таблицу
            try
            {
                //conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Library; Integrated Security=SSPI");

                conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["MSSQLConnString"].ConnectionString;
                //Console.WriteLine("Создан объект подключения к БД");

                conn.Open();
                //Console.WriteLine("Подключение открыто");
                
                string deleteString = @"delete from Authors where id=(select id from Authors where LastName='" + lastName + "')";

                cmd = new SqlCommand(deleteString, conn);
                //Console.WriteLine("Создан объект команды");

                cmd.Connection = conn;
                cmd.CommandText = deleteString;

                int count = cmd.ExecuteNonQuery();
                Console.WriteLine($"Запрос к БД успешно выполнен. Изменено {count} строк");
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    //Console.WriteLine("Подключение закрыто");
                }
            }
        }

        public static void SelectBooks()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;

            // запрос на извлечение данных из таблицы
            try
            {
                //conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Library; Integrated Security=SSPI");

                conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["MSSQLConnString"].ConnectionString;
                //Console.WriteLine("Создан объект подключения к БД");

                conn.Open();
                //Console.WriteLine("Подключение открыто");

                cmd = new SqlCommand(@"select * from Books", conn);
                //Console.WriteLine("Создан объект команды на запрос");

                rdr = cmd.ExecuteReader();
                //Console.WriteLine("Создан объект читателя БД");

                int line = 0;

                do
                {
                    while (rdr.Read())
                    {
                        if (line == 0)
                        {
                            for (int i = 0; i < rdr.FieldCount; i++)
                            {
                                Console.Write($"{rdr.GetName(i).ToString()}\t");
                            }
                            Console.WriteLine();
                        }
                        line++;
                        //Console.WriteLine($"Author ID: {rdr[0]}\tИмя автора: {rdr[1]}\tФамилия автора: {rdr[2]}");
                        Console.WriteLine($"{rdr[0]}\t{rdr[1]}\t{rdr[2]}\t{rdr[3]}\t{rdr[4]}");
                    }
                    Console.WriteLine("Обработано записей: " + line.ToString());

                } while (rdr.NextResult());
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                    //Console.WriteLine("Завершена работа читателя БД");
                }

                if (conn != null)
                {
                    conn.Close();
                    //Console.WriteLine("Подключение закрыто");
                }
            }
        }
        public static void InsertBooks(int authorId, string title, int price, int pages)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["MSSQLConnString"].ConnectionString;
                //Console.WriteLine("Создан объект подключения к БД");

                conn.Open();
                //Console.WriteLine("Подключение открыто");

                string insertString = @"insert into Books (AuthorId, Title, Price, Pages) values (" + authorId + ", '" + title + "', " + price + ", " + pages + ")";

                cmd = new SqlCommand(insertString, conn);
                //Console.WriteLine("Создан объект команды");

                cmd.Connection = conn;
                cmd.CommandText = insertString;

                int count = cmd.ExecuteNonQuery();
                Console.WriteLine($"Запрос к БД успешно выполнен. Изменено {count} строк");
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    //Console.WriteLine("Подключение закрыто");
                }
            }
        }
        public static void DeleteBooks(string title)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            { 
                conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["MSSQLConnString"].ConnectionString;
                //Console.WriteLine("Создан объект подключения к БД");

                conn.Open();
                //Console.WriteLine("Подключение открыто");

                string deleteString = @"delete from Books where id=(select id from Books where title='" + title + "')";

                cmd = new SqlCommand(deleteString, conn);
                //Console.WriteLine("Создан объект команды");

                cmd.Connection = conn;
                cmd.CommandText = deleteString;

                int count = cmd.ExecuteNonQuery();
                Console.WriteLine($"Запрос к БД успешно выполнен. Изменено {count} строк");
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    //Console.WriteLine("Подключение закрыто");
                }
            }
        }
    }
}
