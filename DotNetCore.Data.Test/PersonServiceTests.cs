using DotNetCore.Data.Database;
using DotNetCore.Data.Entities;
using DotNetCore.Data.Interfaces;
using DotNetCore.Data.Enums;
using DotNetCore.BLL.Interfaces;
using DotNetCore.BLL.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace DotNetCore.Data.Test
{
    [TestFixture]
    class PersonServiceTests
    {
        private static DbContextOptions<DotNetCoreContext> options = new DbContextOptionsBuilder<DotNetCoreContext>()
            .UseInMemoryDatabase(databaseName: "DotNetCoreDataTestDb")
            .Options;

        private static DbFactory dbFactory = new DbFactory(options);
        private static IUnitOfWork unitOfWork = new UnitOfWork(dbFactory);
        private IPersonService personService = new PersonService(unitOfWork);

        [SetUp]
        public void Setup()
        {
            personService.Create(new Person { Id = 1, FirstName = "Test", LastName = "Tester", Gender = Gender.Unknown, Status = Status.Unknown });
            personService.Create(new Person { Id = 2, FirstName = "Foo", LastName = "Tester", Gender = Gender.Female, Status = Status.Living });
            personService.Create(new Person { Id = 3, FirstName = "Bar", LastName = "Tester", Gender = Gender.Male, Status = Status.Living });
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var person in personService.GetAll())
            {
                personService.Delete(person);
            }
        }


        [Test]
        public void PersonService_IsInstanceOf_ReturnsIPersonService()
        {
            var pService = new PersonService(unitOfWork);

            Assert.IsInstanceOf<IPersonService>(pService);
        }

        [Test]
        public void PersonService_CreatePerson_ReturnsPerson()
        {
            var person = personService.GetById(1);

            Assert.IsNotNull(person);
            Assert.AreEqual(1, person.Id);
            Assert.AreEqual("Test", person.FirstName);
        }

        [Test]
        public void PersonService_UpdatePerson_ReturnsPerson()
        {
            var person = personService.GetById(1);

            person.LastName = "Testing";
            person.Gender = Gender.Male;
            person.Status = Status.Living;

            personService.Update(person);

            var person2 = personService.GetById(1);

            Assert.AreEqual("Testing", person.LastName);
            Assert.AreEqual(Gender.Male, person.Gender);
            Assert.AreEqual(Status.Living, person.Status);
        }

        [Test]
        public void PersonService_DeletePerson_ReturnsNull()
        {
            var person = personService.GetById(1);

            personService.Delete(person);

            var person2 = personService.GetById(1);

            Assert.AreEqual(1, person.Id);
            Assert.AreEqual(null, person2);
        }

        [Test]
        public void PersonService_GetAll_ReturnsListOfPerson()
        {
            var people = personService.GetAll().OrderBy(m => m.Id).ToList();

            Assert.AreEqual(3, people.Count);
            Assert.AreEqual(1, people[0].Id);
            Assert.AreEqual(2, people[1].Id);
            Assert.AreEqual(3, people[2].Id);
        }

        [Test]
        public void PersonService_GetList_ReturnsListOfPerson()
        {
            var people = personService.GetList(m => m.Status == Status.Living, m => m.OrderBy(o => o.LastName).ThenBy(o => o.FirstName)).ToList();

            Assert.AreEqual(2, people.Count);
            Assert.AreEqual(3, people[0].Id);
            Assert.AreEqual(2, people[1].Id);
        }

    }
}
