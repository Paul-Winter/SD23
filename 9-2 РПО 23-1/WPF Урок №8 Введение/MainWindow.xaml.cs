using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Урок__8_Введение
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox2.Text))
            {
                
            }
            else
            {
                if (string.IsNullOrEmpty(textBox3.Text))
                {
                    double num1 = Convert.ToDouble(textBox1.Text);
                    double num2 = Convert.ToDouble(textBox2.Text);

                    double summa = num1 + num2;

                    textBox3.Text = summa.ToString();
                }
                else
                {
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                }
            }
        }

        private void submitButton_Click2(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox2.Text))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(textBox3.Text))
                {
                    double num1 = Convert.ToDouble(textBox1.Text);
                    double num2 = Convert.ToDouble(textBox2.Text);

                    double summa = num1 - num2;

                    textBox3.Text = summa.ToString();
                }
                else
                {
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                }
            }
        }

        private void submitButton_Click3(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox2.Text))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(textBox3.Text))
                {
                    double num1 = Convert.ToDouble(textBox1.Text);
                    double num2 = Convert.ToDouble(textBox2.Text);

                    double summa = num1 * num2;

                    textBox3.Text = summa.ToString();
                }
                else
                {
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                }
            }
        }

        private void submitButton_Click4(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox2.Text))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(textBox3.Text))
                {
                    double num1 = Convert.ToDouble(textBox1.Text);
                    double num2 = Convert.ToDouble(textBox2.Text);

                    double summa = num1 / num2;

                    textBox3.Text = summa.ToString();
                }
                else
                {
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                }
            }
        }
    }
}
