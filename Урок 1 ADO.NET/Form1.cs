using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Урок__1.ADO.NET;
using static Урок__1.ADO.NET.Groups;

namespace Урок_1_ADO.NET
{
    public partial class Form1 : Form
    {
        string insertGroupsString = null;
        string connString = @"Data Source=ITSTEP-42\SQLEXPRESS; Initial Catalog=Academy; Integrated Security=SSPI;";
        string deleteGroupsString = null;
        SqlCommand command = null;
        SqlConnection connection;
        Groups groups = new Groups();
        Student student = new Student();
        Teacher teacher = new Teacher();

        public Form1()
        {
            InitializeComponent();
            connection = new SqlConnection(connString);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                dataGridView1.DataSource = groups.SelectGroup();
            }
          else if (comboBox1.SelectedIndex == 1)
            {
                dataGridView1.DataSource = student.SelectStudent();
            }
          else if (comboBox1.SelectedIndex == 2)
            {
                dataGridView1.DataSource = teacher.SelectTeacher();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                InsertGroup();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                DeleteGroups();
            }
            else if (comboBox1.SelectedIndex == 1)
            {

            }
            else if (comboBox1.SelectedIndex == 2)
            {

            }
        }

        public void InsertGroup()
        {
            SqlCommand insertCmd = new SqlCommand(insertGroupsString, connection);
                try
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        using (SqlCommand comm = new SqlCommand())
                        {
                            SqlCommand insertCommand = new SqlCommand();
                            comm.Connection = conn;
                            conn.Open();
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                insertGroupsString = $@"insert into Groups (GroupName, Spec, EduForm) values (
                                        '{dataGridView1.Rows[i].Cells["GroupName"].Value}',
                                        '{dataGridView1.Rows[i].Cells["Spec"].Value}',
                                        '{dataGridView1.Rows[i].Cells["EduForm"].Value}')";
                                comm.CommandText = insertGroupsString;
                                comm.ExecuteNonQuery();
                            }

                        
                            insertCommand.Connection = connection;
                            insertCommand.CommandText = insertGroupsString;
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }

        public void DeleteGroups()
        {
            if (dataGridView1.CurrentRow != null)
            {
                string sotrId = dataGridView1.CurrentRow.Cells["id"].Value.ToString();

                deleteGroupsString = @"delete from Groups where id = @id";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(deleteGroupsString, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", sotrId);
                        cmd.ExecuteNonQuery();
                    }
                }
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.");
            }
        }

        public void DeleteStudent()
        {
            if (dataGridView1.CurrentRow != null)
            {
                string sotrId = dataGridView1.CurrentRow.Cells["id"].Value.ToString();

                deleteGroupsString = @"delete from Student where id = @id";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(deleteGroupsString, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", sotrId);
                        cmd.ExecuteNonQuery();
                    }
                }
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.");
            }
        }

        public void DeleteTeacher()
        {
            if (dataGridView1.CurrentRow != null)
            {
                string sotrId = dataGridView1.CurrentRow.Cells["id"].Value.ToString();

                deleteGroupsString = @"delete from Teacher where id = @id";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(deleteGroupsString, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", sotrId);
                        cmd.ExecuteNonQuery();
                    }
                }
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.");
            }
        }

        /*foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
        {
            dataGridView1.Rows.RemoveAt(item.Index);
        }*/
        /*int ansID = Convert.ToInt32(Console.ReadLine());
        deleteGroupsString = @"delete from Groups where id = @id";
        SqlCommand updateCmd = new SqlCommand(deleteGroupsString, connection);
        updateCmd.Parameters.AddWithValue("@id", ansID);
        try
        {
            connection.Open();
            updateCmd.ExecuteNonQuery();
        }
        finally
        {
            connection.Close();
        }*/
    }
}
 