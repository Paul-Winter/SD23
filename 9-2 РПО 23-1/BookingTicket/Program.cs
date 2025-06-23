using System;
using ClassLibraryTicket;

namespace BookingTicket
{
    public class Program
    {
        static Random random = new Random();

        public static void Main(string[] args)
        {
            TicketToTheatre ticket = new TicketToTheatre();
            Console.WriteLine(ticket);
            ticket = ticket.ChangeTicket(100500, "Драматический театр имени М.Ю.Лермонтова", "партер", 33, 500.50);
            Console.WriteLine(ticket);
        }

        public static TicketToTheatre ChangeTicket(TicketToTheatre ticket)
        {
            if (ticket.Cost > 0.0)
            {
                Console.WriteLine("Изменение билета");
                Console.Write("Выберите ложу: ");
                ticket.Space = Console.ReadLine();
                Console.Write("Выберите место: ");
                ticket.Point = Convert.ToInt32(Console.ReadLine());
                ticket.Id = random.Next(1, 1001);
                return ticket;
            }
            else
                return ticket;
        }
    }
}
