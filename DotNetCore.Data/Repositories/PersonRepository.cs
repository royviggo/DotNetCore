using DotNetCore.Data.Entities;
using DotNetCore.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DotNetCore.Data.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Person> GetAllInclude()
        {
            return Db.Context().Set<Person>()
                               .Include(p => p.Events).ThenInclude(e => e.EventType)
                               .Include(p => p.Events).ThenInclude(e => e.Place);
        }
    }
}
