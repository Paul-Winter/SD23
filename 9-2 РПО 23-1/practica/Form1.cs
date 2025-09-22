using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practica
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
          


        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.numericUpDown1.Value = trackBar1.Value;
     
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
