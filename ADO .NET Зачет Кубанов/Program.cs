using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_Зачет_Кубанов
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connString = ConfigurationManager.ConnectionStrings["AcademyConnectionString"].ConnectionString;
            string SelectString = @"select * from Student";
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            try
            {
                conn.Open();

                cmd = new SqlCommand(SelectString, conn);
                rdr = cmd.ExecuteReader();

                while(rdr.Read())
                {
                    //Console.WriteLine($"{rdr[0]}\t|\t {rdr[1]}\t|\t {rdr[2]}\t|\t {rdr[3]}\t|\t {rdr[4]}\t|\t");
                    Console.WriteLine($"{rdr[0]}\t|\t {rdr[1]}\t|\t {rdr[2]}\t|\t {rdr[3]}\t|\t {rdr[4]}\t|\t {rdr[5]}\t|\t {rdr[6]}\t|\t ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message );
            }
            finally
            {
                if (rdr != null)
                    rdr.Close();
                if (conn != null)
                    conn.Close();
            }
        }
    }
}
