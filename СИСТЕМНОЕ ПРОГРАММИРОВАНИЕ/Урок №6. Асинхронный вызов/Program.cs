using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Урок__6.Асинхронный_вызов
{
    internal class Program
    {
        private static byte[] statInfo = new byte[1024];

        static void Main(string[] args)
        {
            byte[] info = new byte[1024];
            Console.WriteLine($"Основной поток ID = {Thread.CurrentThread.ManagedThreadId}");
            FileStream fs = new FileStream(@"../../Program.cs", FileMode.Open,
                FileAccess.Read, FileShare.Read, 1024, FileOptions.Asynchronous);
            fs.BeginRead(info, 0, info.Length, delegate (IAsyncResult result)
            {
                Console.WriteLine($"Чтение в потоке {Thread.CurrentThread.ManagedThreadId} закончено");
                int bytesRead = fs.EndRead(result);
                fs.Close();
                Console.WriteLine($"Количество считанных байт = {bytesRead}");
                Console.WriteLine(Encoding.UTF8.GetString(info));
                Console.ReadLine();
            }, null);
            Console.ReadLine();
        }

        static void ReadIsComplete(IAsyncResult result)
        {
            Console.WriteLine($"Чтение в потоке {Thread.CurrentThread.ManagedThreadId} закончено");
            FileStream fs = (FileStream)result.AsyncState;
            int bytesRead = fs.EndRead(result);
            fs.Close();
            Console.WriteLine($"Количество считанных байт = {bytesRead}");
            //Console.WriteLine(Encoding.UTF8.GetString(info));
        }
    }

    class AsyncReader
    {
        FileStream fs;
        byte[] info;
        IAsyncResult result;

        public AsyncReader(FileStream s, int size)
        {
            fs = s;
            info = new byte[size];
            result = fs.BeginRead(info, 0, size, null, null);
        }

        public string EndRead()
        {
            int biteSize = fs.EndRead(result);
            fs.Close();
            Array.Resize(ref info, biteSize);
            return $"File: {Encoding.UTF8.GetString(info)}\n\n{fs.Name}\n____________________";
        }
    }
}
