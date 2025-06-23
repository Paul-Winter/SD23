using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace РПМ_БД
{
    public partial class Form1 : Form
    {
        private SqlDataReader rdr;
        private DataTable table;
        private SqlConnection conn;
        string connString = "";
        SqlDataAdapter adapter;
        SqlCommandBuilder builder;
        DataSet ds;
        //редактировать, удалять и обновлять
        public Form1()
        {
            InitializeComponent();
            conn = new SqlConnection();
            connString = ConfigurationManager.ConnectionStrings["MSSQLConnString"].ConnectionString;
            conn.ConnectionString = connString;

            
            //adapter.Fill(ds);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    SqlCommand cmd = new SqlCommand();
            //    //cmd.CommandText = textBox1.Text;
            //    cmd.Connection = conn;
            //    dataGridView1.DataSource = null;
            //    conn.Open();
            //    table = new DataTable();
            //    rdr = cmd.ExecuteReader();
            //    int line = 0;

            //    do
            //    {
            //        while (rdr.Read())
            //        {
            //            if (line == 0)
            //            {
            //                for (int i = 0; i < rdr.FieldCount; i++)
            //                {
            //                    table.Columns.Add(rdr.GetName(i));
            //                }
            //                line++;
            //            }
            //            DataRow row = table.NewRow();
            //            for (int i = 0; i < rdr.FieldCount; i++)
            //            {
            //                row[i] = rdr[i];
            //            }
            //            table.Rows.Add(row);
            //        }
            //    } while (rdr.NextResult());

            //    dataGridView1.DataSource = table;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Проблем энджине капут: {ex.Message}");
            //}
            //finally
            //{
            //    if (conn != null)
            //        conn.Close();
            //    if (rdr != null)
            //        rdr.Close();
            //}


        }

        private void button2_Click(object sender, EventArgs e)
        {
            AuthorsForm authForm = new AuthorsForm();
            authForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BooksForm booksForm = new BooksForm();
            booksForm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    conn = new SqlConnection(connString);
            //    ds = new DataSet();
            //    adapter = new SqlDataAdapter(textBox1.Text, conn);
            //    dataGridView1.DataSource = null;
            //    builder = new SqlCommandBuilder(adapter);
                
            //    adapter.Fill(ds, "Book");
                
            //    dataGridView1.DataSource = ds.Tables["Book"];
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Проблема: {ex.Message}");
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            adapter.Update(ds, "Book");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(connString);
                ds = new DataSet();
                adapter = new SqlDataAdapter("Select * from Books;", conn);
                dataGridView1.DataSource = null;
                builder = new SqlCommandBuilder(adapter);

                adapter.Fill(ds, "Book");

                dataGridView1.DataSource = ds.Tables["Book"];
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Проблема: {ex.Message}");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(connString);
                ds = new DataSet();
                adapter = new SqlDataAdapter("Select * from Authors;", conn);
                dataGridView1.DataSource = null;
                builder = new SqlCommandBuilder(adapter);

                adapter.Fill(ds, "Author");

                dataGridView1.DataSource = ds.Tables["Author"];
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Проблема: {ex.Message}");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            adapter.Update(ds, "Author");
        }
    }
}
