using System;

namespace NET_Урок__8_Структуры_и_перечисления
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*Coordinate cord = new Coordinate();
            cord.latitude = 10.123;
            cord.longitude = 12.548;
            Console.WriteLine(cord);
            */

            //  Описать структуру:

            //  Александрова - Article (код товара, название товара, цена товара, тип товара)
            //                 ArticleType (тип товара)
            //  Антонов      - Client (код клиента, ФИО, телефон, количество заказов, общая сумма заказов)
            //                 ClientType (тип клиента)
            //  Духина       - RequestItem (товар, количество единиц товара, тип оплаты)
            //                 PayType (тип оплаты)
            //  Землянский   - Address (страна, город, улица, номер дома, тип субъекта)
            //                 DistrictType (тип субъекта)
            //  Золин        - Request (код заказа, перечень заказанных товаров, сумма заказа (вычисляемое свойство))
            //                 PayType (тип оплаты)
            //  Красицкий    - Article (код товара, название товара, цена товара)
            //                 ArticleType (тип товара)
            //  Кубанов      - Client (код клиента, ФИО, телефон, количество заказов, общая сумма заказов)
            //                 ClientType (тип клиента)
            //  Лушников     - RequestItem (товар, количество единиц товара)
            //                 PayType (тип оплаты)
            //  Мамонтова    - Address (страна, город, улица, номер дома)
            //                 DistrictType (тип субъекта)
            //  Метелицин    - Request (код заказа, перечень заказанных товаров, сумма заказа (вычисляемое свойство))
            //                 PayType (тип оплаты)
            //  Чавычалов    - Address (страна, город, улица, номер дома)
            //                 DistrictType (тип субъекта)
            //  Юнусов       - RequestItem (товар, количество единиц товара)
            //                 PayType (тип оплаты)

            /*double length = 7.342;
            double width = 23.49;
            Dimensions dimensions1 = new Dimensions();
            dimensions1.Show();
            Dimensions dimensions2 = new Dimensions(length, width);
            dimensions2.Show();
            Console.WriteLine(dimensions2);
            */

            DayOfWeek today = DayOfWeek.Friday;
            Season season = Season.Spring;
            Planet planet = Planet.Earth;
            TransportType transport = TransportType.Bus;
            Discount discount = Discount.VIP;

            Console.WriteLine(today);
            Console.WriteLine(season);
            Console.WriteLine(planet);
            Console.WriteLine(transport);
            Console.WriteLine(discount);

            Console.WriteLine($"Tomorrow {NextDay(today)}");
        }

        public static DayOfWeek NextDay(DayOfWeek day)
        {
            //if (day < DayOfWeek.Sunday)
            //{
            //    return ++day;
            //}
            //return DayOfWeek.Monday;

            return (day < DayOfWeek.Sunday) ? ++day : DayOfWeek.Monday;
        }
    }

    #region Структуры

    public interface IShow
    {
        void Show();
    }

    public struct Coordinate
    {
        public double latitude;
        public double longitude;

        public override string ToString()
        {
            return $"{latitude} : {longitude}";
        }
    }

    public struct Dimensions : IShow
    {
        private double length;
        private double width;

        public Dimensions(double length, double width)
        {
            this.length = length;
            this.width = width;
        }

        public void Show()
        {
            Console.WriteLine($"Длина: {length}\nШирина: {width}");
        }

        public override string ToString()
        {
            return $"Length: {length}\nWidth: {width}";
        }
    }

    #endregion

    #region Перечисления

    enum DayOfWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    enum Season
    {
        Winter,
        Spring,
        Summer,
        Autumn
    }

    enum Planet
    {
        Mercury,
        Venus,
        Earth,
        Mars,
        Jupiter,
        Saturn,
        Uranus,
        Neptune
    }

    enum TransportType
    {
        Bike,
        Car,
        Tram,
        Bus,
        TrolleyBus,
        Ship,
        Airplane
    }

    enum Discount
    {
        Default,
        Incentive = 2,
        Patron = 5,
        VIP = 15
    }

    enum DistanceSun : ulong
    {
        Sun = 0,
        Mercury = 57900000,
        Venus = 108200000,
        Earth = 149600000,
        Mars = 227900000,
        Jupiter = 778300000,
        Saturn = 1427000000,
        Uranus = 2870000000,
        Neptune = 4496000000
    }

    #endregion
}
