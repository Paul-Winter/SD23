using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private static Form2 instance;

        public static Form2 GetInstance()
        {
            if (instance == null)
            {
                instance = new Form2();
            }
            return instance;
        }

        private Form2()
        {
            InitializeComponent();
        }
    }
}
