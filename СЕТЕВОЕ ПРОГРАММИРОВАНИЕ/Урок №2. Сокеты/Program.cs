using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Урок__2.Сокеты
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server("192.168.88.128", 1024);
            server.Start();
            Console.WriteLine("Нажмите любую клавишу для выхода");
            Console.ReadKey();
            server.Stop();
        }
    }

    class Server
    {
        delegate void ConnectDelegate(Socket s);
        delegate void StartNetwork(Socket s);
        Socket socket;
        IPEndPoint endPoint;

        public Server(string address, int port)
        {
            endPoint = new IPEndPoint(IPAddress.Parse(address), port);
        }
        void ServerConnect(Socket s)
        {
            socket.Send(Encoding.ASCII.GetBytes($"{DateTime.Now.ToString()}\t-\thello from ZIMIN"));
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        void ServerBegin(Socket s)
        {
            while (true)
            {
                try
                {
                    while (socket != null)
                    {
                        Socket ns = socket.Accept();
                        Console.WriteLine($"{ns.RemoteEndPoint.ToString()}");
                        ConnectDelegate cd = new ConnectDelegate(ServerConnect);
                        cd.BeginInvoke(ns, null, null);
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public void Start()
        {
            if (socket != null)
            {
                return;
            }
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            socket.Bind(endPoint);
            socket.Listen(10);
            StartNetwork start = new StartNetwork(ServerBegin);
            start.BeginInvoke(socket, null, null);
        }
        public void Stop()
        {
            if (socket != null)
            {
                try
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                    socket = null;
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
