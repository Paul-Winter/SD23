namespace ClassLibraryTicket
{
    public abstract class Ticket
    {
        private int    id;
        private string dest;
        private double cost;

        public int      Id { get => id;   set =>   id = value; }
        public string Dest { get => dest; set => dest = value; }
        public double Cost { get => cost; set => cost = value; }
    }

    public class TicketToTheatre : Ticket
    {
        private string space;
        private int point;

        public string Space { get => space; set => space = value; }
        public int Point { get => point; set => point = value; }

        public TicketToTheatre(int id, string dest, string space, int point, double cost)
        {
            this.Id    = id;
            this.Dest  = dest;
            this.Space = space;
            this.Point = point;
            this.Cost  = cost;
        }
        public TicketToTheatre()
        {
            this.Id    = 0;
            this.Dest  = "";
            this.Space = "";
            this.Point = 0;
            this.Cost  = 0.0;
        }
        public override string ToString()
        {
            return $"Id: {Id}\t{Dest}\tSpace: {Space}\tPoint: {Point}\tCost: {Cost}";
        }

        public TicketToTheatre ChangeTicket(int id, string dest, string space, int point, double cost)
        {
            TicketToTheatre ticket = new TicketToTheatre(id, dest, space, point, cost);
            return ticket;
        }

        public double BookingTicket(int id, string dest, string space, int point)
        {
            return 650.50;
        }

        public TicketToTheatre BuyTicket(double cost)
        {
            TicketToTheatre ticket = new TicketToTheatre();
            ticket.Cost = cost;
            return ticket;
        }
    }

    public class TicketToRide : Ticket
    {

    }

    public class TicketToTram : Ticket
    {

    }
}
