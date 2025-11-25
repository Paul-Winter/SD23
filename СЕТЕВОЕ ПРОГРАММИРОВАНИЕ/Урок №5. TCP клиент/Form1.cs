using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Урок__5.TCP_клиент
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        TcpClient client;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(textBox1.Text),
                                                     Convert.ToInt32(textBox2.Text));
                client = new TcpClient();
                client.Connect(endPoint);
                NetworkStream stream = client.GetStream();
                byte[] buffer = Encoding.Unicode.GetBytes(textBox3.Text);
                stream.Write(buffer, 0, buffer.Length);
                client.Close();
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

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (client != null)
            {
                client.Close();
            }
        }
    }
}
