using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Geo
{
    public partial class Form1 : Form
    {
        private PointF[] arr;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen1 = new Pen(Color.Yellow,10);
            Brush brush1 = new SolidBrush(Color.Yellow);
            g.FillRectangle(brush1,50,50,200,100);
            g.DrawRectangle(pen1,50,50,200,100);
            pen1.Dispose();
            brush1.Dispose();

            Graphics gi = e.Graphics;
            Pen pen2 = new Pen(Color.Red,10);
            Brush brush2 = new SolidBrush(Color.Red);
            gi.FillEllipse(brush2, 200, 200, 100, 100);
            gi.DrawEllipse(pen2, 200, 200, 100, 100);
            pen2.Dispose();
            brush2.Dispose();

            Graphics go = e.Graphics;
            Pen pen3 = new Pen(Color.Blue, 10);
            Brush brush3 = new SolidBrush(Color.Blue);
            go.FillRectangle(brush3, 280, 50, 50, 50);
            go.DrawRectangle(pen3, 280, 50, 50, 50);
            pen3.Dispose();
            brush3.Dispose();

            Graphics ge = e.Graphics;
            Pen pen4 = new Pen(Color.Green, 10);
            Brush brush4 = new SolidBrush(Color.Green);
            ge.FillRectangle(brush4, 360, 120, 50,100);
            ge.DrawRectangle(pen4, 360, 120, 50,100);
            pen4.Dispose();
            brush4.Dispose();

            Graphics geo = e.Graphics;
            Pen pen5 = new Pen(Color.Blue, 10);
            Brush brush5 = new SolidBrush(Color.Blue);
            geo.FillEllipse(brush5, 400, 200, 100, 50);
            geo.DrawEllipse(pen5, 400, 200, 100, 50);
            pen5.Dispose();
            brush5.Dispose();



        }
    }
}
