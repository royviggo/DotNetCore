using System;
using DotNetCore.Data.Interfaces;
using DotNetCore.Data.Entities;

namespace DotNetCore.Data.Database
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public DotNetCoreContext DbContext { get; }

        public UnitOfWork(IDbFactory dbFactory)
        {
            DbContext = dbFactory.GetDbContext();
        }

        private IRepository<Person> _personRepository;
        public IRepository<Person> PersonRepository => _personRepository ?? (_personRepository = new GenericRepository<Person>(DbContext));

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