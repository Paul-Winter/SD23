using System;
using System.Windows.Forms;

namespace РПМ_Урок__36_Дополнительные_формы
{
    public partial class Child : Form
    {
        Tovar tovar;
        bool addNew;

        public Child(Tovar tovar, bool addNew)
        {
            InitializeComponent();
            this.addNew = addNew;
            this.tovar = tovar;

            if(addNew == false)
            {
                textBox1.Text = tovar.Name;
                textBox2.Text = tovar.Made_id;
                textBox3.Text = tovar.Price.ToString();
                this.Text = "Редактирование товара";
            }
            else
            {
                this.Text = "Добавление товара";
            }
        }

        // кнопка ОК        
        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
            }
            if (tovar == null)
            {
                tovar = new Tovar();
            }
            tovar.Name = textBox1.Text;
            tovar.Made_id = textBox2.Text;
            try
            {
                tovar.Price = Convert.ToDouble(textBox3.Text);
            }
            catch
            {
                MessageBox.Show("Цена указана неверно!");
            }
            this.DialogResult = DialogResult.OK;
        }
        // кнопка Cancel
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
