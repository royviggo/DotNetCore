using DotNetCore.Data.Database;
using DotNetCore.Data.Entities;
using DotNetCore.Data.Interfaces;
using DotNetCore.Data.Enums;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DotNetCore.Data.Test
{
    [TestFixture]
    class UnitOfWorkTests
    {
        private static DbContextOptions<DotNetCoreContext> options = new DbContextOptionsBuilder<DotNetCoreContext>()
            .UseInMemoryDatabase(databaseName: "DotNetCoreDataTestDb")
            .Options;

        private static DbFactory dbFactory = new DbFactory(options);
        private UnitOfWork unitOfWork = new UnitOfWork(dbFactory);

        [SetUp]
        public void Setup()
        {
            unitOfWork.PersonRepository.Add(new Person { Id = 1, FirstName = "Test", LastName = "Tester", Gender = Gender.Unknown, Status = Status.Unknown });
            unitOfWork.PersonRepository.Add(new Person { Id = 2, FirstName = "Foo", LastName = "Tester", Gender = Gender.Female, Status = Status.Living });
            unitOfWork.PersonRepository.Add(new Person { Id = 3, FirstName = "Bar", LastName = "Tester", Gender = Gender.Male, Status = Status.Living });

            unitOfWork.Save();
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var person in unitOfWork.PersonRepository.GetAll())
            {
                unitOfWork.PersonRepository.Delete(person);
            }
            unitOfWork.Save();
        }


        [Test]
        public void UnitOfWork_IsInstanceOf_ReturnsIUnitOfWork()
        {
            var unitOfWork = new UnitOfWork(new DbFactory());

            Assert.IsInstanceOf<IUnitOfWork>(unitOfWork);
        }

        [Test]
        public void UnitOfWork_CreatePerson_ReturnsPerson()
        {
            var person = unitOfWork.PersonRepository.GetById(1);

            Assert.IsNotNull(person);
            Assert.AreEqual(1, person.Id);
            Assert.AreEqual("Test", person.FirstName);
        }

    }
}
