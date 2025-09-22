using System;

namespace Tamagotchi
{
    internal class Game
    {
        public Game()
        {
        }

        public bool PlayGame(Cat cat)
        {
            Console.Write("Введите состояние котика: ");
            string stateString = Console.ReadLine();
            if (!String.IsNullOrEmpty(stateString))
            {
                if (stateString == "died")
                {
                    cat.DoSomething(State.Died);
                    return false;
                }
                else if (stateString == "hungry" || stateString == "h")
                {
                    cat.DoSomething(State.Hungry);
                }
                else if (stateString == "sleepy" || stateString == "s")
                {
                    cat.DoSomething(State.Sleepy);
                }
                else if (stateString == "happy")
                {
                    cat.DoSomething(State.Happy);
                }
                else if (stateString == "want to play" || stateString == "w")
                {
                    cat.DoSomething(State.WantToPlay);
                }
                else if (stateString == "asleep" || stateString == "a")
                {
                    cat.DoSomething(State.Asleep);
                }
                else if (stateString == "dirty" || stateString == "d")
                {
                    cat.DoSomething(State.Dirty);
                }
                else if (stateString == "ill" || stateString == "i")
                {
                    cat.DoSomething(State.Ill);
                }
                else
                {
                    cat.DoSomething(State.Main);
                }
            }
            return true;
        }
        public Cat PauseGame()
        {
            return new Cat();
        }
    }
}
