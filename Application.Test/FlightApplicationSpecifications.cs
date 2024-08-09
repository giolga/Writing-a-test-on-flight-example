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
        public Entities Entities { get; set; }
        public BookingService(Entities entities)
        {
            Entities = entities;
        }

        public void Book(BookDto bookDto)
        {
            var flight = Entities.Flights.Find(bookDto.FlightId);
            flight.Book(bookDto.PassengerEmail, bookDto.NumberOfseats);
            Entities.SaveChanges();
        }

        public IEnumerable<BookingRm> FindBookings(Guid flightId)
        {
            return Entities.Flights.Find(flightId).BookingList.Select(booking => new BookingRm(booking.Email, booking.NumberOfSeats));
        }
    }

    public class BookDto
    {
        public Guid FlightId { get; set; }
        public string PassengerEmail { get; set; }
        public int NumberOfseats { get; set; }
        public BookDto(Guid flightId, string passengerEmail, int numberOfSeats)
        {
            FlightId = flightId;
            PassengerEmail = passengerEmail;
            NumberOfseats = numberOfSeats;
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