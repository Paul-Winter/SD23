using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_Урок__2.Элементы_управления
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateLabel();
        }

        void CreateLabel()
        {
            Label label2 = new Label();
            label2.Location = new Point(30, 50);
            label2.Text = "Динамически созданный статичекий текст 2";

            Image image2 = Image.FromFile("C:\\Users\\User\\Desktop\\HP.jpg");
            label2.Size = new Size(400, 800);
            //label2.Image = image2;

            this.Controls.Add(label2);
        }
    }
}
