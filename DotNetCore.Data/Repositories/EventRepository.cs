using DotNetCore.Data.Entities;
using DotNetCore.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCore.Data.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Event> GetAllInclude()
        {
            return Db.Context().Set<Event>()
                               .Include(e => e.Person)
                               .Include(e => e.EventType)
                               .Include(e => e.Place)
                               .AsNoTracking();
        }

        public IEnumerable<Event> GetByPersonId(int personId)
        {
            return Db.Context().Set<Event>()
                               .Where(e => e.PersonId == personId)
                               .Include(e => e.Person)
                               .Include(e => e.EventType)
                               .Include(e => e.Place);
        }
    }
}
