using DotNetCore.Data.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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
            var persons = unitOfWork.PersonRepository.GetAll()
                                    .Include(e => e.Events).ThenInclude(et => et.EventType)
                                    .Include(e => e.Events).ThenInclude(p => p.Place);

            foreach (var person in persons)
            {
                Console.WriteLine("{0}, {1} ({2}-{3}) {4}, {5}", person.LastName, person.FirstName, person.BornYear, person.DeathYear, person.Gender, person.Status);

                foreach (var e in person.Events.OrderBy(m => m.Date))
                {
                    Console.WriteLine("     {0} {1}: {2} - {3} - {4}", e.EventTypeId, e.EventType?.Name, e.Date, e.Place?.Name, e.Description ?? "");
                }
            }
        }
    }
}
