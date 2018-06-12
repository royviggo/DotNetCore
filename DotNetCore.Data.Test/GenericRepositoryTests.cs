using DotNetCore.Data.Database;
using DotNetCore.Data.Entities;
using DotNetCore.Data.Enums;
using DotNetCore.Data.Interfaces;
using DotNetCore.Data.Repositories;
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
        private IPersonRepository personRepository;

        public GenericRepositoryTests()
        {
            dbFactory = new DbFactory(options);
            dbContext = dbFactory.Context();
            personRepository = new PersonRepository(dbFactory);

            personRepository.Add(new Person { PersonId = 1, FirstName = "Test", LastName = "Tester", Gender = Gender.Unknown, Status = Status.Unknown });
            personRepository.Add(new Person { PersonId = 2, FirstName = "Foo", LastName = "Tester", Gender = Gender.Female, Status = Status.Living });
            personRepository.Add(new Person { PersonId = 3, FirstName = "Bar", LastName = "Tester", Gender = Gender.Male, Status = Status.Living });

            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            foreach (var person in personRepository.GetAllInclude())
                personRepository.Remove(person);

            dbContext.SaveChanges();

            personRepository.Dispose();
            dbFactory.Dispose();
        }

        [Fact]
        public void GenericRepository_GetPerson_ReturnsPerson()
        {
            var person = personRepository.Get(1);

            Assert.NotNull(personRepository);
            Assert.NotNull(person);
            Assert.Equal(1, person.PersonId);
            Assert.Equal("Test", person.FirstName);
        }

        [Fact]
        public void GenericRepository_UpdatePerson_ReturnsPerson()
        {
            var person = personRepository.Get(1);

            person.LastName = "Testing";
            person.Gender = Gender.Male;
            person.Status = Status.Living;

            dbContext.SaveChanges();

            var person2 = personRepository.Get(1);

            Assert.Equal("Testing", person.LastName);
            Assert.Equal(Gender.Male, person.Gender);
            Assert.Equal(Status.Living, person.Status);
        }

        [Fact]
        public void GenericRepository_DeletePerson_ReturnsNull()
        {
            var person = personRepository.Get(1);

            personRepository.Remove(person);

            dbContext.SaveChanges();

            var person2 = personRepository.Get(1);

            Assert.Equal(1, person.PersonId);
            Assert.Null(person2);
        }

        [Fact]
        public void GenericRepository_GetAll_ReturnsListOfPerson()
        {
            var people = personRepository.GetAllInclude().OrderBy(m => m.PersonId).ToList();

            Assert.Equal(3, people.Count);
            Assert.Equal(1, people[0].PersonId);
            Assert.Equal(2, people[1].PersonId);
            Assert.Equal(3, people[2].PersonId);
        }

        [Fact]
        public void GenericRepository_GetList_ReturnsListOfPerson()
        {
            var people = personRepository.Find(m => m.Status == Status.Living).OrderBy(o => o.LastName).ThenBy(o => o.FirstName).ToList();

            Assert.Equal(2, people.Count);
            Assert.Equal(3, people[0].PersonId);
            Assert.Equal(2, people[1].PersonId);
        }

    }
}
