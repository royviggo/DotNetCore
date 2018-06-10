using DotNetCore.Data.Database;
using DotNetCore.Data.Entities;
using DotNetCore.Data.Interfaces;
using DotNetCore.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace DotNetCore.Data.Test
{
    public class UnitOfWorkTests : IDisposable
    {
        private DbContextOptions<DotNetCoreContext> options = new DbContextOptionsBuilder<DotNetCoreContext>()
            .UseInMemoryDatabase(databaseName: "UnitOfWorkTestDb")
            .Options;

        private IDbFactory dbFactory;
        private IUnitOfWork unitOfWork;

        public UnitOfWorkTests()
        {
            dbFactory = new DbFactory(options);
            unitOfWork = new UnitOfWork(dbFactory);

            unitOfWork.PersonRepository.Add(new Person { PersonId = 1, FirstName = "Test", LastName = "Tester", Gender = Gender.Unknown, Status = Status.Unknown });
            unitOfWork.PersonRepository.Add(new Person { PersonId = 2, FirstName = "Foo", LastName = "Tester", Gender = Gender.Female, Status = Status.Living });
            unitOfWork.PersonRepository.Add(new Person { PersonId = 3, FirstName = "Bar", LastName = "Tester", Gender = Gender.Male, Status = Status.Living });

            unitOfWork.Save();
        }

        public void Dispose()
        {
            foreach (var person in unitOfWork.PersonRepository.GetAll())
                unitOfWork.PersonRepository.Delete(person);

            unitOfWork.Save();

            unitOfWork.Dispose();
            dbFactory.Dispose();
        }

        [Fact]
        public void UnitOfWork_IsInstanceOf_ReturnsIUnitOfWork()
        {
            var unitOfWork = new UnitOfWork(new DbFactory());

            Assert.IsAssignableFrom<IUnitOfWork>(unitOfWork);
        }

        [Fact]
        public void UnitOfWork_GetPerson_ReturnsPerson()
        {
            var person = unitOfWork.PersonRepository.GetById(1);

            Assert.NotNull(person);
            Assert.Equal(1, person.PersonId);
            Assert.Equal("Test", person.FirstName);
        }
    }
}
