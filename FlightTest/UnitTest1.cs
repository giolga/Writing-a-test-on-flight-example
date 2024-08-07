using FluentAssertions;
using Domain.Tests;

namespace FlightTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var flight = new Flight(seatsCapacity: 3);

            flight.Book("el kumi", 1);//book flight for kumi
            flight.RemainingNumberOfSeats.Should().Be(2);
        }
    }
}