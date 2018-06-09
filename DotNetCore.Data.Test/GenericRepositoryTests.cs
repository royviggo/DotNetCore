using DotNetCore.Data.Database;
using DotNetCore.Data.Entities;
using DotNetCore.Data.Enums;
using DotNetCore.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace DotNetCore.Data.Test
{
    public class GenericRepositoryTests : IDisposable
    {
        private DbContextOptions<DotNetCoreContext> options = new DbContextOptionsBuilder<DotNetCoreContext>()
           .UseInMemoryDatabase(databaseName: "GenericRepositoryTestDb")
           .Options;

        private IDbFactory dbFactory;
        private DotNetCoreContext dbContext;
        private IGenericRepository<Person> personRepository;

        public GenericRepositoryTests()
        {
            dbFactory = new DbFactory(options);
            dbContext = dbFactory.GetDbContext();
            personRepository = new GenericRepository<Person>(dbContext);

            personRepository.Add(new Person { Id = 1, FirstName = "Test", LastName = "Tester", Gender = Gender.Unknown, Status = Status.Unknown });
            personRepository.Add(new Person { Id = 2, FirstName = "Foo", LastName = "Tester", Gender = Gender.Female, Status = Status.Living });
            personRepository.Add(new Person { Id = 3, FirstName = "Bar", LastName = "Tester", Gender = Gender.Male, Status = Status.Living });

            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            foreach (var person in personRepository.GetAll())
                personRepository.Delete(person);

            dbContext.SaveChanges();

            personRepository.Dispose();
            dbFactory.Dispose();
        }

        [Fact]
        public void GenericRepository_GetPerson_ReturnsPerson()
        {
            var person = personRepository.GetById(1);

            Assert.NotNull(personRepository);
            Assert.NotNull(person);
            Assert.Equal(1, person.Id);
            Assert.Equal("Test", person.FirstName);
        }

        [Fact]
        public void GenericRepository_UpdatePerson_ReturnsPerson()
        {
            var person = personRepository.GetById(1);

            person.LastName = "Testing";
            person.Gender = Gender.Male;
            person.Status = Status.Living;

            dbContext.SaveChanges();

            var person2 = personRepository.GetById(1);

            Assert.Equal("Testing", person.LastName);
            Assert.Equal(Gender.Male, person.Gender);
            Assert.Equal(Status.Living, person.Status);
        }

        [Fact]
        public void GenericRepository_DeletePerson_ReturnsNull()
        {
            var person = personRepository.GetById(1);

            personRepository.Delete(person);

            dbContext.SaveChanges();

            var person2 = personRepository.GetById(1);

            Assert.Equal(1, person.Id);
            Assert.Null(person2);
        }

        [Fact]
        public void GenericRepository_GetAll_ReturnsListOfPerson()
        {
            var people = personRepository.GetAll().OrderBy(m => m.Id).ToList();

            Assert.Equal(3, people.Count);
            Assert.Equal(1, people[0].Id);
            Assert.Equal(2, people[1].Id);
            Assert.Equal(3, people[2].Id);
        }

        [Fact]
        public void GenericRepository_GetList_ReturnsListOfPerson()
        {
            var people = personRepository.GetList(m => m.Status == Status.Living, m => m.OrderBy(o => o.LastName).ThenBy(o => o.FirstName)).ToList();

            Assert.Equal(2, people.Count);
            Assert.Equal(3, people[0].Id);
            Assert.Equal(2, people[1].Id);
        }

    }
}
