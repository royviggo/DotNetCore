using System;
using DotNetCore.Data.Interfaces;
using DotNetCore.Data.Entities;
using DotNetCore.Data.Repositories;

namespace DotNetCore.Data.Database
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public UnitOfWork(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
        }

        public IDbFactory DbFactory { get; }

        private IGenericRepository<Event> _eventRepository;
        public IGenericRepository<Event> Events => _eventRepository ?? (_eventRepository = new GenericRepository<Event>(DbFactory));

        private IGenericRepository<EventType> _eventTypeRepository;
        public IGenericRepository<EventType> EventTypes => _eventTypeRepository ?? (_eventTypeRepository = new GenericRepository<EventType>(DbFactory));

        private IPersonRepository _personRepository;
        public IPersonRepository Persons => _personRepository ?? (_personRepository = new PersonRepository(DbFactory));

        private IGenericRepository<Place> _placeRepository;
        public IGenericRepository<Place> Places => _placeRepository ?? (_placeRepository = new GenericRepository<Place>(DbFactory));

        public void Save()
        {
            DbFactory.Context().SaveChanges();
        }

        public void Dispose()
        {
            DbFactory.Context().Dispose();
            DbFactory.Dispose();
        }
    }
}