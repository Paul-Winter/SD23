using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Урок__1.Порождение_процессов
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            testProcess.StartInfo = new ProcessStartInfo("calc.exe");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            testProcess.Start();
            this.Text = testProcess.ProcessName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (testProcess.CloseMainWindow())
                MessageBox.Show("Успешное завершение");
            //testProcess.Close();
        }
    }
}
