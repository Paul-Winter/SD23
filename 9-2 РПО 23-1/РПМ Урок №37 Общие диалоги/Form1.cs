using System;
using System.IO;
using System.Windows.Forms;

namespace РПМ_Урок__37_Общие_диалоги
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // загрузить
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files (*.*)|*.*|Text Files (*.txt)|*.txt||";
            openFileDialog.FilterIndex = 1;

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(openFileDialog.FileName);
                textBox1.Text = streamReader.ReadToEnd();
                streamReader.Close();
            }
        }
        // сохранить
        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName);
                streamWriter.Write(textBox1.Text);
                streamWriter.Close();
            }
        }
    }
}
