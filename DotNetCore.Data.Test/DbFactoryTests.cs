using DotNetCore.Data.Database;
using DotNetCore.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace DotNetCore.Data.Test
{
    public class DbFactoryTests : IDisposable
    {
        public DbFactoryTests() { }

        public void Dispose() {}

        [Fact]
        public void DbFactory_CreateDbFactory_ReturnsInstance()
        {
            var dbFactory = new DbFactory();

            Assert.IsAssignableFrom<IDbFactory>(dbFactory);
        }

        [Fact]
        public void DbFactory_GetDbContext_ReturnsDbFactory()
        {
            var dbFactory = new DbFactory();

            Assert.IsAssignableFrom<DotNetCoreContext>(dbFactory.Context());
        }

        [Fact]
        public void DbFactory_ConnectToInMemoryDatabase_ReturnsDbContext()
        {
            var options = new DbContextOptionsBuilder<DotNetCoreContext>()
                .UseInMemoryDatabase(databaseName: "DotNetCoreDataTestDb")
                .Options;

            var dbFactory = new DbFactory(options);
            Assert.IsAssignableFrom<DotNetCoreContext>(dbFactory.Context());
        }
    }

}
