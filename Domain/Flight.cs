namespace Domain
{
    public class Flight
    {

        private List<Booking> bookingList = new();

        public IEnumerable<Booking> BookingList => bookingList;
        //public List<Booking> BookingList { get; set; } = new List<Booking>();
        public int RemainingNumberOfSeats { get; set; }

        public Flight(int seatsCapacity)
        {
            RemainingNumberOfSeats = seatsCapacity;
        }

        public object? Book(string passengerEmail, int numberOfSeats)
        {

            if(numberOfSeats > this.RemainingNumberOfSeats)
            {
                return new OverbookingError();
            }

            RemainingNumberOfSeats -= numberOfSeats;
            bookingList.Add(new Booking(passengerEmail, numberOfSeats));


            return null;
        }
    }
}
