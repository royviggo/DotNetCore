using DotNetCore.Data.Database;
using System;

namespace DotNetCore
{
    public class Program
    {
        public static void Main()
        {
            using (var db = new DotNetCoreContext())
            {
                foreach (var person in db.Persons)
                {
                    Console.WriteLine("{0}, {1} ({2}-{3}) {4}, {5}", person.LastName, person.FirstName, person.BornYear, person.DeathYear, person.Gender, person.Status);
                }
            }
        }
    }
}
