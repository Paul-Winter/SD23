using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Урок__1.Сокеты
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Создать сокет
            IPAddress ip = IPAddress.Parse("192.168.88.128");
            IPEndPoint endPoint = new IPEndPoint(ip, 1024);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            string result = "";

            while (true)
            {
                // 2. Вызвать метод Connect сокета и передать ему объект класса EndPoint
                try
                {
                    socket.Connect(endPoint);

                    // 3. В случае успешного соединения - начать обмен сообщениями
                    // метод Send - для отправки сообщений, Receive - для получения
                    if (socket.Connected)
                    {
                        Console.Write("Введите сообщение: ");
                        string strSend = Console.ReadLine();
                        socket.Send(Encoding.ASCII.GetBytes(strSend));
                        byte[] buffer = new byte[1024];
                        int length;
                        do
                        {
                            length = socket.Receive(buffer);
                            result += Encoding.ASCII.GetString(buffer, 0, 1);
                            Console.WriteLine(result);
                        } while (length > 0);
                    }
                    else
                    {
                        Console.WriteLine("___________________________________________________");
                        Console.WriteLine("____________________ERROR!!!_______________________");
                        Console.WriteLine("___________________________________________________");
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
