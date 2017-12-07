using DotNetCore.Data.Database;
using DotNetCore.Data.Entities;
using DotNetCore.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace DotNetCore
{
    public class Program
    {
        public static void Main()
        {
            var options = new DbContextOptionsBuilder<DotNetCoreContext>()
                .UseSqlite("Data Source=C:\\Privat\\Slekt\\Data\\sqlitetest.sqlite")
                .Options;

            var dbFactory = new DbFactory(options);
            var personRepo = new GenericRepository<Person>(dbFactory.GetDbContext());

            foreach (var person in personRepo.GetAll())
            {
                Console.WriteLine("{0}, {1} ({2}-{3}) {4}, {5}", person.LastName, person.FirstName, person.BornYear, person.DeathYear, person.Gender, person.Status);
            }
        }
    }
}
