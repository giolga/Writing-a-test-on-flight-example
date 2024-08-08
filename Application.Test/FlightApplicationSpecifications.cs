using Xunit;
using System.Collections.Generic;
using FluentAssertions;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Test
{
    public class FlightApplicationSpecifications
    {
        [Theory]
        [InlineData("chama@gmail.com", 2)]
        [InlineData("aa@bb.com", 2)]
        public void Books_flight(string passengerEmail, int numberOfSeats)
        {
            var entities = new Entities(new DbContextOptionsBuilder<Entities>().UseInMemoryDatabase("Flights").Options);

            var flight = new Flight(3);
            entities.Flights.Add(flight);
            var bookingService = new BookingService(entities: entities);

            bookingService.Book(new BookDto(flight.Id, passengerEmail, numberOfSeats));
            bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(new BookingRm(passengerEmail, numberOfSeats));
        }
    }

    public class BookingService
    {
        public BookingService(Entities entities)
        {

        }

        public void Book(BookDto bookDto)
        {

        }

        public IEnumerable<BookingRm> FindBookings(Guid flightId)
        {
            return new[]
            {
                    new BookingRm(passengerEmail: "chama@gmail.com", numberOfSeats: 2)
            };
        }
    }

    public class BookDto
    {
        public BookDto(Guid flightId, string passengerEmail, int numberOfSeats)
        {

        }
    }

    public class BookingRm
    {
        public string PassengerEmail { get; set; }
        public int NumberOfSeats { get; set; }

        public BookingRm(string passengerEmail, int numberOfSeats)
        {
            this.PassengerEmail = passengerEmail;
            this.NumberOfSeats = numberOfSeats;
        }
    }


}