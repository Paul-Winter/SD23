using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace РПМ_Урок__39_Игровое_приложение
{
    public partial class Form1 : Form
    {
        const int SPEED = 10;
        //const int CADR_TIMEOUT = 150;
        static int count = 0;
        static Random random;
        static bool isCross = false;

        public Form1()
        {
            InitializeComponent();
            label1.Text = count.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }

            //while (true)
            //{
            //    if (button1.Location.Y < this.MaximumSize.Height)
            //    {
            //        // TODO: избавиться от этой строчки путём добавления в программу таймера
            //        Thread.Sleep(CADR_TIMEOUT);
            //        // TODO: запускать этот метод на событие timer.Tick
            //        this.ButtonMove(button1);
            //    }
            //    else
            //    {
            //        button1.Location = new Point(button1.Location.X, -25);
            //        this.Update();
            //    }
            //}
        }

        private void ButtonMove(Button button)
        {
            button.Location = new Point(button.Location.X, button.Location.Y + SPEED);
        }

        private bool ButtonCross(Button button1, Button button2)
        {
            // пересечение слева-направо
            if ((button1.Location.X + button1.Width > button2.Location.X) && (button1.Location.Y + button1.Height > button2.Location.Y && button1.Location.Y < button2.Location.Y + button2.Height))
            {
                return true;
            }
            // пересечение справа-налево
            else if ((button1.Location.X < button2.Location.X - button2.Width) && (button1.Location.Y + button1.Height > button2.Location.Y && button1.Location.Y < button2.Location.Y + button2.Height))
            {
                return true;
            }
            // пересечение снизу-вверх
            else if ((button1.Location.Y < button2.Location.Y + button2.Height) && (button1.Location.X + button1.Width > button2.Location.X && button1.Location.X < button2.Location.X + button2.Width))
            {
                return true;
            }
            // пересечение сверху-вниз
            else if ((button1.Location.Y + button1.Height > button2.Location.Y) && (button1.Location.X + button1.Width > button2.Location.X && button1.Location.X < button2.Location.X + button2.Width))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    button2.Location = new Point(button2.Location.X, button2.Location.Y - SPEED);
                    break;
                case Keys.A:
                    button2.Location = new Point(button2.Location.X - SPEED, button2.Location.Y);
                    break;
                case Keys.S:
                    button2.Location = new Point(button2.Location.X, button2.Location.Y + SPEED);
                    break;
                case Keys.D:
                    button2.Location = new Point(button2.Location.X + SPEED, button2.Location.Y);
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            while (!isCross)
            {
                this.ButtonMove(button1);
                isCross = this.ButtonCross(button2, button1);
                this.Update();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Graphics graphics = e.Graphics;
            Pen pen = new Pen(Brushes.Yellow, 10);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            //graphics.DrawEllipse(pen, 150, 300, 200, 100);
            //graphics.Dispose();

            Graphics graphics = e.Graphics;
            Rectangle rect = this.ClientRectangle;
            Image img = new Bitmap("image.bmp");

            graphics.DrawImage(img, rect);
            graphics.DrawLine(pen, 250, 50, 400, 250);

            Rectangle rectangle1 = new Rectangle(20, 20, 200, 40);
            Rectangle rectangle4 = new Rectangle(200, 20, 200, 40);
            Rectangle rectangle5 = new Rectangle(400, 20, 200, 40);
            Rectangle rectangle6 = new Rectangle(600, 20, 200, 40);

            Rectangle rectangle2 = new Rectangle(20, 80, 200, 40);
            Rectangle rectangle2h = new Rectangle(200, 80, 200, 40);
            Rectangle rectangle3h = new Rectangle(400, 80, 200, 40);
            Rectangle rectangle4h = new Rectangle(600, 80, 200, 40);

            Rectangle rectangle3 = new Rectangle(20, 140, 200, 40);

            LinearGradientBrush lgBrush = new LinearGradientBrush(rectangle1, Color.Red, Color.Green, 0.0f, true);
            LinearGradientBrush lgBrush4 = new LinearGradientBrush(rectangle4, Color.Green, Color.Blue, 0.0f, true);
            LinearGradientBrush lgBrush5 = new LinearGradientBrush(rectangle5, Color.Blue, Color.Red, 0.0f, true);
            LinearGradientBrush lgBrush6 = new LinearGradientBrush(rectangle6, Color.Red, Color.Green, 0.0f, true);

            HatchBrush hBrush = new HatchBrush(HatchStyle.Cross, Color.Red);
            HatchBrush hBrush2 = new HatchBrush(HatchStyle.DiagonalCross, Color.Green);
            HatchBrush hBrush3 = new HatchBrush(HatchStyle.HorizontalBrick, Color.Blue);
            HatchBrush hBrush4 = new HatchBrush(HatchStyle.DiagonalBrick, Color.White);

            TextureBrush tBrush = new TextureBrush(new Bitmap("image.bmp"));

            graphics.FillRectangle(lgBrush, rectangle1);
            graphics.FillRectangle(hBrush, rectangle2);
            graphics.FillRectangle(tBrush, rectangle3);

            graphics.FillRectangle(lgBrush4, rectangle4);
            graphics.FillRectangle(lgBrush5, rectangle5);
            graphics.FillRectangle(lgBrush6, rectangle6);

            graphics.FillRectangle(hBrush2, rectangle2h);
            graphics.FillRectangle(hBrush3, rectangle3h);
            graphics.FillRectangle(hBrush4, rectangle4h);

            graphics.Dispose();
        }
    }
}
