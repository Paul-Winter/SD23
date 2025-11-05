using System;
using System.Drawing;
using System.Windows.Forms;

namespace TextDrawer
{
    public partial class Form1 : Form
    {
        string sourceText = "Текст ещё не был добавлен";
        Font drawingFont;

        public Form1()
        {
            InitializeComponent();
            drawingFont = new Font("Arial", 45);
            panel1.Paint += Panel1_Paint;
            this.Paint += Form1_Paint;
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            if (!String.IsNullOrEmpty(sourceText))
            {
                Image img = new Bitmap(panel1.ClientRectangle.Width, panel1.ClientRectangle.Height);
                Graphics gr = Graphics.FromImage(img);
                gr.Clear(BackColor);
                gr.DrawString(sourceText, drawingFont, Brushes.Brown, ClientRectangle,
                    new StringFormat(StringFormatFlags.NoFontFallback));
                e.Graphics.DrawImage(img, 0, 0);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Panel1_Paint(panel1, new PaintEventArgs(panel1.CreateGraphics(), panel1.ClientRectangle));
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.Font = drawingFont;
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                drawingFont = fontDialog.Font;
                Panel1_Paint(panel1, new PaintEventArgs(panel1.CreateGraphics(), panel1.ClientRectangle));
            }
        }

        public void SetText(string text)
        {
            sourceText = text;
            Panel1_Paint(panel1, new PaintEventArgs(panel1.CreateGraphics(), panel1.ClientRectangle));
        }

        public void Move(Point newLocation, int width)
        {
            this.Location = newLocation;
            this.Width = width;
        }
    }
}
