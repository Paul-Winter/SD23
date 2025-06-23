using System;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //DialogResult dialogResult = colorDialog1.ShowDialog();
            //if (dialogResult == DialogResult.OK)
            //{
            //    button1.BackColor = colorDialog1.Color;
            //}
            
            Form2.GetInstance()+
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
