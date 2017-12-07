using DotNetCore.Data.Database;
using DotNetCore.Data.Entities;
using DotNetCore.Data.Interfaces;
using DotNetCore.Data.Enums;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DotNetCore.Data.Test
{
    [TestFixture]
    public class DotNetCoreDataTests
    {
        [Test]
        public void UnitOfWork_IsInstanceOf_IUnitOfWork()
        {
            var unitOfWork = new UnitOfWork(new DbFactory());

            Assert.IsInstanceOf<IUnitOfWork>(unitOfWork);
        }

        [Test]
        public void DbFactory_CreateDbFactory()
        {
            var dbFactory = new DbFactory();

            Assert.IsInstanceOf<IDbFactory>(dbFactory);
        }

        [Test]
        public void DbFactory_GetDbContext()
        {
            var dbFactory = new DbFactory();

            Assert.IsInstanceOf<DotNetCoreContext>(dbFactory.GetDbContext());
        }

        [Test]
        public void DbFactory_ConnectToInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<DotNetCoreContext>()
                .UseInMemoryDatabase(databaseName: "DotNetCoreDataTestDb")
                .Options;

            var dbFactory = new DbFactory(options);
            Assert.IsInstanceOf<DotNetCoreContext>(dbFactory.GetDbContext());
        }

        [Test]
        public void GenericRepository_CreatePerson_AddAndFind()
        {
            var options = new DbContextOptionsBuilder<DotNetCoreContext>()
                .UseInMemoryDatabase(databaseName: "DotNetCoreDataTestDb")
                .Options;

            var dbFactory = new DbFactory(options);
            var dbContext = dbFactory.GetDbContext();

            var personRepository = new GenericRepository<Person>(dbContext);
            personRepository.Add(new Person
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Tester",
                Gender = Gender.Unknown,
                Status = Status.Unknown
            });

            var person = personRepository.GetById(1);

            Assert.IsNotNull(personRepository);
            Assert.IsNotNull(person);
            Assert.AreEqual(1, person.Id);
            Assert.AreEqual("Test", person.FirstName);
        }

    }

}
