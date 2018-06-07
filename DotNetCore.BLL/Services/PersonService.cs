using DotNetCore.BLL.Interfaces;
using DotNetCore.Data.Entities;
using DotNetCore.Data.Interfaces;
using System;

using Microsoft.EntityFrameworkCore.Query;
using System.Linq;
using System.Linq.Expressions;

namespace DotNetCore.BLL.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(Person person)
        {
            _unitOfWork.PersonRepository.Add(person);
            _unitOfWork.Save();
        }

        public void Update(Person person)
        {
            _unitOfWork.PersonRepository.Update(person);
            _unitOfWork.Save();
        }

        public void Delete(Person person)
        {
            _unitOfWork.PersonRepository.Delete(person);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            var person = GetById(id);
            if (person == null)
                throw (new ArgumentException());

            _unitOfWork.PersonRepository.Delete(person);
            _unitOfWork.Save();
        }

        public Person GetById(int id)
        {
            return _unitOfWork.PersonRepository.GetById(id);
        }

        public IQueryable<Person> GetAll()
        {
            return _unitOfWork.PersonRepository.GetAll();
        }

        public IQueryable<Person> GetList(Expression<Func<Person, bool>> predicate = null,
                                    Func<IQueryable<Person>, IOrderedQueryable<Person>> orderBy = null,
                                   Func<IQueryable<Person>, IIncludableQueryable<Person, object>> include = null)
        {
            return _unitOfWork.PersonRepository.GetList(predicate, orderBy, include);
        }
    }
}
