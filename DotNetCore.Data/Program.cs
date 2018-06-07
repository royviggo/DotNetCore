using DotNetCore.Data.Database;
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

            var unitOfWork = new UnitOfWork(new DbFactory(options));

            foreach (var person in unitOfWork.PersonRepository.GetAll())
            {
                Console.WriteLine("{0}, {1} ({2}-{3}) {4}, {5}", person.LastName, person.FirstName, person.BornYear, person.DeathYear, person.Gender, person.Status);
            }
        }
    }
}
