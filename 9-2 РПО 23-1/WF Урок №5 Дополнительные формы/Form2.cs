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
    public partial class Form2 : Form
    {
        public string ReturnText
        {
            get
            {
                return this.Text;
            }
        }

        public Form2()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(string s)
        {
            this.Text = s;
            return ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
