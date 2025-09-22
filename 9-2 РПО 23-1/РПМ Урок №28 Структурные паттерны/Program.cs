using System;

namespace РПМ_Урок__28_Структурные_паттерны
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Фасад
            Script scriptHamlet = new Script();
            Role roleHamlet = new Role();
            Grim grimHamlet = new Grim();
            Actor actor = new Actor();

            ShowMustGoOnFacade hamlet = new ShowMustGoOnFacade(scriptHamlet, roleHamlet, grimHamlet);
            actor.ShowMustGoOn(hamlet);

            Script scriptMono = new Script();
            Role roleSolo = new Role();

            ShowMustGoOnFacade mono = new ShowMustGoOnFacade(scriptMono, roleSolo);
            actor.ShowMustGoOn(mono);

            // Адаптер
            //Warrior warrior = new Warrior();
            //Tank tank = new Tank();
            //warrior.Attack(tank);
            //Horse horse = new Horse();
            //ITransport horseTransport = new HorseToTransportAdapter(horse);
            //warrior.Attack(horseTransport);

            // Декоратор
            //Burger burger1 = new GermanBurger();
            //burger1 = new BavarianSausage(burger1);
            //Console.WriteLine(burger1.Name + " " + burger1.GetCost() + "\n");

            //Burger burger2 = new AmericanBurger();
            //burger2 = new BeefSteak(burger2);
            //Console.WriteLine(burger2.Name + " " + burger2.GetCost() + "\n");

            //burger2 = new BavarianSausage(burger2);
            //Console.WriteLine(burger2.Name + " " + burger2.GetCost() + "\n");
        }
    }

    #region Facade

    class Actor
    {
        public void ShowMustGoOn(ShowMustGoOnFacade facade)
        {
            facade.Show();
            facade.Applause();
            facade.GetFlowers();
        }
    }
    class Script
    {
        public void ReadScript()
        {
            Console.WriteLine("Тсс. Актёр читает сценарий!\n");
        }
    }
    class Role
    {
        public void MemorizeRole()
        {
            Console.WriteLine("Не мешайте - актёр учит роль!\n");
        }
        public void PlayRole()
        {
            Console.WriteLine("Быть или не быть? Вот в чём вопрос...\n");
        }
    }
    class Grim
    {
        public void MakeUp()
        {
            Console.WriteLine("Актёр гримируется!\n");
        }
    }
    class ShowMustGoOnFacade
    {
        Script script = new Script();
        Role   role   = new Role();
        Grim   grim   = new Grim();

        public ShowMustGoOnFacade(Script script, Role role, Grim grim)
        {
            this.script = script;
            this.role   = role;
            this.grim   = grim;
        }

        public ShowMustGoOnFacade(Script script, Role role)
        {
            this.script = script;
            this.role   = role;
            this.grim   = null;
        }

        public void Show()
        {
            script.ReadScript();
            role.MemorizeRole();
            if(grim != null)
                grim.MakeUp();
            role.PlayRole();
        }

        public void Applause()
        {
            Console.WriteLine("Актёр кланяется\n");
        }

        public void GetFlowers()
        {
            Console.WriteLine("Актёр получает цветы\n");
        }
    }

    #endregion

    #region Adapter

    interface ITransport
    {
        void Drive();
    }
    class Tank : ITransport
    {
        public void Drive()
        {
            Console.WriteLine("Танки грязи не боятся!\n");
        }
    }

    interface IAnimal
    {
        void Move();
    }
    class Horse : IAnimal
    {
        public void Move()
        {
            Console.WriteLine("Лошадь скачет очень быстро!\n");
        }
    }

    class HorseToTransportAdapter : ITransport
    {
        Horse horse;
        public HorseToTransportAdapter(Horse horse)
        {
            this.horse = horse;
        }

        public void Drive()
        {
            horse.Move();
        }
    }

    class Warrior
    {
        public void Attack(ITransport transport)
        {
            transport.Drive();
        }
    }

    #endregion

    #region Decorator

    abstract class Burger
    {
        public string Name { get; protected set; }

        protected Burger(string name)
        {
            this.Name = name;
        }

        public abstract int GetCost();
    }

    class GermanBurger : Burger
    {
        public GermanBurger() : base("Баварский гамбургер") {}

        public override int GetCost()
        {
            return 11;
        }
    }

    class AmericanBurger : Burger
    {
        public AmericanBurger() : base("Американский чизбургер") {}

        public override int GetCost()
        {
            return 10;
        }
    }

    abstract class BurgerDecorator : Burger
    {
        protected Burger burger;

        protected BurgerDecorator(string name,  Burger burger) : base(name)
        {
            this.burger = burger;
        }
    }

    class BavarianSausage : BurgerDecorator
    {
        public BavarianSausage(Burger burger) : base(burger.Name + " , с баварскими сосисками", burger) {}

        public override int GetCost()
        {
            return burger.GetCost() + 3;
        }
    }

    class BeefSteak : BurgerDecorator
    {
        public BeefSteak(Burger burger) : base(burger.Name + " , с говяжьей котлеткой", burger) {}
        
        public override int GetCost()
        {
            return burger.GetCost() + 2;
        }
    }

    #endregion
}
