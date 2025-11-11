using System;
using System.IO;
using System.Text;

namespace Урок__6.Асинхронный_вызов
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] files =
            {
                "../../Program.cs",
                "../../Урок №6. Асинхронный вызов.csproj",
                "../../Properties/AssemblyInfo.cs"
            };
            AsyncReader[] readers = new AsyncReader[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                readers[i] = new AsyncReader(new FileStream(files[i], FileMode.Open,
                    FileAccess.Read, FileShare.Read, 1024, FileOptions.Asynchronous), 1024);
            }
            foreach (AsyncReader reader in readers)
            {
                Console.WriteLine(reader.EndRead());
            }
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
