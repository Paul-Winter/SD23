using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Урок__3.Использование_доменов_приложений
{
    internal static class Program
    {
        static AppDomain Drawer;
        static AppDomain TextWindow;
        static Assembly DrawAssembly;
        static Assembly TextAssembly;
        static Form DrawerWnd;
        static Form TextWnd;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        [LoaderOptimization(LoaderOptimization.MultiDomain)]
        static void Main()
        {
            Application.EnableVisualStyles();

            Drawer = AppDomain.CreateDomain("Drawer");
            TextWindow = AppDomain.CreateDomain("TextWindow");

            DrawAssembly = Drawer.Load(AssemblyName.GetAssemblyName("Text Drawer.exe"));
            TextAssembly = TextWindow.Load(AssemblyName.GetAssemblyName("TextWindow.exe"));

            DrawerWnd = Activator.CreateInstance(DrawAssembly.GetType("TextDrawer.Form1")) as Form;
            TextWnd = Activator.CreateInstance(TextAssembly.GetType("TextWindow.Form1"),
                new object[]
                {
                    DrawAssembly.GetModule("Text Drawer.exe"),
                    DrawerWnd
                }) as Form;

            (new Thread(new ThreadStart(RunVisualizer))).Start();
            (new Thread(new ThreadStart(RunDrawer))).Start();
            Drawer.DomainUnload += new EventHandler(Drawer_DomainUpload);
        }

        static void Drawer_DomainUpload(object sender, EventArgs e)
        {
            MessageBox.Show("Домен: " + (sender as AppDomain).FriendlyName + " был успешно загружен");
        }

        static void RunDrawer()
        {
            DrawerWnd.ShowDialog();
            AppDomain.Unload(Drawer);
        }

        static void RunVisualizer()
        {
            TextWnd.ShowDialog();
            Application.Exit();
        }
    }
}
