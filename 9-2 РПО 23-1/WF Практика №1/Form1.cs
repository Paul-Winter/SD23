using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_Практика__1
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        DialogResult result;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal numRandom;
            decimal obmen;
            
            if (numericUpDown1.Value > numericUpDown2.Value)
            {
                result = MessageBox.Show("Вы ввели число не верно,\nнужно чтобы минимальное число\nбыло больше чем максимальное",
                    "Ошибка", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    obmen = numericUpDown1.Value;
                    numericUpDown1.Value = numericUpDown2.Value;
                    numericUpDown2.Value = obmen;
                }
                
                else if(result == DialogResult.No)
                {
                    numericUpDown1.Value = 0;
                    numericUpDown2.Value = 0;
                }
            }
            numRandom = random.Next((int)numericUpDown1.Value, (int)numericUpDown2.Value);

            label3.Text = numRandom.ToString();
            //for (int i = 0; i < num1; i++)
            //{
            //    for (int j = 0; j < num2; j++)
            //    {
            //        numRandom = num2[j];
            //    }
            //}
        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {

                result = MessageBox.Show($"вы выбрали год {radioButton1.Text}",
                    "Ошибка", MessageBoxButtons.YesNo);
            }
            if (radioButton2.Checked)
            {

                result = MessageBox.Show($"вы выбрали год {radioButton2.Text}",
                    "Ошибка", MessageBoxButtons.YesNo);
            }
            if (radioButton3.Checked)
            {

                result = MessageBox.Show($"вы выбрали год {radioButton3.Text}",
                    "Ошибка", MessageBoxButtons.YesNo);
            }
            if (radioButton4.Checked)
            {

                result = MessageBox.Show($"вы выбрали год {radioButton4.Text}",
                    "Ошибка", MessageBoxButtons.YesNo);
            }
            if (radioButton5.Checked)
            {

                result = MessageBox.Show($"вы выбрали год {radioButton5.Text}",
                    "Ошибка", MessageBoxButtons.YesNo);
            }
            if (radioButton6.Checked)
            {

                result = MessageBox.Show($"вы выбрали год {radioButton6.Text}",
                    "Ошибка", MessageBoxButtons.YesNo);
            }
            if (radioButton7.Checked)
            {

                result = MessageBox.Show($"вы выбрали год {radioButton7.Text}",
                    "Ошибка", MessageBoxButtons.YesNo);
            }
            if (radioButton8.Checked)
            {

                result = MessageBox.Show($"вы выбрали год {radioButton8.Text}",
                    "Ошибка", MessageBoxButtons.YesNo);
            }
            if (radioButton9.Checked)
            {

                result = MessageBox.Show($"вы выбрали год {radioButton9.Text}",
                    "Ошибка", MessageBoxButtons.YesNo);
            }
            if (radioButton10.Checked)
            {

                result = MessageBox.Show($"вы выбрали год {radioButton10.Text}",
                    "Ошибка", MessageBoxButtons.YesNo);
            }
            if (radioButton11.Checked)
            {

                result = MessageBox.Show($"вы выбрали год {radioButton11.Text}",
                    "Ошибка", MessageBoxButtons.YesNo);
            }
            if (radioButton12.Checked)
            {

                result = MessageBox.Show($"вы выбрали год {radioButton12.Text}",
                    "Ошибка", MessageBoxButtons.YesNo);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton13.Checked)
            {
                MessageBox.Show($"вы выбрали цвет: {radioButton13.Text}");
                BackColor = Color.Red;
            }
            else if (radioButton14.Checked)
            {
                MessageBox.Show($"вы выбрали цвет: {radioButton14.Text}");
                BackColor = Color.Orange;
            }
            else if (radioButton15.Checked)
            {
                MessageBox.Show($"вы выбрали цвет: {radioButton15.Text}");
                BackColor = Color.Yellow;
            }
            else if (radioButton16.Checked)
            {
                MessageBox.Show($"вы выбрали цвет: {radioButton16.Text}");
                BackColor = Color.Green;
            }
            else if (radioButton17.Checked)
            {
                MessageBox.Show($"вы выбрали цвет: {radioButton17.Text}");
                BackColor = Color.LightBlue;
            }
            else if (radioButton18.Checked)
            {
                MessageBox.Show($"вы выбрали цвет: {radioButton18.Text}");
                BackColor = Color.DarkBlue;
            }
            else if (radioButton19.Checked)
            {
                MessageBox.Show($"вы выбрали цвет: {radioButton19.Text}");
                BackColor = Color.Purple;
            }

            


        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Логин: {maskedTextBox1.Text}, Пароль: { textBox2.Text}, Электронная почта: {maskedTextBox1}, Телефон: {maskedTextBox3.Text}");
            
            
        }
    }
}
