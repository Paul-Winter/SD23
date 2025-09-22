using System;
using System.Collections.Generic;

namespace РПМ_Урок__29_Поведенческие_паттерны
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Стратегия
            Transformer transformer = new Transformer(8, "Angara", new StarshipMove());
            Console.WriteLine(transformer);
            transformer.Move();

            transformer = new Transformer(220, "Lastochka", new TrainMove());
            Console.WriteLine(transformer);
            transformer.Move();

            transformer = new Transformer(32, "Щ-721", new SubmarineMove());
            Console.WriteLine(transformer);
            transformer.Move();

            Console.WriteLine("\n\n\n_______________________________________________________________________\n\n\n");

            // Наблюдатель
            Stock stock = new Stock();
            Bank bank = new Bank("Сбер", stock);
            BrokerRUB rubler = new BrokerRUB("Иван Иваныч", stock);
            BrokerUSD doller = new BrokerUSD("Джон Джоныч", stock);
            BrokerAsia asian = new BrokerAsia("И Ли", stock);

            stock.Market();
            bank.StopTrade();
            rubler.StopTrade();
            doller.StopTrade();
            asian.StopTrade();
        }
    }

    #region Strategy

    interface IMovable
    {
        void Move();
    }

    class StarshipMove : IMovable
    {
        public void Move()
        {
            Console.WriteLine("Поехали!!!\n");
        }
    }
    
    class SubmarineMove : IMovable
    {
        public void Move()
        {
            Console.WriteLine("20000 лье под водой\n");
        }
    }

    class TrainMove : IMovable
    {
        public void Move()
        {
            Console.WriteLine("Стучат колёса по рельсам\n");
        }
    }

    class Transformer
    {
        protected int passengers;
        protected string model;
        private IMovable movable;

        public IMovable Movable
        {
            private get;
            set;
        }

        public Transformer(int passengers, string model, IMovable movable)
        {
            this.passengers = passengers;
            this.model      = model;
            Movable         = movable;
        }

        public void Move()
        {
            Movable.Move();
        }

        public override string ToString()
        {
            return "Модель " + model + "\t вмещает " + passengers + " пассажиров";
        }
    }

    #endregion

    #region Observer

    interface IObserver
    {
        void Update(Object obj);
    }
    interface IObservable
    {
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers();
    }

    class Stock : IObservable
    {
        StockInfo info;
        List<IObserver> observers;

        public Stock()
        {
            info = new StockInfo();
            observers = new List<IObserver>();
        }

        // добавление наблюдателя в список наблюдателей
        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }
        // удаление наблюдателя из списка
        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }
        // оповещение всех наблюдателей из списка
        public void NotifyObservers()
        {
            foreach (IObserver observer in observers)
            {
                observer.Update(info);
            }
        }

        // имитация работы биржи
        public void Market()
        {
            Random random = new Random();
            info.USD = random.Next(70, 100);
            info.YUN = random.Next(40, 70);
            info.IEN = random.Next(20, 50);
            info.RUB = random.Next(Convert.ToInt32(info.USD * 0.012), Convert.ToInt32(info.USD * 0.02));
            NotifyObservers();
        }
    }

    class StockInfo
    {
        public int USD { get; set; }
        public int RUB { get; set; }
        public int YUN { get; set; }
        public int IEN { get; set; }
    }

    // наблюдает за курсом рубля
    class BrokerRUB : IObserver
    {
        public string Name { get; set; }
        IObservable stock;

        public BrokerRUB(string name, IObservable stock)
        {
            this.Name = name;
            this.stock = stock;
            stock.AddObserver(this);
        }

        public void Update(object obj)
        {
            StockInfo info = (StockInfo)obj;

            if (info.RUB >= 90)
            {
                Console.WriteLine("Брокер " + Name + " покупает рубли по курсу: " + info.RUB + "\n");
            }
            else if (info.RUB <= 80)
            {
                Console.WriteLine("Брокер " + Name + " продаёт рубли по курсу: " + info.RUB + "\n");
            }
        }

        public void StopTrade()
        {
            stock.RemoveObserver(this);
            stock = null;
        }
    }

    // наблюдает за курсом доллара
    class BrokerUSD : IObserver
    {
        public string Name { get; set; }
        IObservable stock;

        public BrokerUSD(string name, IObservable stock)
        {
            this.Name = name;
            this.stock = stock;
            stock.AddObserver(this);
        }

        public void Update(object obj)
        {
            StockInfo info = (StockInfo)obj;

            if (info.USD >= 90)
            {
                Console.WriteLine("Брокер " + Name + " продаёт доллары по курсу: " + info.USD + "\n");
            }
            else if (info.USD <= 80)
            {
                Console.WriteLine("Брокер " + Name + " покупает доллар по курсу: " + info.USD + "\n");
            }
        }

        public void StopTrade()
        {
            stock.RemoveObserver(this);
            stock = null;
        }
    }

    // наблюдает за курсами юаня и иены
    class BrokerAsia : IObserver
    {
        public string Name { get; set; }
        IObservable stock;

        public BrokerAsia(string name, IObservable stock)
        {
            this.Name = name;
            this.stock = stock;
            stock.AddObserver(this);
        }

        public void Update(object obj)
        {
            StockInfo info = (StockInfo)obj;

            if (info.YUN >= 60)
            {
                Console.WriteLine("Брокер " + Name + " продаёт юани по курсу: " + info.YUN + "\n");
            }
            else if (info.YUN <= 50)
            {
                Console.WriteLine("Брокер " + Name + " покупает юани по курсу: " + info.YUN + "\n");
            }

            if (info.IEN >= 40)
            {
                Console.WriteLine("Брокер " + Name + " продаёт иены по курсу: " + info.IEN + "\n");
            }
            else if (info.IEN <= 30)
            {
                Console.WriteLine("Брокер " + Name + " покупает иены по курсу: " + info.IEN + "\n");
            }
        }

        public void StopTrade()
        {
            stock.RemoveObserver(this);
            stock = null;
        }
    }

    // наблюдает за курсами всех валют
    class Bank : IObserver
    {
        public string Name { get; set; }
        IObservable stock;

        public Bank(string name, IObservable stock)
        {
            this.Name = name;
            this.stock = stock;
            stock.AddObserver(this);
        }

        public void Update(object obj)
        {
            StockInfo info = (StockInfo)obj;

            if (info.RUB >= 90)
            {
                Console.WriteLine("Брокер " + Name + " покупает рубли по курсу: " + info.RUB + "\n");
            }
            else if (info.RUB <= 80)
            {
                Console.WriteLine("Брокер " + Name + " продаёт рубли по курсу: " + info.RUB + "\n");
            }

            if (info.USD >= 90)
            {
                Console.WriteLine("Брокер " + Name + " продаёт доллары по курсу: " + info.USD + "\n");
            }
            else if (info.USD <= 80)
            {
                Console.WriteLine("Брокер " + Name + " покупает доллар по курсу: " + info.USD + "\n");
            }

            if (info.YUN >= 60)
            {
                Console.WriteLine("Брокер " + Name + " продаёт юани по курсу: " + info.YUN + "\n");
            }
            else if (info.YUN <= 50)
            {
                Console.WriteLine("Брокер " + Name + " покупает юани по курсу: " + info.YUN + "\n");
            }

            if (info.IEN >= 40)
            {
                Console.WriteLine("Брокер " + Name + " продаёт иены по курсу: " + info.IEN + "\n");
            }
            else if (info.IEN <= 30)
            {
                Console.WriteLine("Брокер " + Name + " покупает иены по курсу: " + info.IEN + "\n");
            }
        }

        public void StopTrade()
        {
            stock.RemoveObserver(this);
            stock = null;
        }
    }

    #endregion
}
