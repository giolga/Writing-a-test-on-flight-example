using Xunit;
using System.Collections.Generic;
using FluentAssertions;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using Application.Test;
using Application;

namespace Application.Test
{
    public class FlightApplicationSpecifications
    {
        private readonly Entities entities = new Entities(new DbContextOptionsBuilder<Entities>().UseInMemoryDatabase("Flights").Options);
        private readonly BookingService bookingService ;

        public FlightApplicationSpecifications()
        {
            bookingService = new BookingService(entities: entities);
        }

        [Theory]
        [InlineData("chama@gmail.com", 2)]
        [InlineData("aa@bb.com", 2)]
        public void Books_flight(string passengerEmail, int numberOfSeats)
        {
            var flight = new Flight(3);
            entities.Flights.Add(flight);

            bookingService.Book(new BookDto(flight.Id, passengerEmail, numberOfSeats));
            bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(new BookingRm(passengerEmail, numberOfSeats));
        }
         
        [Theory]
        [InlineData(3)]
        [InlineData(10)]
        public void Cancels_booking(int initialCapacity)
        {
            //Given
            var flight = new Flight(initialCapacity);
            entities.Flights.Add(flight);
            bookingService.Book(new BookDto(flightId: flight.Id, passengerEmail: "chama@gmail.com", 2));

            //When
            bookingService.CancelBooking(
                    new CancelBookingDto(
                        flightId: flight.Id,
                        passengerEmail: "chama@gmail.com",
                        numberOfSeats: 2)
                    );

            //Then
            bookingService.GetRemainingNumberOfSeatsFor(flight.Id).Should().Be(initialCapacity);
        }
    }

    

}
