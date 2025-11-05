using System;
using System.Reflection;

namespace Урок__3.Домены_приложений
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // создать домен приложения
            AppDomain domain = AppDomain.CreateDomain("Test Domain");

            // загрузить в домен подготовленную библиотеку
            Assembly assembly = domain.Load(AssemblyName.GetAssemblyName("SampleLibrary.dll"));

            // получить объект модуля для вызова
            Module module = assembly.GetModule("SampleLibrary.dll");

            // получить тип данных
            Type type = module.GetType("SampleLibrary.SampleClass");

            // получить метод из типа данных
            MethodInfo method = type.GetMethod("DoSome");

            // вызвать метод
            method.Invoke(null, null);

            // однострочный способ вызова
            assembly.GetModule("SampleLibrary.dll").GetType("SampleLibrary.SampleClass").
                GetMethod("DoSome").Invoke(null, null);

            // отгрузить домен приложения
            AppDomain.Unload(domain);
        }
    }
}
