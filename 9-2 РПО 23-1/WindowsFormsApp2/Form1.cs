using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string pol = "";
            string pol1 = "";
            string prof = "";
            string prof1 = "";

            if (rd3.Checked)
            {
                pol = rd3.Text;
            }
            if (rd4.Checked)
            {
                pol1 = rd4.Text;
            }
            if (ck3.Checked)
            {
                prof = ck3.Text;
            }
            if (ck4.Checked)
            {
                prof1 = ck4.Text;
            }
            if (btn.Enabled)
            {
               
                 
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string pol = "";
            string pol1 = "";
            string prof = "";
            string prof1 = "";

            if (rd3.Checked)
            {
               pol = rd3.Text;
            }
            if (rd4.Checked)
            {
                pol1 = rd4.Text;
            }
            if(ck3.Checked)
            {
                prof = ck3.Text;
            }
            if (ck4.Checked)
            {
                prof1 = ck4.Text;
            }
            textBox1.Text = $"Пол:{pol}{pol1}\nПрофессия: {prof}{prof1}";
            fontDialog1.ShowDialog(this);
            textBox1.Font = fontDialog1.Font;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void saveFileDialog2_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }
    }
}
