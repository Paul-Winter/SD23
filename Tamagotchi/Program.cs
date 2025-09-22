using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isPlay = true;
            Game game = new Game();
            while (isPlay)
            {
                isPlay = game.PlayGame(new Cat());
            }
        }
    }
}
