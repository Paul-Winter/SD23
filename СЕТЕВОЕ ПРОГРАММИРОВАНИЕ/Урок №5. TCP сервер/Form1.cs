using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Урок__5.TCP_сервер
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        TcpListener server;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                server = new TcpListener(IPAddress.Parse(textBox1.Text),
                                         Convert.ToInt32(textBox2.Text));
                server.Start();
                Thread thread = new Thread(new ThreadStart(ThreadFunc));
                thread.IsBackground = true;
                thread.Start();
            }
            catch (SocketException se)
            {
                MessageBox.Show($"Ошибка сокета: {se.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        void ThreadFunc()
        {
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                StreamReader sr = new StreamReader(client.GetStream(), Encoding.Unicode);
                string s = sr.ReadLine();
                listBox1.Items.Add(s);
                client.Close();
                if (s.ToUpper() == "EXIT")
                {
                    server.Stop();
                    this.Close();
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (server != null)
            {
                server.Stop();
            }
        }
    }
}
