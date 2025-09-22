using System;
using System.Windows.Forms;

namespace РПМ_Урок__36_Дополнительные_формы
{
    public partial class Parent : Form
    {
        public Parent()
        {
            InitializeComponent();
        }

        Tovar tovar = null;

        // добавить
        private void button3_Click(object sender, EventArgs e)
        {
            tovar = new Tovar();
            Child addForm = new Child(tovar, true);

            if (addForm.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Add(tovar);
            }
        }
        // редактировать
        private void button4_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            tovar = (Tovar)listBox1.Items[index];
            Child editForm = new Child(tovar, false);
            editForm.ShowDialog();
            listBox1.Items.RemoveAt(index);
            listBox1.Items.Insert(index, tovar);
            listBox1.SelectedIndex = index;
        }
        // удалить
        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }
        // очистить
        private void button1_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
