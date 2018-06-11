using DotNetCore.Data.Entities;

namespace DotNetCore.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Event> EventRepository { get; }
        IGenericRepository<EventType> EventTypeRepository { get; }
        IPersonRepository PersonRepository { get; }
        IGenericRepository<Place> PlaceRepository { get; }

        void Save();
        void Dispose();
    }
}