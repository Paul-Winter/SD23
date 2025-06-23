using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WP_Урок__6_Меню
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void фонToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = colorDialog1.Color;
            }
            else
            {
                this.BackColor = Color.White;
            }
        }

        private void цветШрифтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ForeColor = colorDialog1.Color;

            }
            else
            {
                this.ForeColor = Color.White;
            }


        }

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == fontDialog1.ShowDialog())
                this.Font = fontDialog1.Font;
            else
                this.Font = new Font("Times New Roman", 12);
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            label1.Text = openFileDialog1.FileName;
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            label1.Text = saveFileDialog1.FileName;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {       
               DialogResult dialogResult = MessageBox.Show("Вы уверены что хотите выйти?","Выход",MessageBoxButtons.OKCancel);
                
                if (dialogResult == DialogResult.OK)
                {
                    this.Close();
                }
                else
                {

                }
            

        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {

           

        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Находится в разработке ","Помощь", MessageBoxButtons.OK);

        }

        private void создателиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Это разработано студентами второго курса ,Академии ItTop  ", "Справка", MessageBoxButtons.OK);
        }
    }
}
