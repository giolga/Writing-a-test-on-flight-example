namespace Domain
{
    public class Flight
    {

        private List<Booking> bookingList = new();

        public IEnumerable<Booking> BookingList => bookingList;
        //public List<Booking> BookingList { get; set; } = new List<Booking>();
        public int RemainingNumberOfSeats { get; set; }

        public Guid Id { get; }

        [Obsolete("Needed for EF")]
        Flight() { }
        public Flight(int seatsCapacity)
        {
            RemainingNumberOfSeats = seatsCapacity;
        }

        public object? Book(string passengerEmail, int numberOfSeats)
        {

            if (numberOfSeats > this.RemainingNumberOfSeats)
            {
                return new OverbookingError();
            }

            RemainingNumberOfSeats -= numberOfSeats;
            bookingList.Add(new Booking(passengerEmail, numberOfSeats));


            return null;
        }

        public object? CancelBooking(string passengerEmail, int numberOfSeats)
        {
            if (!bookingList.Any(booking => booking.Email == passengerEmail))
            {
                return new BookingNotFoundError();
            }

            RemainingNumberOfSeats += numberOfSeats;
            return null;
        }
    }
}
