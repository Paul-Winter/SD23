using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace TextWindow
{
    public partial class Form1 : Form
    {
        Module DrawerModule {  get; set; }
        object Drawer;

        public Form1()
        {
            InitializeComponent();
        }

        public Form1(Module drawer, object targetWindow)
        {
            this.DrawerModule = drawer;
            this.Drawer = targetWindow;
            InitializeComponent();
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            DrawerModule.GetType("TextDrawer.Form1").GetMethod("Move").
                Invoke(Drawer, new object[] {new Point(this.Location.X, this.Location.Y), this.Width});
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DrawerModule.GetType("TextDrawer.Form1").GetMethod("SetText").
                Invoke(Drawer, new object[] {textBox1.Text});
        }
    }
}
