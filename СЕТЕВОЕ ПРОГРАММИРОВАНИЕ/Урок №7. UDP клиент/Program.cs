using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Урок__7.UDP_клиент
{
    internal class Program
    {
        static int remotePort;
        static int localPort;
        static IPAddress remoteAddress;

        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Console.SetWindowSize(40, 20);
                Console.Title = "Чатик";
                Console.Write("введите удалённый IP-адрес: ");
                remoteAddress = IPAddress.Parse(Console.ReadLine());
                Console.Write("введите удалённый порт: ");
                remotePort = Convert.ToInt32(Console.ReadLine());
                Console.Write("введите локальный порт: ");
                localPort = Convert.ToInt32(Console.ReadLine());
                Thread thread = new Thread(new ThreadStart(ReceiveFunc));
                thread.IsBackground = true;
                thread.Start();
                Console.ForegroundColor = ConsoleColor.Red;
                while (true)
                {
                    SendFunc(Console.ReadLine());
                }
            }
            catch (FormatException fe)
            {
                Console.WriteLine($"Неверный формат: {fe.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void ReceiveFunc()
        {
            while (true)
            {
                UdpClient client = null;
                try
                {
                    client = new UdpClient(localPort);
                    IPEndPoint endPoint = null;
                    byte[] responce = client.Receive(ref endPoint);
                    string result = Encoding.Unicode.GetString(responce);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(result);
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                catch (SocketException se)
                {
                    Console.WriteLine($"Ошибка сокета: {se.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                finally
                {
                    client.Close();
                }
            }
        }

        static void SendFunc(string message)
        {
            UdpClient client = new UdpClient();
            IPEndPoint endPoint = new IPEndPoint(remoteAddress, remotePort);
            try
            {
                byte[] buffer = Encoding.Unicode.GetBytes(message);
                client.Send(buffer, buffer.Length, endPoint);
            }
            catch (SocketException se)
            {
                Console.WriteLine($"Ошибка сокета: {se.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            finally
            {
                client.Close();
            }
        }
    }
}
