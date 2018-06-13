using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using DotNetCore.Data.Database;

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
            var persons = unitOfWork.Persons.GetAllInclude();

            foreach (var person in persons)
            {
                Console.WriteLine("{0}, {1} ({2}-{3}) {4}, {5}", person.LastName, person.FirstName, person.BornYear, person.DeathYear, person.Gender, person.Status);

                foreach (var e in person.Events.OrderBy(m => m.Date))
                {
                    Console.WriteLine("     {0} {1}: {2} - {3} - {4}", e.EventTypeId, e.EventType?.Name, e.Date, e.Place?.Name, e.Description ?? "");
                }
            }

            var events = unitOfWork.Events.GetByPersonId(1);

            foreach (var e in events)
            {
                Console.WriteLine("{0}, {1} | {2} {3} {4}", e.EventId, e.EventType.EventTypeId, e.EventType.Name, e.Date, e.Place?.Name);
            }

            var eventTypes = unitOfWork.EventTypes.GetAll();

            foreach (var et in eventTypes)
            {
                Console.WriteLine("{0} | {1} - {2}", et.EventTypeId, et.Name, et.Sentence);
            }

            var places = unitOfWork.Places.GetAll();

            foreach (var p in places)
            {
                Console.WriteLine("{0} | {1}", p.PlaceId, p.Name);
            }
        }
    }
}
