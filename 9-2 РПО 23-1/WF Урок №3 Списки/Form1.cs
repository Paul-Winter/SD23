using System;
using System.Windows.Forms;

namespace WF_Урок__3_Списки
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label2.Text = trackBar1.Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = checkedListBox2.Items.Count;

            if (count < trackBar1.Value)
            {
                textBox1.Text = textBox1.Text.Trim(' ');
                if (!string.IsNullOrEmpty(textBox1.Text) && textBox1.Text != " ")
                {
                    checkedListBox2.Items.Add(textBox1.Text);
                    textBox1.Clear();
                }
                else
                {
                    MessageBox.Show("Ошибка");
                }
            }
            else if (count < 0 || count >= trackBar1.Value)
            {
                MessageBox.Show("Вам запрещено добавлять больше элементов в список");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < checkedListBox2.CheckedItems.Count; i++)
            {
                while (checkedListBox2.CheckedIndices.Count > 0)
                {

                    checkedListBox2.Items.RemoveAt(checkedListBox2.CheckedIndices[i]);

                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            checkedListBox2.Items.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItems != null)
            {

                listBox2.Items.Remove(listBox2.SelectedItem);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value < listBox2.Items.Count + checkedListBox2.CheckedItems.Count)
            {
                MessageBox.Show("Количество элементов в Лист-боксе будет превышено при переносе что спровоцирует ошибку!");
            }
            else 
            {
                for (int i = 0; i < checkedListBox2.CheckedItems.Count; i++)
                {
                    while (checkedListBox2.CheckedIndices.Count > 0)
                    {
                        listBox2.Items.Add(checkedListBox2.CheckedItems[i]);
                        checkedListBox2.Items.RemoveAt(checkedListBox2.CheckedIndices[i]);
                    }
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if(trackBar1.Value <= checkedListBox2.Items.Count)
            {
                trackBar1.Value = checkedListBox2.Items.Count;
            }
            label2.Text = trackBar1.Value.ToString();
        }

        private void checkedListBox2_MouseLeave(object sender, EventArgs e)
        {
            double items_count = checkedListBox2.Items.Count;
            double items_count_selected = checkedListBox2.CheckedItems.Count;

            if (items_count_selected > 0 && items_count > 0)
            {
                progressBar1.Value = Convert.ToInt32((items_count_selected / items_count) * 100);
            }
            if (items_count_selected == 0)
            {
                progressBar1.Value = 0;
            }
        }

        private void checkedListBox2_MouseEnter(object sender, EventArgs e)
        {
            double items_count = checkedListBox2.Items.Count;
            double items_count_selected = checkedListBox2.CheckedItems.Count;

            if (items_count_selected > 0 && items_count > 0)
            {
                progressBar1.Value = Convert.ToInt32((items_count_selected / items_count) * 100);
            }
            if (items_count_selected == 0)
            {
                progressBar1.Value = 0;
            }
        }

        private void checkedListBox2_MouseUp(object sender, MouseEventArgs e)
        {
            double items_count = checkedListBox2.Items.Count;
            double items_count_selected = checkedListBox2.CheckedItems.Count;

            if (items_count_selected > 0 && items_count > 0)
            {
                progressBar1.Value = Convert.ToInt32((items_count_selected / items_count) * 100);
            }
            if (items_count_selected == 0)
            {
                progressBar1.Value = 0;
            }
        }

        private void listBox2_MouseEnter(object sender, EventArgs e)
        {
            double items_count = Convert.ToDouble(numericUpDown1.Value);
            double items_count_selected = listBox2.Items.Count;

            if (items_count_selected > 0 && items_count > 0)
            {
                progressBar2.Value = Convert.ToInt32((items_count_selected / items_count) * 100);
            }
            if (items_count_selected == 0)
            {
                progressBar2.Value = 0;
            }
        }

        private void listBox2_MouseLeave(object sender, EventArgs e)
        {
            double items_count = Convert.ToDouble(numericUpDown1.Value);
            double items_count_selected = listBox2.Items.Count;

            if (items_count_selected > 0 && items_count > 0)
            {
                progressBar2.Value = Convert.ToInt32((items_count_selected / items_count) * 100);
            }
            if (items_count_selected == 0)
            {
                progressBar2.Value = 0;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown1.Minimum = listBox2.Items.Count;
            
        }
    }
}
