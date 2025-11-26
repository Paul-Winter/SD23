using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Урок__6.UDP_сокеты
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UdpClient client = new UdpClient();
            IPAddress ip = IPAddress.Parse("192.168.1.51");
            IPEndPoint endPoint = new IPEndPoint(ip, 1234);
            byte[] buffer = Encoding.ASCII.GetBytes("test message");
            // установка соединения
            try
            {
                //client.Connect(endPoint);
                client.Connect(ip, 1234);

                // отправка сообщения
                try
                {
                    client.Send(buffer, buffer.Length, new IPEndPoint(ip, 9001));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка отправки: " + ex.ToString());
                }

                // получение сообщения
                byte[] responce = client.Receive(ref endPoint);
                string result = Encoding.ASCII.GetString(responce);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка подключения: " + ex.ToString());
            }
            finally
            {
                // закрытие соединения
                client.Close();
            }
        }
    }
}
