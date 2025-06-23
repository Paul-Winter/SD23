using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace РПМ_Урок__35_Элементы_управления
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "" && textBox1.Text != string.Empty)
            {
                listBox1.Items.Add(textBox1.Text);
                textBox1.Text = string.Empty;
            }
        }

        private void btn_RemoveSelected_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedItems.Count > 0)
            {
                for(int i = 0; i < listBox1.SelectedItems.Count; i++)
                {
                    listBox2.Items.Add(listBox1.SelectedItems[i]);
                    listBox1.Items.Remove(listBox1.SelectedItems[i]);
                }
            }
        }

        private void btn_Clean_Click(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Step = 10;

            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = 100;
            toolStripProgressBar1.Step = 10;

            if (progressBar1.Value >= progressBar1.Maximum)
            {
                progressBar1.Value = 0;
                toolStripProgressBar1.Value = 0;

                if (progressBar1.RightToLeftLayout)
                {
                    progressBar1.RightToLeftLayout = false;
                    toolStripProgressBar1.RightToLeftLayout = true;
                }
                else
                {
                    progressBar1.RightToLeftLayout = true;
                    toolStripProgressBar1.RightToLeftLayout = false;
                }
            }
            
            toolStripStatusLabel1.Text = DateTime.Now.ToString();

            progressBar1.PerformStep();
            toolStripProgressBar1.PerformStep();
            label3.Text = "Value = " + progressBar1.Value.ToString();
            this.Update();

            // зациклить анимацию прогресс-бара
            // при достижении максимального значения - свойства прогресс-бара меняются
            // и направление отрисовки изменяется на противоположное
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void UpdateColor()
        {
            Color color = Color.FromArgb(this.trackBar1.Value, this.trackBar2.Value, this.trackBar3.Value);
            this.BackColor = color;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.UpdateColor();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            this.UpdateColor();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            this.UpdateColor();
        }
    }
}
