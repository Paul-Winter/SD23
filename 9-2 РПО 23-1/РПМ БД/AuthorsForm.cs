using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace РПМ_БД
{
    public partial class AuthorsForm : Form
    {
        private SqlDataReader rdr;
        private SqlConnection conn;
        string connString = "";

        public AuthorsForm()
        {
            InitializeComponent();
            conn = new SqlConnection();
            connString = ConfigurationManager.ConnectionStrings["MSSQLConnString"].ConnectionString;
            conn.ConnectionString = connString;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "insert into Authors(FirstName, LastName) " +
                                  "values ('"+ textBox1.Text +"', '"+ textBox2.Text +"')";
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Данные добавлены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Проблем энджине капут: {ex.Message}");
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
