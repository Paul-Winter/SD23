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
    public partial class Form2 : Form
    {
        public static Form2 _intanse2;

        public Form2()
        {
            InitializeComponent();
        }

        public static Form2 GetForm2()
        {
            if ( _intanse2 == null)
            {
                _intanse2 = new Form2();
            }
            return _intanse2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        public Form2(string parametr)
        {
            InitializeComponent();

            textBox1 = new TextBox();
            textBox2 = new TextBox();
        }
    }
}