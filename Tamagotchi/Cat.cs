using System;

namespace Tamagotchi
{
    enum State
    {
        Asleep,
        Died,
        Dirty,
        Happy,
        Ill,
        Hungry,
        Main,
        Sleepy,
        WantToPlay
    }

    internal class Cat
    {
        internal int age;
        internal State state;

        public Cat()
        {
            age = 0;
            state = State.Hungry;
        }

        public override string ToString()
        {
            return $"Your pet is {state}";
        }

        public State DoSomething(State state)
        {
            switch (state)
            {
                case State.Asleep:
                    Console.WriteLine("Проигрывается анимация спящего питомца");
                    break;
                case State.Died:
                    Console.WriteLine(@"
                    __
                    ||
                 |==||==|
                    ||
                  __||__
                 /      \       
       ($)      /        \     ((%))
        |   (^) | R.I.P. | (*)   |
  (@)   |    |  |        |  |    |   (&)
 _\|/__\|/__\|/_|________|_\|/__\|/__\|/_");
                    break;
                case State.Dirty:
                    Console.WriteLine("Проигрывается анимация грязного питомца");
                    break;
                case State.Happy:
                    Console.WriteLine("Проигрывается анимация счастливого питомца");
                    break;
                case State.Hungry:
                    Console.WriteLine("Проигрывается анимация голодного питомца");
                    break;
                case State.Ill:
                    Console.WriteLine("Проигрывается анимация больного питомца");
                    break;
                case State.Sleepy:
                    Console.WriteLine("Проигрывается анимация сонного питомца");
                    break;
                case State.WantToPlay:
                    Console.WriteLine("Проигрывается анимация игривого питомца");
                    break;
                default:
                    Console.WriteLine("Проигрывается обычная анимация питомца");
                    break;
            }
            return State.Main;
        }
    }
}
