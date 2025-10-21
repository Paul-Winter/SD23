using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Урок__2_Отсоединённый_режим
{
    public partial class Form1 : Form
    {
        private SqlConnection connection = null;
        private SqlDataAdapter da = null;
        private DataSet dataSet = null;
        private SqlCommandBuilder cmd = null;
        private string connStringAcademy = "";
        private string connStringCollege = "";
        private string connStringschool = "";
        List<string> tables = new List<string>();
        public Form1()
        {
            InitializeComponent();
            connStringAcademy = ConfigurationManager.ConnectionStrings["AcademyConnectionString"].ConnectionString;
            connStringCollege = ConfigurationManager.ConnectionStrings["CollegeConnectionString"].ConnectionString;
            connStringschool = ConfigurationManager.ConnectionStrings["schoolConnectionString"].ConnectionString;
            domainUpDown1.Text = "Выберите базу данных";
            domainUpDown1.Items.Add("Academy");
            domainUpDown1.Items.Add("College");
            domainUpDown1.Items.Add("School");
            domainUpDown2.Visible = false;
            domainUpDown2.Text = "Выберите таблицу";
        }
        public IList<string> ListTables()
        {
            tables.Clear();
            connection = new SqlConnection(connStringAcademy);
            if (domainUpDown1.SelectedItem == "Academy")
            {
                connection = new SqlConnection(connStringAcademy);
            }
            if (domainUpDown1.SelectedItem == "College")
            {
                connection = new SqlConnection(connStringCollege);
            }
            if (domainUpDown1.SelectedItem == "School")
            {
                connection = new SqlConnection(connStringschool);
            }
            domainUpDown2.Visible = true;

            connection.Open();
            DataTable dt = connection.GetSchema("Tables");
            foreach (DataRow row in dt.Rows)
            {
                string tablename = (string)row[2];
                tables.Add(tablename);
            }
            connection.Close();
            return tables;
        }
        private void button1_Click(object sender, EventArgs e) // UPDATE
        {
            try
            {
                if (dataGridView1.Columns.Count == 0)
                {
                    MessageBox.Show("Для начала выведите таблицу!");
                }
                else
                {
                    da.Update(dataSet, domainUpDown2.SelectedItem.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Вы сменили базу данных или таблицу");
            }
        }
        private void button2_Click(object sender, EventArgs e) // FILL
        {
            try
            {
                if (domainUpDown1.SelectedItem == "Academy")
                {
                    connection = new SqlConnection(connStringAcademy);
                }
                else if (domainUpDown1.SelectedItem == "College")
                {
                    connection = new SqlConnection(connStringCollege);
                }
                else if (domainUpDown1.SelectedItem == "School")
                {
                    connection = new SqlConnection(connStringschool);
                }
                else
                {
                    MessageBox.Show("Выберите БД!");
                }
                dataSet = new DataSet();
                if (domainUpDown2.SelectedItem == null)
                {
                    MessageBox.Show("Выберите Таблицу!");
                }
                string sqlCommand = $"select * from {domainUpDown2.SelectedItem}";
                da = new SqlDataAdapter(sqlCommand, connection);
                dataGridView1.DataSource = null;
                cmd = new SqlCommandBuilder(da);
                da.Fill(dataSet, domainUpDown2.SelectedItem.ToString());
                dataGridView1.DataSource = dataSet.Tables[domainUpDown2.SelectedItem.ToString()];
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
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            domainUpDown2.Items.Clear();
            List<string> tables = (List<string>)ListTables();
            foreach (string i in tables)
            {
                domainUpDown2.Items.Add(i);
            }
        }
    }
}
