using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Урок__3.Асинхронные_сокеты
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AsyncServer server = new AsyncServer("192.168.88.128", 1024);
            server.StartServer();
            Console.ReadLine();
        }
    }

    class AsyncServer
    {
        // 1. создать объект Socket
        Socket socket;
        IPEndPoint endPoint;

        public AsyncServer(string strAddr, int port)
        {
            endPoint = new IPEndPoint(IPAddress.Parse(strAddr), port);
        }

        // 3. установить сокет в состояние прослушивания
        void MyAcceptCallBackFunc(IAsyncResult asyncResult)
        {
            Socket socket = (Socket)asyncResult.AsyncState;

            // 6. у полученного сокета вызвать метод EndAccept, который вернёт новый
            //    объект сокета, через который будет происходить обмен сообщениями
            Socket newSocket = socket.EndAccept(asyncResult);
            Console.WriteLine($"{newSocket.RemoteEndPoint.ToString()}");
            byte[] buffer = Encoding.ASCII.GetBytes(DateTime.Now.ToString());

            // 4. создать делегат AsyncCallback и вызвать метод BeginAccept, передав
            //    в качестве параметра делегат и слушающий сокет
            newSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None,
                new AsyncCallback(MySendCallBackFunc), newSocket);
            
            // 7. снова вызвать BeginAccept в цикле
            socket.BeginAccept(new AsyncCallback(MyAcceptCallBackFunc), socket);
        }

        void MySendCallBackFunc(IAsyncResult ar)
        {
            Socket newSocket = (Socket)ar.AsyncState;
            int n = ((Socket)ar.AsyncState).EndSend(ar);
            newSocket.Shutdown(SocketShutdown.Send);
            newSocket.Close();
        }

        // 2. связать объект с портом на сервере вызвав метод Bind
        public void StartServer()
        {
            if (socket != null)
            {
                return;
            }
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                                ProtocolType.IP);
            socket.Bind(endPoint);
            socket.Listen(10);

            // 5. при подключении клиента будет вызван делегат, которому в свойстве
            //    AsyncState придёт наш слушающий сокет
            socket.BeginAccept(new AsyncCallback(MyAcceptCallBackFunc), socket);
        }
    }
}
