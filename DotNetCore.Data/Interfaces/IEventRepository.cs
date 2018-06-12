using DotNetCore.Data.Entities;
using System.Collections.Generic;

namespace DotNetCore.Data.Interfaces
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        IEnumerable<Event> GetAllInclude();
        IEnumerable<Event> GetByPersonId(int id);
    }
}
