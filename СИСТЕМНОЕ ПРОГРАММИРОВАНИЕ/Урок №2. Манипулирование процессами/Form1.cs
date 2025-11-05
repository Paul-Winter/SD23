using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Урок__2.Манипулирование_процессами
{
    public partial class Form1 : Form
    {
        // константа, идентифицирующая сообщение
        const uint WM_SETTEXT = 0x0C;
        // счётчик запущенных процессов
        int counter = 0;
        // список дочерних процессов
        List<Process> processes = new List<Process>();

        // импорт функции SendMessage
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hwnd, uint msg, int wParam,
            [MarshalAs(UnmanagedType.LPStr)] string lParam);
        public Form1()
        {
            InitializeComponent();
            LoadAvailableAssemblies();
        }
        // метод, загружающий доступные исполняемые файлы из домашней директории проекта
        void LoadAvailableAssemblies()
        {
            string except = new FileInfo(Application.ExecutablePath).Name;
            except = except.Substring(0, except.IndexOf("."));
            string[] files = Directory.GetFiles(Application.StartupPath, "*.exe");
            foreach (var file in files)
            {
                string fileName = new FileInfo(file).Name;
                if (fileName.IndexOf(except) == -1)
                {
                    availableAssemblies.Items.Add(fileName);
                }
            }
        }
        // метод, запускающий процесс на исполнение
        void RunProcess(string AssemblyName)
        {
            Process proc = Process.Start(AssemblyName);
            processes.Add(proc);
            if (Process.GetCurrentProcess().Id == GetParentProcessId(proc.Id))
            {
                MessageBox.Show(proc.ProcessName + " является дочерним для текущего процесса!");
            }
            proc.EnableRaisingEvents = true;
            proc.Exited += proc_Exited;
            SetChildWindowText(proc.MainWindowHandle, "Child process #" + (++counter));
            if (!startedAssemblies.Items.Contains(proc.ProcessName))
            {
                startedAssemblies.Items.Add(proc.ProcessName);
                availableAssemblies.Items.Remove(availableAssemblies.SelectedItem);
            }
        }
        // метод обёртывания для отправки сообщения
        private void SetChildWindowText(IntPtr handle, string text)
        {
            SendMessage(handle, WM_SETTEXT, 0, text);
        }
        // обработчик события закрытия
        private void proc_Exited(object sender, EventArgs e)
        {
            Process proc = sender as Process;
            startedAssemblies.Items.Remove(proc.ProcessName);
            availableAssemblies.Items.Add(proc.ProcessName);
            processes.Remove(proc);
            counter--;
            int index = 0;
            foreach (var p in processes)
            {
                SetChildWindowText(p.MainWindowHandle, "Child process #" + ++index);
            }
        }
        private int GetParentProcessId(int id)
        {
            int parentId = 0;
            using (ManagementObject obj =
                new ManagementObject("win32_process.handle=" + id.ToString()))
            {
                obj.Get();
                parentId = Convert.ToInt32(obj["ParentProcessId"]);
            }
            return parentId;
        }

        delegate void ProcessDelegate(Process proc);

        private void ExecuteProcessByName(string processName, ProcessDelegate func)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            foreach (Process process in processes)
            {
                if (Process.GetCurrentProcess().Id == GetParentProcessId(process.Id))
                {
                    func(process);
                }
            }
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            RunProcess(availableAssemblies.SelectedItem.ToString());
        }

        private void Kill(Process proc)
        {
            proc.Kill();
        }
        private void btn_stop_Click(object sender, EventArgs e)
        {
            ExecuteProcessByName(startedAssemblies.SelectedItem.ToString(), Kill);
            startedAssemblies.Items.Remove(startedAssemblies.SelectedItem);
        }

        private void CloseMainWindow(Process proc)
        {
            proc.CloseMainWindow();
        }
        private void btn_closeWindow_Click(object sender, EventArgs e)
        {
            ExecuteProcessByName(startedAssemblies.SelectedItem.ToString(), CloseMainWindow);
            startedAssemblies.Items.Remove(startedAssemblies.SelectedItem);
        }

        private void Refresh(Process proc)
        {
            proc.Refresh();
        }
        private void btn_refresh_Click(object sender, EventArgs e)
        {
            ExecuteProcessByName(startedAssemblies.SelectedItem.ToString(), Refresh);
        }

        private void availableAssemblies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (availableAssemblies.SelectedItems.Count == 0)
            {
                btn_start.Enabled = false;
            }
            else
            {
                btn_start.Enabled = true;
            }
        }
        private void startedAssemblies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (startedAssemblies.SelectedItems.Count == 0)
            {
                btn_stop.Enabled = false;
                btn_closeWindow.Enabled = false;                
            }
            else
            {
                btn_stop.Enabled = true;
                btn_closeWindow.Enabled = true;
            }
        }

        private void btn_run_Click(object sender, EventArgs e)
        {
            RunProcess("calc.exe");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var p in processes)
            {
                p.Kill();
            }
        }
    }
}
