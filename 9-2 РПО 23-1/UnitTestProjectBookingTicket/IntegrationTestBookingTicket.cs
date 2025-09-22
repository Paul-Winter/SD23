using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ClassLibraryTicket;
using BookingTicket;

namespace UnitTestProjectBookingTicket
{
    [TestClass]
    public class IntegrationTestBookingTicket
    {
        // попытка замены бесплатного билета на платный вернёт тот же билет
        [TestMethod]
        public void ChangeTicket_Cost_0_SameTicketReturn()
        {
            // Arrange
            TicketToTheatre ticket1 = new TicketToTheatre();
            TicketToTheatre ticket2 = new TicketToTheatre(1, "theatre", "lounge", 1, 100);
            // Act
            ticket2 = Program.ChangeTicket(ticket1);
            Console.WriteLine(ticket1);
            Console.WriteLine(ticket2);
            // Assert
            Assert.AreEqual(ticket1.Cost, ticket2.Cost);
        }
    }
}
