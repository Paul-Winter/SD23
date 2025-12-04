using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Урок__8.Unicast_server
{
    public partial class Form1 : Form
    {
        static string message = "Hello there!!!";
        static int interval = 1000;

        static void MulticastSend()
        {
            while (true)
            {
                Thread.Sleep(interval);
                Socket socket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Dgram, ProtocolType.Udp);
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 2);
                IPAddress dest = IPAddress.Parse("224.5.5.5");
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(dest));
                IPEndPoint endPoint = new IPEndPoint(dest, 4567);
                socket.Connect(endPoint);
                socket.Send(Encoding.Default.GetBytes(message));
                socket.Close();
            }
        }

        Thread sender = new Thread(new ThreadStart(MulticastSend));

        public Form1()
        {
            InitializeComponent();
            sender.IsBackground = true;
            sender.Start();
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            message = textBox1.Text;
        }
    }
}
