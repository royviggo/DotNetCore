using DotNetCore.Data.Entities;

namespace DotNetCore.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Event> Events { get; }
        IGenericRepository<EventType> EventTypes { get; }
        IPersonRepository Persons { get; }
        IGenericRepository<Place> Places { get; }

        void Save();
        void Dispose();
    }
}