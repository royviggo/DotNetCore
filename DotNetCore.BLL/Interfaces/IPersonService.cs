using DotNetCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DotNetCore.BLL.Interfaces
{
    public interface IPersonService
    {
        void Dispose();

        void Create(Person person);
        void Delete(Person person);

        Person GetById(int id);

        IEnumerable<Person> GetAll();
        IEnumerable<Person> GetList(Expression<Func<Person, bool>> predicate);
    }
}
