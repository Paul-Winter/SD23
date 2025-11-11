using System;
using System.IO;
using System.Text;

namespace Урок__6.Асинхронный_вызов
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileStream fs = new FileStream(@"../../Program.cs", FileMode.Open,
                FileAccess.Read, FileShare.Read, 1024, FileOptions.Asynchronous);
            byte[] info = new byte[256];
            IAsyncResult result = fs.BeginRead(info, 0, info.Length, null, null);
            int bytesRead = fs.EndRead(result);
            Console.WriteLine($"Количество прочитанных байт = {bytesRead}");
            Console.WriteLine(Encoding.UTF8.GetString(info));
        }
    }
}
