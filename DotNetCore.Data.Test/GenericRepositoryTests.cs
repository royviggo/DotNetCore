using DotNetCore.Data.Database;
using DotNetCore.Data.Entities;
using DotNetCore.Data.Enums;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace DotNetCore.Data.Test
{
    [TestFixture]
    public class GenericRepositoryTests
    {
        private static DbContextOptions<DotNetCoreContext> options = new DbContextOptionsBuilder<DotNetCoreContext>()
            .UseInMemoryDatabase(databaseName: "DotNetCoreDataTestDb")
            .Options;

        private static DbFactory dbFactory = new DbFactory(options);
        private static DotNetCoreContext dbContext = dbFactory.GetDbContext();

        GenericRepository<Person> personRepository = new GenericRepository<Person>(dbContext);

        [SetUp]
        public void Setup()
        {
            personRepository.Add(new Person { Id = 1, FirstName = "Test", LastName = "Tester", Gender = Gender.Unknown, Status = Status.Unknown });
            personRepository.Add(new Person { Id = 2, FirstName = "Foo", LastName = "Tester", Gender = Gender.Female, Status = Status.Living });
            personRepository.Add(new Person { Id = 3, FirstName = "Bar", LastName = "Tester", Gender = Gender.Male, Status = Status.Living });

            dbContext.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var person in personRepository.GetAll())
            {
                personRepository.Delete(person);
            }
            dbContext.SaveChanges();
        }


        [Test]
        public void GenericRepository_CreatePerson_ReturnsPerson()
        {
            var person = personRepository.GetById(1);

            Assert.IsNotNull(personRepository);
            Assert.IsNotNull(person);
            Assert.AreEqual(1, person.Id);
            Assert.AreEqual("Test", person.FirstName);
        }

        [Test]
        public void GenericRepository_UpdatePerson_ReturnsPerson()
        {
            var person = personRepository.GetById(1);

            person.LastName = "Testing";
            person.Gender = Gender.Male;
            person.Status = Status.Living;

            dbContext.SaveChanges();

            var person2 = personRepository.GetById(1);

            Assert.AreEqual("Testing", person.LastName);
            Assert.AreEqual(Gender.Male, person.Gender);
            Assert.AreEqual(Status.Living, person.Status);
        }

        [Test]
        public void GenericRepository_DeletePerson_ReturnsNull()
        {
            var person = personRepository.GetById(1);

            personRepository.Delete(person);

            dbContext.SaveChanges();

            var person2 = personRepository.GetById(1);

            Assert.AreEqual(1, person.Id);
            Assert.AreEqual(null, person2);
        }

        [Test]
        public void GenericRepository_GetAll_ReturnsListOfPerson()
        {
            var people = personRepository.GetAll().OrderBy(m => m.Id).ToList();

            Assert.AreEqual(3, people.Count);
            Assert.AreEqual(1, people[0].Id);
            Assert.AreEqual(2, people[1].Id);
            Assert.AreEqual(3, people[2].Id);
        }

        [Test]
        public void GenericRepository_GetList_ReturnsListOfPerson()
        {
            var people = personRepository.GetList(m => m.Status == Status.Living, m => m.OrderBy(o => o.LastName).ThenBy(o => o.FirstName)).ToList();

            Assert.AreEqual(2, people.Count);
            Assert.AreEqual(3, people[0].Id);
            Assert.AreEqual(2, people[1].Id);
        }

    }
}
