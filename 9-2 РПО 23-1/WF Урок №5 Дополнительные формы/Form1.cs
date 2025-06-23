using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_Урок__5_Дополнительные_формы
{
    public partial class Form1 : Form
    {
        public string MyData { get; set; }

        

        public Form1()
        {
            InitializeComponent();
            label1.Text = "C:\\Users\\User\\Downloads";
            label2.Text = colorDialog1.Color.ToString();
            label4.Text = "C:\\Users\\User\\Downloads";
            label5.Text = "C:\\Users\\User\\Downloads";
            label6.Text= colorDialog2.Color.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            folderBrowserDialog1.ShowDialog();
            label1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            button1.BackColor = colorDialog1.Color;
            button2.BackColor = colorDialog1.Color;
            button3.BackColor = colorDialog1.Color;
            button4.BackColor = colorDialog1.Color;
            button5.BackColor = colorDialog1.Color;
            button6.BackColor = colorDialog1.Color;
            button7.BackColor = colorDialog1.Color;

            label2.Text = colorDialog1.Color.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Font font = new Font("Times New Roman", 12, FontStyle.Regular);
            //label3.Font = font;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                font = fontDialog1.Font;
                label1.Font = font;
                label2.Font = font;
                label3.Font = font;
                label4.Font = font;
                label5.Font = font;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.ShowDialog();
            label4.Text = openFileDialog1.FileName;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            label5.Text = saveFileDialog1.FileName;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog(fontDialog1.Font.ToString());
            if (form2.DialogResult == DialogResult.OK)
            {
                label3.Text = form2.ReturnText;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            MyData = colorDialog1.Color.ToString();
            form3.MyData = MyData;
            form3.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            colorDialog2.ShowDialog();
            label1.ForeColor = colorDialog2.Color;
            label2.ForeColor = colorDialog2.Color;
            label3.ForeColor = colorDialog2.Color;
            label4.ForeColor = colorDialog2.Color;
            label5.ForeColor = colorDialog2.Color;
            label6.ForeColor = colorDialog2.Color;

            button1.ForeColor = colorDialog2.Color;
            button2.ForeColor = colorDialog2.Color;
            button3.ForeColor = colorDialog2.Color;
            button4.ForeColor = colorDialog2.Color;
            button5.ForeColor = colorDialog2.Color;
            button6.ForeColor = colorDialog2.Color;
            button7.ForeColor = colorDialog2.Color;
            button8.ForeColor = colorDialog2.Color;

            label6.Text = colorDialog2.Color.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(label1.ForeColor.ToString());
            form4.ShowDialog();

        }
    }
}
