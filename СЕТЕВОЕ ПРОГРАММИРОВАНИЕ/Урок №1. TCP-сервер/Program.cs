using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Урок__1.TCP_сервер
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Создать объект сокета
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            IPAddress ip = IPAddress.Parse("192.168.88.128");
            IPEndPoint endPoint = new IPEndPoint(ip, 1024);

            // 2. Связать сокет с IP адресом и портом на сервере
            socket.Bind(endPoint);

            // 3. Установить сокет в режим прослушивания
            socket.Listen(1000);

            // 4. Вызвать метод Accept (в цикле), чтобы получить сокет для обмена сообщениями
            try
            {
                while (true)
                {
                    Socket ns = socket.Accept();
                    Console.WriteLine(ns.RemoteEndPoint.ToString());
                    ns.Send(Encoding.ASCII.GetBytes(DateTime.Now.ToString()));
                    
                    // 5. После завершения обмена сообщениями, сокет закрывается
                    ns.Shutdown(SocketShutdown.Both);
                    ns.Close();
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
