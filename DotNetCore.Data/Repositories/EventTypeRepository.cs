using DotNetCore.Data.Entities;
using DotNetCore.Data.Interfaces;

namespace DotNetCore.Data.Repositories
{
    public class EventTypeRepository : GenericRepository<EventType>, IEventTypeRepository
    {
        public EventTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
