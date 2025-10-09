using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Project
{
    internal class Game
    {
        private const int DAY_TIME = 500000;
        private int k = 1;

        public void MainMenu()
        {
            Console.WriteLine("=====================================");
            Console.WriteLine("=================PET=================");
            Console.WriteLine("=====================================");
            Console.WriteLine("\n(1) Начать игру");
            Console.WriteLine("(2) Выход");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    GameMenu();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("До свидания...");
                    break;

            }
        }

        private void GameMenu()
        {

        }
    }
}
