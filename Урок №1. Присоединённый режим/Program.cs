using System;
using System.Collections;
using System.Data.SqlClient;
using System.Security.AccessControl;

namespace Урок__1.ADO.NET
{
    internal class Program
    {
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
        
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.mainMenu();
        }
    }
}
