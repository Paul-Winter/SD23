using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Урок__8.Unicast
{
    public partial class Form1 : Form
    {
        delegate void AppendText(string text);
        void AppendTextProc(string text)
        {
            textBox1.Text = text;
        }

        void Listener()
        {
            while (true)
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 4567);
                socket.Bind(endPoint);
                IPAddress ip = IPAddress.Parse("224.5.5.5");
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership,
                    new MulticastOption(ip, IPAddress.Any));
                byte[] buffer = new byte[1024];
                socket.Receive(buffer);
                this.Invoke(new AppendText(AppendTextProc), Encoding.Default.GetString(buffer));
                socket.Close();
            }
        }

        Thread listen;

        public Form1()
        {
            InitializeComponent();
            listen = new Thread(new ThreadStart(Listener));
            listen.IsBackground = true;
            listen.Start();
        }
    }
}
