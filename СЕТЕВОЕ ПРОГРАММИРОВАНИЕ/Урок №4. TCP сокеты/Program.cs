using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;

namespace Урок__4.TCP_сокеты
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // server
            IPAddress address = IPAddress.Parse("127.0.0.1");
            //IPEndPoint endPoint = new IPEndPoint(address, 1024);
            int port = 11000;
            //TcpListener listener = new TcpListener(endPoint);
            TcpListener listener = new TcpListener(address, port);
            listener.Start();
            if (listener.Pending())
            {
                Console.WriteLine("В очереди имеются запросы на соединение");
            }
            Socket socket = listener.AcceptSocket();
            //TcpClient client = listener.AcceptTcpClient();
            //socket.Send();
            //socket.Receive();
            listener.Stop();

            // client
            try
            {
                TcpClient client = new TcpClient("localhost", 80);
                NetworkStream stream = client.GetStream();
                byte[] buffer = Encoding.ASCII.GetBytes("connection test");
                stream.Write(buffer, 0, buffer.Length);
                stream.Read(buffer, 0, buffer.Length);
            }
            catch (SocketException se)
            {
                Console.WriteLine($"Ошибка сокета: {se.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
