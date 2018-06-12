using DotNetCore.Data.Entities;

namespace DotNetCore.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IEventRepository Events { get; }
        IPersonRepository Persons { get; }

        IGenericRepository<EventType> EventTypes { get; }
        IGenericRepository<Place> Places { get; }

        void Save();
        void Dispose();
    }
}