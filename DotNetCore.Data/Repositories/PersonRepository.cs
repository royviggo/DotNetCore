using DotNetCore.Data.Database;
using DotNetCore.Data.Entities;
using DotNetCore.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DotNetCore.Data.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(DotNetCoreContext context) : base(context) { }

        public DotNetCoreContext DbContext => _dbContext as DotNetCoreContext;

        public IEnumerable<Person> GetAllInclude()
        {
            return DbContext.Set<Person>()
                            .Include(e => e.Events).ThenInclude(et => et.EventType)
                            .Include(e => e.Events).ThenInclude(p => p.Place);
        }
    }
}
