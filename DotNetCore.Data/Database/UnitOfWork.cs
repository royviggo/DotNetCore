﻿using System;
using DotNetCore.Data.Interfaces;
using DotNetCore.Data.Entities;
using DotNetCore.Data.Repositories;

namespace DotNetCore.Data.Database
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public UnitOfWork(IDbFactory dbFactory)
        {
            DbContext = dbFactory.GetDbContext();
        }

        public DotNetCoreContext DbContext { get; }

        private IGenericRepository<Event> _eventRepository;
        public IGenericRepository<Event> EventRepository => _eventRepository ?? (_eventRepository = new GenericRepository<Event>(DbContext));

        private IGenericRepository<EventType> _eventTypeRepository;
        public IGenericRepository<EventType> EventTypeRepository => _eventTypeRepository ?? (_eventTypeRepository = new GenericRepository<EventType>(DbContext));

        private IPersonRepository _personRepository;
        public IPersonRepository PersonRepository => _personRepository ?? (_personRepository = new PersonRepository(DbContext));

        private IGenericRepository<Place> _placeRepository;
        public IGenericRepository<Place> PlaceRepository => _placeRepository ?? (_placeRepository = new GenericRepository<Place>(DbContext));

        public void Save()
        {
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}