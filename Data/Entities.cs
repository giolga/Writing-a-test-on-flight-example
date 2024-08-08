using Microsoft.EntityFrameworkCore;
using Domain;

namespace Data
{
    public class Entities : DbContext
    {
        public DbSet<Flight> Flights => Set<Flight>();
    }
}
