namespace Domain
{
    public class Flight
    {
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
            return null;
        }
    }
}
