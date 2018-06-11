using DotNetCore.BLL.Interfaces;
using DotNetCore.Data.Entities;
using DotNetCore.Data.Interfaces;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace DotNetCore.BLL.Services
{
    public class PersonService : IPersonService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public void Create(Person person)
        {
            _unitOfWork.PersonRepository.Add(person);
            _unitOfWork.Save();
        }

        public void Delete(Person person)
        {
            _unitOfWork.PersonRepository.Remove(person);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            var person = GetById(id);
            if (person == null)
                throw (new ArgumentException());

            _unitOfWork.PersonRepository.Remove(person);
            _unitOfWork.Save();
        }

        public Person GetById(int id)
        {
            return _unitOfWork.PersonRepository.Get(id);
        }

        public IEnumerable<Person> GetAll()
        {
            return _unitOfWork.PersonRepository.GetAllInclude();
        }

        public IEnumerable<Person> GetList(Expression<Func<Person, bool>> predicate)
        {
            return _unitOfWork.PersonRepository.Find(predicate);
        }
    }
}
