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

        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Privat\\Slekt\\Data\\sqlitetest.sqlite");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("person");
            base.OnModelCreating(modelBuilder);
        }
    }
}