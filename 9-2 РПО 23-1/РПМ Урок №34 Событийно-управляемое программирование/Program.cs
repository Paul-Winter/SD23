using System;
using System.Windows.Forms;

namespace РПМ_Урок__34_Событийно_управляемое_программирование
{
    internal static class Program
    {
        // Написать программу, которая "угадывает" задуманное пользователем число от 1 до 2000
        // Для запроса к пользователю использовать MessageBox
        // После того, как число отгадано, необходимо вывести количество попыток пользователя
        // и предоставить пользователю возможность сыграть ещё раз, не выходя из программы

        // границы угадывания
        static int low = 1;
        static int high = 2000;

        // переменная, которая будет хранить количество попыток
        static int count = 0;
        static int number = (high + low) / 2;

        static DialogResult ShowMessageBoxes()
        {
            DialogResult result;

            // угадывание должно происходить в цикле
            do
            {
                count++;
                // угадываемое число
                string message = "Ваше число: " + number + "?";
                string caption = "Попытка " + count;
                result = MessageBox.Show(message, caption, MessageBoxButtons.OKCancel);

                if (result == DialogResult.OK)
                    return result;
                else
                {
                    message = "Ваше число больше, чем " + number + "?";
                    DialogResult res = MessageBox.Show(message, caption, MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                        number = Ugadaika(res);
                    else
                        number = Ugadaika(res);
                }
            } while (result == DialogResult.Cancel);

            return result;
        }

        static int Ugadaika(DialogResult result)
        {
            if (result == DialogResult.Yes)
            {
                low = number;
            }
            else
            {
                high = number;
            }
            return (high + low) / 2;
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //string message = "Загадайте число от 1 до 2000";
            //MessageBox.Show(message);

            //DialogResult result;
            //do
            //{
            //    result = ShowMessageBoxes();
            //} while (result == DialogResult.Retry);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
