using FluentAssertions;
using Domain;

namespace FlightTest
{
    public class FlightSpecifications
    {
        [Fact]
        public void Booking_reduces_the_number_of_seats()
        {
            var flight = new Flight(seatsCapacity: 3);

            flight.Book("el kumi", 1);//book flight for kumi
            flight.RemainingNumberOfSeats.Should().Be(2);
        }

        [Fact]
        public void Avoids_overbooking()
        {
            //Given
            var flight = new Flight(seatsCapacity: 3);
            //When
            var error = flight.Book("el_kumi@gmail.com", 4);
            //Then
            error.Should().BeOfType<OverbookingError>();
        }
    }
}