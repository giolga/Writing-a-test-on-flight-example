using Microsoft.EntityFrameworkCore;
using Domain;

namespace Data
{
    public class Entities : DbContext
    {
        public Entities(DbContextOptions options) : base(options) { }
        public DbSet<Flight> Flights => Set<Flight>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>().HasKey(f => f.Id);
            modelBuilder.Entity<Flight>().OwnsMany(f => f.BookingList);
            base.OnModelCreating(modelBuilder);
        }
    }


}
