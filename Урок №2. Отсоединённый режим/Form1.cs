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

namespace Урок__2_Отсоединённый_режим
{
    public partial class Form1 : Form
    {
        private SqlConnection connection = null;
        private SqlDataAdapter da = null;
        private DataSet dataSet = null;
        private SqlCommandBuilder cmd = null;
        private string connString = "";

        public Form1()
        {
            InitializeComponent();
            connString = ConfigurationManager.ConnectionStrings["AcademyConnectionString"].ConnectionString;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            da.Update(dataSet, "Groups");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connString);
                dataSet = new DataSet();
                string sqlCommand = textBox1.Text;
                da = new SqlDataAdapter(sqlCommand, connection);
                dataGridView1.DataSource = null;
                cmd = new SqlCommandBuilder(da);
                da.Fill(dataSet, "Groups");
                dataGridView1.DataSource = dataSet.Tables["Groups"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!");
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
