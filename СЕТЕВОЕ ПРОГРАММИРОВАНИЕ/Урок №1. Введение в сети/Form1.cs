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

namespace Урок__1.Введение_в_сети
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Создать сокет
            IPAddress ip = IPAddress.Parse("207.46.197.32");
            IPEndPoint endPoint = new IPEndPoint(ip, 80);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            // 2. Вызвать метод Connect сокета и передать ему объект класса EndPoint
            try
            {
                socket.Connect(endPoint);

                // 3. В случае успешного соединения - начать обмен сообщениями
                // метод Send - для отправки сообщений, Receive - для получения
                if (socket.Connected)
                {
                    string strSend = "GET\r\n\r\n";
                    socket.Send(Encoding.ASCII.GetBytes(strSend));
                    byte[] buffer = new byte[1024];
                    int length;
                    do
                    {
                        length = socket.Receive(buffer);
                        textBox1.Text += Encoding.ASCII.GetString(buffer, 0, 1);
                    } while (length > 0);
                }
                else
                {
                    MessageBox.Show("CONNECTION ERROR!!!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }
    }
}
