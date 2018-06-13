using DotNetCore.Data.Database;
using DotNetCore.Data.Entities;
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
        private IGenericRepository<Place> repository;

        public GenericRepositoryTests()
        {
            dbFactory = new DbFactory(options);
            repository = new GenericRepository<Place>(dbFactory);

            repository.Add(new Place { PlaceId = 1, Name = "Place 1" });
            repository.Add(new Place { PlaceId = 2, Name = "Place 2" });
            repository.Add(new Place { PlaceId = 3, Name = "Place 3" });

            dbFactory.Context().SaveChanges();
        }

        public void Dispose()
        {
            foreach (var place in repository.GetAll())
                repository.Remove(place);

            dbFactory.Context().SaveChanges();

            repository.Dispose();
            dbFactory.Dispose();
        }

        [Fact]
        public void GenericRepository_Get_ReturnsObject()
        {
            var place = repository.Get(1);

            Assert.NotNull(repository);
            Assert.NotNull(place);
            Assert.Equal(1, place.PlaceId);
            Assert.Equal("Place 1", place.Name);
        }

        [Fact]
        public void GenericRepository_Update_ObjectIsChanged()
        {
            var place = repository.Get(1);

            place.Name = "Testing";
            dbFactory.Context().SaveChanges();

            var place2 = repository.Get(1);

            Assert.Equal("Testing", place.Name);
        }

        [Fact]
        public void GenericRepository_UpdateNoTracking_ObjectIsChanged()
        {
            using (var dbFactory = new DbFactory(options))
            {
                var repo = new GenericRepository<Place>(dbFactory);
                var place = repo.Find(p => p.PlaceId == 1).FirstOrDefault();

                place.Name = "Testing";
                repo.Update(place);
                dbFactory.Context().SaveChanges();

                var repoFind = new GenericRepository<Place>(new DbFactory(options));
                var place2 = repoFind.Find(p => p.PlaceId == 1).FirstOrDefault();

                Assert.Equal("Testing", place.Name);
            }
        }

        [Fact]
        public void GenericRepository_Find_StateIsDetached()
        {
            var place = repository.Find(p => p.PlaceId == 1).FirstOrDefault();

            Assert.Equal("Detached", dbFactory.Context().Entry(place).State.ToString());
        }

        [Fact]
        public void GenericRepository_Remove_ObjectIsRemoved()
        {
            var place = repository.Get(1);

            repository.Remove(place);
            dbFactory.Context().SaveChanges();

            var place2 = repository.Get(1);

            Assert.Equal(1, place.PlaceId);
            Assert.Null(place2);
        }

        [Fact]
        public void GenericRepository_GetAll_ReturnsList()
        {
            var people = repository.GetAll().OrderBy(m => m.PlaceId).ToList();

            Assert.Equal(3, people.Count);
            Assert.Equal(1, people[0].PlaceId);
            Assert.Equal(2, people[1].PlaceId);
            Assert.Equal(3, people[2].PlaceId);
        }

        [Fact]
        public void GenericRepository_Find_ReturnsList()
        {
            var people = repository.Find(m => m.PlaceId >= 2).OrderBy(m => m.PlaceId).ToList();

            Assert.Equal(2, people.Count);
            Assert.Equal(2, people[0].PlaceId);
        }

    }
}
