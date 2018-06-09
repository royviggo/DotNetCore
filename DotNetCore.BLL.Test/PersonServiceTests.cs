using System;
using DotNetCore.Data.Database;
using DotNetCore.Data.Entities;
using DotNetCore.Data.Interfaces;
using DotNetCore.Data.Enums;
using DotNetCore.BLL.Interfaces;
using DotNetCore.BLL.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace DotNetCore.BLL.Test
{
    public class PersonServiceTests : IDisposable
    {
        private DbContextOptions<DotNetCoreContext> options = new DbContextOptionsBuilder<DotNetCoreContext>()
           .UseInMemoryDatabase(databaseName: "PersonServiceTestDb")
           .Options;

        private IPersonService personService;
        private IUnitOfWork unitOfWork;
        private IDbFactory dbFactory;

        public PersonServiceTests()
        {
            dbFactory = new DbFactory(options);
            unitOfWork = new UnitOfWork(dbFactory);
            personService = new PersonService(unitOfWork);

            personService.Create(new Person { Id = 1, FirstName = "Test", LastName = "Tester", Gender = Gender.Unknown, Status = Status.Unknown });
            personService.Create(new Person { Id = 2, FirstName = "Foo", LastName = "Tester", Gender = Gender.Female, Status = Status.Living });
            personService.Create(new Person { Id = 3, FirstName = "Bar", LastName = "Tester", Gender = Gender.Male, Status = Status.Living });
        }

        public void Dispose()
        {
            foreach (var person in personService.GetAll())
                personService.Delete(person);

            personService.Dispose();
            unitOfWork.Dispose();
            dbFactory.Dispose();
        }


        [Fact]
        public void PersonService_IsInstanceOf_ReturnsIPersonService()
        {
            var pService = new PersonService(unitOfWork);

            Assert.IsAssignableFrom<IPersonService>(pService);
        }

        [Fact]
        public void PersonService_GetPerson_ReturnsPerson()
        {
            var person = personService.GetById(1);

            Assert.NotNull(person);
            Assert.Equal(1, person.Id);
            Assert.Equal("Test", person.FirstName);
        }

        [Fact]
        public void PersonService_UpdatePerson_ReturnsPerson()
        {
            var person = personService.GetById(1);

            person.LastName = "Testing";
            person.Gender = Gender.Male;
            person.Status = Status.Living;

            personService.Update(person);

            var person2 = personService.GetById(1);

            Assert.Equal("Testing", person.LastName);
            Assert.Equal(Gender.Male, person.Gender);
            Assert.Equal(Status.Living, person.Status);
        }

        [Fact]
        public void PersonService_DeletePerson_ReturnsNull()
        {
            var person = personService.GetById(1);

            personService.Delete(person);

            var person2 = personService.GetById(1);

            Assert.Equal(1, person.Id);
            Assert.Null(person2);
        }

        [Fact]
        public void PersonService_GetAll_ReturnsListOfPerson()
        {
            var people = personService.GetAll().OrderBy(m => m.Id).ToList();

            Assert.Equal(3, people.Count);
            Assert.Equal(1, people[0].Id);
            Assert.Equal(2, people[1].Id);
            Assert.Equal(3, people[2].Id);
        }

        [Fact]
        public void PersonService_GetList_ReturnsListOfPerson()
        {
            var people = personService.GetList(m => m.Status == Status.Living, m => m.OrderBy(o => o.LastName).ThenBy(o => o.FirstName)).ToList();

            Assert.Equal(2, people.Count);
            Assert.Equal(3, people[0].Id);
            Assert.Equal(2, people[1].Id);
        }

    }
}
