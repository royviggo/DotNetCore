using DotNetCore.Data.Database;
using DotNetCore.Data.Entities;
using DotNetCore.Data.Interfaces;
using System;

namespace DotNetCore
{
    public class Program
    {
        public static void Main()
        {
            var db = new DotNetCoreContext();
            var personRepo = new GenericRepository<Person>(db);

            foreach (var person in personRepo.GetAll())
            {
                Console.WriteLine("{0}, {1} ({2}-{3}) {4}, {5}", person.LastName, person.FirstName, person.BornYear, person.DeathYear, person.Gender, person.Status);
            }
        }
    }
}
