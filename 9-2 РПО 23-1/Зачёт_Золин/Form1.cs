using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Зачёт_Золин
{
    public partial class Form1 : Form
    {
        private bool card = false;
        private bool password = false;
        private bool add = false;
        private bool min = false;
        private int cash = 15000;

        public Form1()
        {
            InitializeComponent();
            textBox1.Enabled = false;
        }

        // Энтер
        private void button13_Click(object sender, EventArgs e)
        {
            if (!card)
                MessageBox.Show("Вставьте карту");
            else
            {
                if (textBox1.Text == "5050" && !add && !min)
                {
                    MessageBox.Show("Вы ввели верный пароль");
                    password = true;
                    textBox1.Text = null;
                }
                else if (add)
                {
                    cash += Convert.ToInt32(textBox1.Text);
                    add = false;
                    textBox1.Text = null;
                }
                else if (min)
                {
                    int minMoney = Convert.ToInt32(textBox1.Text);
                    if (minMoney < cash)
                    {
                        cash -= Convert.ToInt32(textBox1.Text);
                        min = false;
                        textBox1.Text = null;
                    }
                    else
                    {
                        MessageBox.Show("Вы не можете снять эту сумму");
                    }
                }
                else
                {
                    MessageBox.Show("Вы ввели неверный пароль");
                }
            }
        }

        // Отмена
        private void button11_Click(object sender, EventArgs e)
        {

        }

        //Удалить
        private void button10_Click(object sender, EventArgs e)
        {
            if (!card)
                MessageBox.Show("Вставьте карту");
            else
                textBox1.Text = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!card)
                MessageBox.Show("Вставьте карту");
            else
                textBox1.Text += "1";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (!card)
                MessageBox.Show("Вставьте карту");
            else
                textBox1.Text += "2";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (!card)
                MessageBox.Show("Вставьте карту");
            else
                textBox1.Text += "3";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (!card)
                MessageBox.Show("Вставьте карту");
            else
                textBox1.Text += "4";
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (!card)
                MessageBox.Show("Вставьте карту");
            else
                textBox1.Text += "5";
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (!card)
                MessageBox.Show("Вставьте карту");
            else
                textBox1.Text += "6";
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (!card)
                MessageBox.Show("Вставьте карту");
            else
                textBox1.Text += "7";
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (!card)
                MessageBox.Show("Вставьте карту");
            else
                textBox1.Text += "8";
        }
        private void button9_Click(object sender, EventArgs e)
        {
            if (!card)
                MessageBox.Show("Вставьте карту");
            else
                textBox1.Text += "9";
        }
        private void button15_Click(object sender, EventArgs e)
        {
            if (!card)
                MessageBox.Show("Вставьте карту");
            else
                textBox1.Text += "0";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (!card)
            {
                label2.Text = "Введите Пин-код";
                card = true;
                button12.BackColor = Color.Green;
            }
            else
            {
                label2.Text = "Вы забрали карту";
                card = false;
                button12.BackColor = Color.Gray;
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (!card)
                MessageBox.Show("Вставьте карту");
            else if (!password)
                MessageBox.Show("Введите верный пароль");
            else if (card && password)
                MessageBox.Show($"Владелец карты: Золин Денис Львович\nДенег на карте: {cash} рублей", "Информация");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (!card)
                MessageBox.Show("Вставьте карту");
            else if (!password)
                MessageBox.Show("Введите верный пароль");
            else if (card && password)
            {
                label2.Text = "Введите сумму";
                add = true;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (!card)
                MessageBox.Show("Вставьте карту");
            else if (!password)
                MessageBox.Show("Введите верный пароль");
            else if (card && password)
            {
                label2.Text = "Введите сумму";
                min = true;
            }
        }
    }
}
