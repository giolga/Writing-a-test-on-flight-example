using FluentAssertions;
using Domain;

namespace FlightTest
{
    public class FlightSpecifications
    {
        [Theory]
        [InlineData(3, 1, 2)] //Create flight with 3 seats, book 1 and result should be 2 remaining seats
        [InlineData(6, 3, 3)]
        [InlineData(10, 6, 4)]
        [InlineData(12, 8, 4)]
        public void Booking_reduces_the_number_of_seats(int seatCapacity, int numberOfSeats, int remainingNumberOfSeats)
        {
            var flight = new Flight(seatsCapacity: seatCapacity);

            flight.Book("el kumi", numberOfSeats);//book flight for kumi
            flight.RemainingNumberOfSeats.Should().Be(remainingNumberOfSeats);
        }

        //[Fact]
        //public void Booking_reduces_the_number_of_seats_2()
        //{
        //    var flight = new Flight(seatsCapacity: 6);

        //    flight.Book("el kumi", 3);//book flight for kumi
        //    flight.RemainingNumberOfSeats.Should().Be(3);
        //}

        //[Fact]
        //public void Booking_reduces_the_number_of_seats_3()
        //{
        //    var flight = new Flight(seatsCapacity: 10);

        //    flight.Book("el kumi", 6);//book flight for kumi
        //    flight.RemainingNumberOfSeats.Should().Be(4);
        //}

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

        [Fact]
        public void Books_flights_successfully()
        {
            var flight = new Flight(seatsCapacity: 3);
            var error = flight.Book("elkumi@gmail.com", 1);
            error.Should().BeNull();
        }

        [Fact]
        public void Remembers_booking()
        {
            var flight = new Flight(seatsCapacity: 150);
            flight.Book(passengerEmail: "a@b.com", 4);

            flight.BookingList.Should().ContainEquivalentOf(new Booking("a@b.com", 4));
        }

        [Theory]
        [InlineData(3, 1, 1, 3)]
        [InlineData(4, 2, 2, 4)]
        [InlineData(7, 5, 4, 6)]
        public void Cancelling_bookings_frees_up_seats(int initialCapacity, int numberOfSeatsToBook, int numberOfSeatsToCancel, int remainingNumberOfSeats)
        {
            // given
            var flight = new Flight(initialCapacity);
            flight.Book("a@b.com", numberOfSeatsToBook);
            // When
            flight.CancelBooking("a@b.com", numberOfSeatsToCancel);
            // then
            flight.RemainingNumberOfSeats.Should().Be(remainingNumberOfSeats);
        }

        [Fact]
        public void Doesnt_cancel_bookings_for_passengers_who_have_not_booked()
        {
            var flight = new Flight(3);

            var error = flight.CancelBooking("CHAMA@gmail.com", 2);

            error.Should().BeOfType<BookingNotFoundError>();
        }

        [Fact]
        public void Returns_null_when_successfully_cancels_a_booking()
        {
            var flight = new Flight(3);
            flight.Book("chama@gmail.com", 1);
            var error = flight.CancelBooking("chama@gmail.com", 1);
            error.Should().BeNull();
        }
    }
}