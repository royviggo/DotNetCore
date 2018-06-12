using DotNetCore.Data.Entities;
using DotNetCore.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DotNetCore.Data.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<Person> GetAllInclude()
        {
            return Db.Context().Set<Person>()
                               .Include(e => e.Events).ThenInclude(et => et.EventType)
                               .Include(e => e.Events).ThenInclude(p => p.Place);
        }
    }
}
