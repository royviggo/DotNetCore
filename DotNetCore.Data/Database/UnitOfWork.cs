using System;
using DotNetCore.Data.Interfaces;
using DotNetCore.Data.Entities;

namespace DotNetCore.Data.Database
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public UnitOfWork(IDbFactory dbFactory)
        {
            DbContext = dbFactory.GetDbContext();
        }

        public DotNetCoreContext DbContext { get; }

        private IGenericRepository<Person> _personRepository;
        public IGenericRepository<Person> PersonRepository => _personRepository ?? (_personRepository = new GenericRepository<Person>(DbContext));

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