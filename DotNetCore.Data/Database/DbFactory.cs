using System;
using DotNetCore.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore.Data.Database
{
    public class DbFactory : IDisposable, IDbFactory
    {
        private DotNetCoreContext _dbContext;

        public DbFactory()
        {
        }

        public DbFactory(DbContextOptions<DotNetCoreContext> options)
        {
            _dbContext = new DotNetCoreContext(options);
        }

        public DotNetCoreContext GetDbContext()
        {
            return _dbContext ?? (_dbContext = new DotNetCoreContext());
        }

        public DotNetCoreContext GetDbContext(DbContextOptions<DotNetCoreContext> options)
        {
            return _dbContext ?? (_dbContext = new DotNetCoreContext(options));
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}