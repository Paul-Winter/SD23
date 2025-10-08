using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Урок__1.ADO.NET;
using static Урок__1.ADO.NET.Groups;

namespace Урок_1_ADO.NET
{
    public partial class Form1 : Form
    {
        SqlConnection connection = null;
        Groups groups = new Groups();
        Student student = new Student();
        Teacher teacher = new Teacher();

        public Form1()
        {
            InitializeComponent();
            connection = new SqlConnection(@"Data Source=ITSTEP-42\SQLEXPRESS; Initial Catalog=Academy; Integrated Security=SSPI;");
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


                
            

            //dataGridView1.DataSource = dtRecords;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        
    }
}