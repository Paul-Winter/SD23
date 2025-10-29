using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Урок__2.Процессы
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            Process proc = new Process();
            proc.StartInfo.FileName = "notepad.exe";
            proc.Start();
            Console.WriteLine($"Запущен процесс: {proc.ProcessName}");
            proc.WaitForExit();
            Console.WriteLine($"Процесс завершился с кодом: {proc.ExitCode}");
            Console.WriteLine($"Текущий процесс: {Process.GetCurrentProcess().ProcessName}");
            */

            /*
            Console.Title = "Список процессов";
            Console.WindowHeight = 40;
            Console.WindowWidth = 40;
            Process[] processes = Process.GetProcesses();
            Console.WriteLine("\t{0,-40}{1,-12}","Имя процесса:","PID:\n");
            foreach (Process p in processes)
            {
                Console.WriteLine("\t{0,-40}{1,-12}", p.ProcessName, p.Id);
            }
            */

            Assembly assembly = Assembly.Load(AssemblyName.GetAssemblyName("Human.dll"));
            Module module = assembly.GetModule("Human.dll");
            Console.WriteLine("Объявленные типы данных:");
            foreach (Type t in module.GetTypes())
            {
                Console.WriteLine(t.FullName);
            }
            Type Employee = module.GetType("Human.Employee") as Type;
            object employee = Activator.CreateInstance(Employee, new object[] { "Ivan", "Ivanov", 19, 33000.33 });
            Employee.GetMethod("ShowEmployee").Invoke(employee, null);
        }
    }
}
