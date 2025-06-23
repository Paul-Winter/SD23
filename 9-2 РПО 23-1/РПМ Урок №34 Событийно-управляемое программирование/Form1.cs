using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace РПМ_Урок__34_Событийно_управляемое_программирование
{
    public partial class Form1 : Form
    {
        public string but1 = "кнопка 1";
        public string but2 = "кнопка 2";
        public static Random random = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button2.Visible)
                button2.Visible = false;
            else
                button2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button1.Visible)
                button1.Visible = false;
            else
                button1.Visible = true;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.Location = new Point(random.Next(0, (this.Size.Width - 2 * button1.Width)), random.Next(0, (this.Size.Height - 2 * button1.Height)));
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.Location = new Point(random.Next(0, (this.Size.Width - 2 * button2.Width)), random.Next(0, (this.Size.Height - 2 * button2.Height)));
        }
    }
}
