using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ADO.NET_test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            string connString = ConfigurationManager.ConnectionStrings["AcademyConnectionString"].ConnectionString;
            Console.WriteLine("Создана строка подключения");
            string selectString = "select * from Student";
            Console.WriteLine("Создана строка селекта");
            conn = new SqlConnection(connString);

            try
            {
                conn.Open();
                Console.WriteLine("Подключение открыто");
                
                cmd = new SqlCommand(selectString, conn);
                rdr = cmd.ExecuteReader();
                Console.WriteLine("Создана команда ридера");

                while (rdr.Read())
                {
                    Console.WriteLine($"{rdr[0]}\t|\t{rdr[1]}\t|\t{rdr[2]}\t|");
                    Console.Write($"\t{((DateTime)rdr[3]).ToLongDateString()}\t|\t{rdr[4]}\t|\t{rdr[5]}\t|\t{rdr[6]}");
                }
                Console.WriteLine("Конец ридера");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Проблема проблема блин: {ex}");
            }
            finally
            {
                if (rdr != null)
                    rdr.Close();
                Console.WriteLine("Ридер закрыт");
                if (conn != null )
                    conn.Close();
                Console.WriteLine("Подключение закрыто");
            }*/

            SqlConnection conn = null;
            SqlCommandBuilder cmd = null;
            SqlDataAdapter adapter = null;
            DataSet dataSet = null;
            string connString = ConfigurationManager.ConnectionStrings["AcademyConnectionString"].ConnectionString;
            Console.WriteLine("Создана строка подключения");
            string selectString = "select * from Student";

            try
            {
                dataSet = new DataSet();
                adapter = new SqlDataAdapter(selectString, connString);
                cmd = new SqlCommandBuilder(adapter);
                adapter.Fill(dataSet, "Student");
                
                for (int i = 0; i < dataSet.Tables["Student"].Rows.Count; i++)
                {
                    for (int j = 0; j < dataSet.Tables["Student"].Columns.Count; j++)
                    {
                        Console.WriteLine($"{dataSet.Tables[j]} | ");
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Проблема: {ex.Message}");
            }
        }
    }
}
