using Microsoft.EntityFrameworkCore;
using DotNetCore.Data.Entities;

namespace DotNetCore.Data.Database
{
    public class DotNetCoreContext : DbContext
    {
        public DotNetCoreContext()
        {
        }

        public DotNetCoreContext(DbContextOptions<DotNetCoreContext> options) : base(options)
        {
        }

        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Place> Places { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().ToTable("Event");
            modelBuilder.Entity<EventType>().ToTable("EventType");
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<Place>().ToTable("Place");

            modelBuilder.Entity<Event>().OwnsOne(e => e.Date, d =>
            {
                d.OwnsOne(c => c.DateFrom);
                d.OwnsOne(c => c.DateTo);
            });

            modelBuilder.Entity<Event>()
                .HasOne(e => e.EventType);

            base.OnModelCreating(modelBuilder);
        }
    }
}