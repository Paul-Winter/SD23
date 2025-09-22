using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_Урок__7_GDI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            const int WIDTH = 20;
            int arcX = 10;
            int arcY = 25;
            int arcWidth = 960;
            int arcHeight = 1065;
            int arcStart = 180;
            int arcFinish = 180;

            Graphics g = e.Graphics;

            Pen penBlue = new Pen (Brushes.Blue, WIDTH);
            Pen penGreen = new Pen(Brushes.Green, WIDTH);
            Pen penRed = new Pen(Brushes.Red, WIDTH);
            Pen penYellow = new Pen(Brushes.Yellow, WIDTH);
            Pen penPurple = new Pen(Brushes.Purple, WIDTH);
            Pen penDarkBlue = new Pen(Brushes.DarkBlue, WIDTH);
            Pen penOrange = new Pen(Brushes.Orange, WIDTH);

            //g.DrawRectangle(penBlue, 50, 50, 50, 40);
            //g.DrawArc(penRed, 200, 200, 150, 150, 180, 180);

            Pen[] pens = { penRed, penOrange, penYellow, penGreen,  penBlue, penDarkBlue, penPurple };
            //Pen[] pens2 = { penPurple, penRed, penOrange, penYellow, penGreen, penBlue, penDarkBlue,  };

            /*for (int i = pens.Length - 1; i >= 3; i--)
            //for (int i = 0; i < pens.Length-1; i++)
            {
                
                //g.DrawArc(pens[i], arcX+WIDTH*i, arcY+ WIDTH*i, arcWidth- WIDTH*(i*2), (arcHeight- WIDTH*(i-5))-350, arcStart, arcFinish);
                g.DrawArc(pens[i], arcX + WIDTH * i, arcY + WIDTH * i, arcWidth - WIDTH * (i * 2), arcHeight - WIDTH * (i*2), arcStart, arcFinish);

            }*/
            for (int i = 0; i < pens.Length; i++)
            {
                g.DrawRectangle(pens[i], arcX + WIDTH * i, arcY + WIDTH * i, arcWidth - WIDTH * (i * 2), (arcHeight - 20)-WIDTH * (i + 30));

            }
            g.FillRectangle(Brushes.Silver, 0, arcWidth - WIDTH * (13 * 2), 990, 40);
        }
    }
}