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
            string[] files =
            {
                "../../Program.cs",
                "../../Properties/AssemblyInfo.cs",
                "../../Урок №6. Асинхронный вызов.csproj"
            };
            for (int i = 0; i < files.Length; i++)
            {
                new AsyncCallBackReader(new FileStream(files[i], FileMode.Open, FileAccess.Read, FileShare.Read,
                    1024, FileOptions.Asynchronous), 1024, delegate (byte[] info)
                    {
                        Console.WriteLine($"Количество прочитанных байт = {info.Length}");
                        Console.WriteLine(Encoding.UTF8.GetString(info) + "\n\n");
                    });
            }
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

    public delegate void AsyncBytesReadDel(byte[] streamData);

    class AsyncCallBackReader
    {
        FileStream fs;
        byte[] info;
        IAsyncResult result;
        AsyncBytesReadDel callBackMethod;

        public AsyncCallBackReader(FileStream s, int size, AsyncBytesReadDel meth)
        {
            fs = s;
            info = new byte[size];
            callBackMethod = meth;
            result = s.BeginRead(info, 0, size, ReadIsComplete, null);
        }

        private void ReadIsComplete(IAsyncResult ar)
        {
            int countByte = fs.EndRead(result);
            fs.Close();
            Array.Resize(ref info, countByte);
            callBackMethod(info);
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
