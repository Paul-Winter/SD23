using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO.NET_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectStudent();
        }

        public void SelectStudent()
        {
            string connString = ConfigurationManager.ConnectionStrings["AcademyConnectionString"].ConnectionString;
            
            string selectString = "select * from Student";
            SqlConnection connection = new SqlConnection(connString);
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            DataSet dataSet = null;
            DataTable dataTable = null;

            try
            {
                cmd = new SqlCommand(selectString, connection);
                cmd.CommandType = CommandType.Text;
                connection.Open();
                reader = cmd.ExecuteReader();
                dataTable.Load(cmd.ExecuteReader());
                dataGridView1.DataSource = dataTable;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (connection != null)
                    connection.Close();
                
            }
        }

    }
}
