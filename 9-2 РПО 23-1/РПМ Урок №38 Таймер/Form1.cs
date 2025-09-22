using System;
using System.Windows.Forms;

namespace РПМ_Урок__38_Таймер
{
    public partial class Form1 : Form
    {
        public static Timer timer = new Timer();

        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;

            label2.Text = DateTime.Now.ToLongTimeString();
            timer.Tick += new System.EventHandler(ShowTime);
            timer.Interval = 500;
            timer.Start();
        }

        private void ShowTimer(object Object, EventArgs e)
        {
            timer1.Stop();
            button2.Enabled = false;
            MessageBox.Show("Таймер отработал", "Таймер");
        }

        private void ShowTime(object Object, EventArgs e)
        {
            label2.Text = DateTime.Now.ToLongTimeString();
        }

        // старт
        private void button1_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value <= 0)
            {
                MessageBox.Show("Количество секунд должно быть больше 0!");
                return;
            }
            button2.Enabled = true;
            timer1.Interval = Decimal.ToInt32(numericUpDown1.Value) * 1000;
            timer1.Start();
        }
        // стоп
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            MessageBox.Show("Таймер не успел отработать!", "Таймер");
            button2.Enabled = false;
        }
    }
}
