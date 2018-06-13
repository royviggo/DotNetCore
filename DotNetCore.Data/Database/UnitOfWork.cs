using DotNetCore.Data.Interfaces;
using DotNetCore.Data.Repositories;

namespace DotNetCore.Data.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
        }

        public IDbFactory DbFactory { get; }

        private IEventRepository _eventRepository;
        public IEventRepository Events => _eventRepository ?? (_eventRepository = new EventRepository(DbFactory));

        private IEventTypeRepository _eventTypeRepository;
        public IEventTypeRepository EventTypes => _eventTypeRepository ?? (_eventTypeRepository = new EventTypeRepository(DbFactory));

        private IPersonRepository _personRepository;
        public IPersonRepository Persons => _personRepository ?? (_personRepository = new PersonRepository(DbFactory));

        private IPlaceRepository _placeRepository;
        public IPlaceRepository Places => _placeRepository ?? (_placeRepository = new PlaceRepository(DbFactory));

        public void Save()
        {
            DbFactory.Context().SaveChanges();
        }

        public void Dispose()
        {
            DbFactory.Dispose();

        }
    }
}