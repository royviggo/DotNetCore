using DotNetCore.Data.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DotNetCore.BLL.Interfaces
{
    public interface IPersonService
    {
        void Dispose();

        void Create(Person person);
        void Update(Person person);
        void Delete(Person person);

        Person GetById(int id);
        Person GetByIdNoTracking(int id);

        IQueryable<Person> GetAll();
        IQueryable<Person> GetList(Expression<Func<Person, bool>> predicate = null,
                                    Func<IQueryable<Person>, IOrderedQueryable<Person>> orderBy = null,
                                   Func<IQueryable<Person>, IIncludableQueryable<Person, object>> include = null);
    }
}
