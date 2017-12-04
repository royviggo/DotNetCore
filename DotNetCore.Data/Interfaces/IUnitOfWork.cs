using DotNetCore.Data.Entities;

namespace DotNetCore.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Person> PersonRepository { get; }

        void Save();
        void Dispose();
    }
}