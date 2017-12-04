using DotNetCore.Data.Entities;

namespace DotNetCore.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Person> PersonRepository { get; }

        void Save();
        void Dispose();
    }
}