using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Albert_Singelton
{
    public partial class Form1 : Form
    {
        public static Form1 _intanse1;

        private Form1()
        {
            InitializeComponent();
        }

        public static Form1 GetForm1 ( )
        {
            if (_intanse1 == null)
            {
                _intanse1 = new Form1();
            }
            return _intanse1;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2 (textBox1.Text);
            form.Show();
        }
    }
}