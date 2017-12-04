using System;
using DotNetCore.Data.Interfaces;

namespace DotNetCore.Data.Database
{
    public class DbFactory : IDisposable, IDbFactory
    {
        private DotNetCoreContext _dbContext;

        public DotNetCoreContext GetDbContext()
        {
            return _dbContext ?? (_dbContext = new DotNetCoreContext());
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}