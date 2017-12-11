using DotNetCore.Data.Database;
using DotNetCore.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DotNetCore.Data.Test
{
    [TestFixture]
    public class DbFactoryTests
    {
        [Test]
        public void DbFactory_CreateDbFactory_ReturnsInstance()
        {
            var dbFactory = new DbFactory();

            Assert.IsInstanceOf<IDbFactory>(dbFactory);
        }

        [Test]
        public void DbFactory_GetDbContext_ReturnsDbFactory()
        {
            var dbFactory = new DbFactory();

            Assert.IsInstanceOf<DotNetCoreContext>(dbFactory.GetDbContext());
        }

        [Test]
        public void DbFactory_ConnectToInMemoryDatabase_ReturnsDbContext()
        {
            var options = new DbContextOptionsBuilder<DotNetCoreContext>()
                .UseInMemoryDatabase(databaseName: "DotNetCoreDataTestDb")
                .Options;

            var dbFactory = new DbFactory(options);
            Assert.IsInstanceOf<DotNetCoreContext>(dbFactory.GetDbContext());
        }
    }

}
